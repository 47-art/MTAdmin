namespace MTAdmin.Shared.ViewModels
{
    public class IdNameVM<T> where T : struct
    {
        public T Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
    }
}
