namespace skiena.datastructures.graph
{
    public partial class EdgeClassifier<T> where T :IEquatable<T>
    {
        public enum enEdge 
        {
            Tree,
            Forward,
            Back,
            Cross
        }
    }
}
