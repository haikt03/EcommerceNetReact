using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class AppUser: IdentityUser<int>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public Image? Image { get; set; }
        public UserAddress? Address { get; set; }
        public List<RefreshToken>? RefreshTokens { get; set; }
    }
}
