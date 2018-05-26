using DataBaseConnector;

namespace TaskBoard.Models.User
{
    public class UserModel
    {
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Education { get; set; }
        public OrderTheme InterestedTheme { get; set; }
        public string Information { get; set; }
        public string PhotoLink { get; set; }
        public int WorksPerformed { get; set; }
        public int WorksOrdered { get; set; }
    }
}
