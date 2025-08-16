using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace skiena.algorithms.sorting
{
    public class ExternalSort<T> where T : IEquatable<T>, IComparable<T>
    {
        public static void sort(string dataPath,string workingDirPath, string resultPath, int batchSize)
        {
            var dataEnumerable = enumerateData(dataPath);
            int nbBatch = 0;
            // create smaller batch
            while (dataEnumerable.Any()) 
            {
               var batchData = dataEnumerable.Take(batchSize).OrderBy(x => x);
               writeData($"{workingDirPath}/batch_{nbBatch}.txt", batchData);
                ++nbBatch;
            }
            // merge batch and write it in another file
            writeData(resultPath, enumerateBatchMergeResult(nbBatch, workingDirPath));
            // delete smaller batches
            for (int i = 0; i < nbBatch; i++) 
            {
                File.Delete(getBatchFilePath(workingDirPath, i));
            }
        }
        private static IEnumerable<T> enumerateBatchMergeResult(int nbBatch, string workingDirPath) 
        {
            List<IEnumerator<T>> batchEnumerators = new List<IEnumerator<T>>();
            for (int i = 0; i < nbBatch; i++)
            {
                batchEnumerators.Add(enumerateData(getBatchFilePath(workingDirPath, i)).GetEnumerator());
            }
            PriorityQueue<IEnumerator<T>, T> queue = new PriorityQueue<IEnumerator<T>, T>();
            for (int i = 0; i < batchEnumerators.Count; i++)
            {
                if (batchEnumerators[i].MoveNext())
                {
                    queue.Enqueue(batchEnumerators[i], batchEnumerators[i].Current);
                }
            }
            while (queue.Count > 0)
            {
                IEnumerator<T> lowest = queue.Dequeue();
                yield return lowest.Current;
                if (lowest.MoveNext()) 
                {
                    queue.Enqueue(lowest, lowest.Current);
                }
            }
        }

        private static string getBatchFilePath(string workingDirPath, int i)
        {
            return $"{workingDirPath}/batch_{i}.txt";
        }

        private static void writeData(string batchFile,IEnumerable<T> batch) 
        {
            using (var batchStream = File.Create(batchFile)) 
            {
                using (var writer = new BinaryWriter(batchStream)) 
                {
                    foreach (T elem in batch) 
                    {
                        switch (Type.GetTypeCode(typeof(T)))
                        {
                            case TypeCode.Int32:
                                writer.Write(Convert.ToInt32(elem));
                                break;
                            case TypeCode.Int64:
                                writer.Write(Convert.ToInt64(elem));
                                break;
                            case TypeCode.Double:
                                writer.Write(Convert.ToDouble(elem));
                                break;
                            case TypeCode.Single:
                                writer.Write(Convert.ToSingle(elem));
                                break;
                            case TypeCode.Decimal:
                                writer.Write(Convert.ToDecimal(elem));
                                break;
                            default:
                                throw new NotSupportedException($"Type {typeof(T).Name} is not supported.");
                        }
                    }
                }
            }
        }
        public static IEnumerable<T> enumerateData(string path) 
        {
            using (FileStream stream = File.OpenRead(path)) 
            {
                using (var reader = new BinaryReader(stream))
                {
                    switch (Type.GetTypeCode(typeof(T)))
                    {
                        case TypeCode.Int32:
                            yield return (T)(object)reader.ReadInt32();
                            break;
                        case TypeCode.Int64:
                            yield return (T)(object)reader.ReadInt64();
                            break;
                        case TypeCode.Decimal:
                            yield return (T)(object)reader.ReadDecimal();
                            break;
                        case TypeCode.Double:
                            yield return (T)(object)reader.ReadDouble();
                            break;
                        case TypeCode.Single:
                            yield return (T)(object)reader.ReadSingle();
                            break;
                        default:
                            throw new NotSupportedException($"Type {typeof(T).Name} is not supported.");
                    }
                }
            }
        }
    }
}
