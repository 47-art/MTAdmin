namespace MTAdmin.Infra.Identity.Entities.Comman
{
    public interface IBaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public string CreatedById { get; set; }
        public AppUser CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string ModifiedById { get; set; }
        public AppUser ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
