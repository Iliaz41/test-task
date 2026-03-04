namespace Fitness.Domain.Models
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PasswordHash { get; set; } // for next features

        public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}
