namespace CSharpFuncStructs
{
    sealed class FList<T>
    {
        public T Head { get; }
        public FList<T> Tail { get; }
        public bool IsEmpty { get; }

        private FList(T head, FList<T> tail)
        {
            Head = head;
            Tail = tail.IsEmpty
                ? Empty
                : tail;
            IsEmpty = false;
        }

        private FList()
        {
            IsEmpty = true;
        }

        public static FList<T> Cons(T head, FList<T> tail)
            => tail.IsEmpty
                ? new FList<T>(head, Empty)
                : new FList<T>(head, tail);

        public FList<T> Cons(T head) => new FList<T>(head, this); 

        public static readonly FList<T> Empty = new FList<T>();
    }
}
