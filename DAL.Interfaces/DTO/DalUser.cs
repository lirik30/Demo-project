namespace DAL.Interfaces.DTO
{
    public class DalUser : IEntity
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Image { get; set; }

        public int? BlogId { get; set; }
    }
}
