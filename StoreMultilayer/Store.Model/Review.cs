namespace Store.Model
{
    public class Review
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public int? Rating { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public Guid CreatedByUserId { get; set; }
        public Guid UpdatedByUserId { get; set; }
    }
}
