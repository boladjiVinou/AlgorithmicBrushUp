namespace skiena.datastructures.lists
{
    public class LinkedNode<T>(T val) where T : IEquatable<T>
    {
        public T Value { get; set; } = val;
        public LinkedNode<T> Next { get; set; }
        public LinkedNode<T> Previous { get; set; }
    }
}
