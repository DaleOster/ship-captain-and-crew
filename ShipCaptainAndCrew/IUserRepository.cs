namespace ShipCaptainAndCrew
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetAllUsers();

        public User GetUser(string username);

        public string GetPassword(string username);

        public int CheckForUser(string username);

        public void ResetStats(string username);

        public void InsertUser(string username, string password);

        public void UpdateUser(string username, int gamesPlayed, int gamesWon, int allTimeTreasure);

        public void DeleteUser(string username);
    }
}