using System.Data;
using Dapper;

namespace ShipCaptainAndCrew
{
    public class DapperUserRepository : IUserRepository
    {
        private readonly IDbConnection _connection;

        public DapperUserRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                return _connection.Query<User>("SELECT * FROM Users INNER JOIN ShipCaptainAndCrew ON Users.Username = ShipCaptainAndCrew.Username");
            }
            catch (Exception ex)
            {
                Program.Speak("Sorry, but there was an issue with the connection to the database. This may be due to a temporary network issue or a problem with the database, or it may be an issue with your internet connection. Please try again later or send me an e-mail at daleoster@outlook.com for assistance.");
                return null;
            }
        }

        public User GetUser(string username)
        {
            try
            {
                return _connection.QuerySingle<User>("SELECT * FROM Users INNER JOIN ShipCaptainAndCrew ON Users.Username = ShipCaptainAndCrew.Username WHERE Users.Username = @username", new { username = username });
            }
            catch (Exception ex)
            {
                Program.Speak("Sorry, but there was an issue with the connection to the database. This may be due to a temporary network issue or a problem with the database, or it may be an issue with your internet connection. Please try again later or send me an e-mail at daleoster@outlook.com for assistance.");
                return null;
            }
        }

        public string GetPassword(string username)
        {
            try
            {
                return _connection.QuerySingle<string>("SELECT Password FROM Users WHERE Username = @username", new { username = username });
            }
            catch (Exception ex)
            {
                Program.Speak("Sorry, but there was an issue with the connection to the database. This may be due to a temporary network issue or a problem with the database, or it may be an issue with your internet connection. Please try again later or send me an e-mail at daleoster@outlook.com for assistance.");
                return null;
            }
        }

        public int CheckForUser(string username)
        {
            try
            {
                return _connection.QuerySingle<int>("SELECT COUNT(*) FROM Users WHERE Username = @username", new { username = username });
            }
            catch (Exception ex)
            {
                Program.Speak("Sorry, but there was an issue with the connection to the database. This may be due to a temporary network issue or a problem with the database, or it may be an issue with your internet connection. Please try again later or send me an e-mail at daleoster@outlook.com for assistance.");
                return -1;
            }
        }

        public void ResetStats(string username)
        {
            try
            {
                _connection.Execute("UPDATE ShipCaptainAndCrew SET GamesPlayed = 0, GamesWon = 0, AllTimeTreasure = 0 WHERE Username = @username;", new { username });
                Program.Speak("Your stats have been successfully reset for this game.");
                Options.Run();
            }
            catch (Exception ex)
            {
                Program.Speak("Sorry, but there was an issue with the connection to the database. This may be due to a temporary network issue or a problem with the database, or it may be an issue with your internet connection. Please try again later or send me an e-mail at daleoster@outlook.com for assistance.");
                Environment.Exit(0);
            }
        }

        public void InsertUser(string username, string password)
        {
            try
            {
                _connection.Execute("INSERT INTO Users (Username, Password) VALUES (@username, @password)", new { username, password });
                Program.Speak("Your account has been successfully created.");
            }
            catch (Exception ex)
            {
                Program.Speak("Sorry, but there was an issue with the connection to the database. This may be due to a temporary network issue or a problem with the database, or it may be an issue with your internet connection. Please try again later or send me an e-mail at daleoster@outlook.com for assistance.");
                Environment.Exit(0);
            }
        }

        public void UpdateUser(string username, int gamesPlayed, int gamesWon, int allTimeTreasure)
        {
            try
            {
                _connection.Execute("UPDATE ShipCaptainAndCrew SET GamesPlayed = GamesPlayed + @gamesPlayed, GamesWon = GamesWon + @gamesWon, AllTimeTreasure = AllTimeTreasure + @allTimeTreasure WHERE Username = @username", new { username, gamesPlayed, gamesWon, allTimeTreasure });
            }
            catch (Exception ex)
            {
                Program.Speak("Sorry, but there was an issue with the connection to the database. This may be due to a temporary network issue or a problem with the database, or it may be an issue with your internet connection. Please try again later or send me an e-mail at daleoster@outlook.com for assistance.");
                Environment.Exit(0);
            }
        }

        public void DeleteUser(string username)
        {
            try
            {
                _connection.Execute("DELETE FROM ShipCaptainAndCrew WHERE Username = @username", new { username = username });
                _connection.Execute("DELETE FROM Users WHERE Username = @username", new { username = username });
                Program.Speak("Your account has been deleted. Taking you back to the login screen.");
                Login.Run();
            }
            catch (Exception ex)
            {
                Program.Speak("Sorry, but there was an issue with the connection to the database. This may be due to a temporary network issue or a problem with the database, or it may be an issue with your internet connection. Please try again later or send me an e-mail at daleoster@outlook.com for assistance.");
                Environment.Exit(0);
            }
        }
    }
}