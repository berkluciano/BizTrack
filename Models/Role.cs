namespace UniProject1.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!; // Admin, Manager, User

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }

}
