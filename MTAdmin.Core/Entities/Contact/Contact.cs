namespace MTAdmin.Core.Entities.Contact
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Reason { get; set; }
        public string Message { get; set; }
        public string IPAddress { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
