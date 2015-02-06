namespace MyWebsite.Utils
{
    public class DataReturn<T>
    {
        public T Obj { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
