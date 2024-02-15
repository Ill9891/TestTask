namespace EmulationOfServices.Models
{
    public class User
    {
        public int Id { get; }

        public User(int userId)
        {
            Id = userId;
        }
    }
}