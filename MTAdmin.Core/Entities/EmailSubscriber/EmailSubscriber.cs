namespace MTAdmin.Core.Entities.EmailSubscriber
{
    public class EmailSubscriber
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool IsValid { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
