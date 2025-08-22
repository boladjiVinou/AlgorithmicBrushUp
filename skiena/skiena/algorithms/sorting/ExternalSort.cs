using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
            // create smaller sorted batch
            foreach (var batch in dataEnumerable.Chunk(batchSize)) 
            {
                writeData($"{workingDirPath}/batch_{nbBatch}.txt", batch.OrderBy(x=>x));
                ++nbBatch;
            }
            // merge batch and write it in another file
            writeData(resultPath, enumerateBatchMergeResult(nbBatch, workingDirPath));
            // delete  batches
            for (int i = 0; i < nbBatch; i++) 
            {
                var batch = getBatchFilePath(workingDirPath, i);
                if (File.Exists(batch)) 
                {
                    File.Delete(batch);
                }
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
                using (var writer = new StreamWriter(batchStream)) 
                {
                    foreach (T elem in batch) 
                    {
                        switch (Type.GetTypeCode(typeof(T)))
                        {
                            case TypeCode.Int32:
                                writer.WriteLine(Convert.ToInt32(elem));
                                break;
                            case TypeCode.Int64:
                                writer.WriteLine(Convert.ToInt64(elem));
                                break;
                            case TypeCode.Double:
                                writer.WriteLine(Convert.ToDouble(elem));
                                break;
                            case TypeCode.Single:
                                writer.WriteLine(Convert.ToSingle(elem));
                                break;
                            case TypeCode.Decimal:
                                writer.WriteLine(Convert.ToDecimal(elem));
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
            if (!File.Exists(path)) 
            {
                yield break;
            }
            using (FileStream stream = File.OpenRead(path)) 
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        line = line.Trim();

                        switch (Type.GetTypeCode(typeof(T)))
                        {
                            case TypeCode.Int32:
                                if (int.TryParse(line, out int result)) 
                                {
                                    yield return (T)(object)result;
                                }
                                break;
                            case TypeCode.Int64:
                                if (long.TryParse(line, out long resultLong))
                                {
                                    yield return (T)(object)resultLong;
                                }
                                break;
                            case TypeCode.Decimal:
                                if (decimal.TryParse(line, out decimal resultDec))
                                {
                                    yield return (T)(object)resultDec;
                                }
                                break;
                            case TypeCode.Double:
                                if (double.TryParse(line, out double resultDouble))
                                {
                                    yield return (T)(object)resultDouble;
                                }
                                break;
                            case TypeCode.Single:
                                if (float.TryParse(line, out float resultFloat))
                                {
                                    yield return (T)(object)resultFloat;
                                }
                                break;
                            default:
                                throw new NotSupportedException($"Type {typeof(T).Name} is not supported.");
                        }
                    }
                }

            }
        }
    }
}
