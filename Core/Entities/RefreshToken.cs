namespace Core.Entities
{
    public class RefreshToken : BaseEntity
    {
        public required string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime? RevokedAt { get; set; }

        public int? UserId { get; set; }
        public AppUser? User { get; set; }
    }
}
