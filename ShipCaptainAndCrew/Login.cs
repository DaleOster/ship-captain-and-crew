using Microsoft.Identity.Client;

namespace ShipCaptainAndCrew
{
    public class Login
    {
        public static void Run()
        {
            var userRepo = DatabaseHelper.Connect();
            Program.Speak("Do you have an existing account?");
            var username = "";
            var humanPlayer = new Player();
            ConsoleKey key = Console.ReadKey(intercept: true).Key;
            switch (key)
            {
                case ConsoleKey.Y:
                    var validUsername = false;
                    while (validUsername == false)
                    {
                        Program.Speak("What is your username? Type \"back\" to go back.");
                        var usernameOrBack = Program.hideInput();
                        if (usernameOrBack.Length >= 1)
                        {
                            if (usernameOrBack.ToLower() != "back")
                            {
                                Program.Speak("Loading");
                                username = userRepo.GetUser(usernameOrBack).Username;
                                if (userRepo.CheckForUser(username) == -1)
                                {
                                    Environment.Exit(0);
                                }
                                else if (userRepo.CheckForUser(username) == 0)
                                {
                                    Program.Speak("Sorry, that username was not found.");
                                    continue;
                                }
                                else
                                {
                                    var validPassword = false;
                                    while (validPassword == false)
                                    {
                                        Program.Speak("What is your password?");
                                        var password = Program.hideInput();
                                        if (password.Length >= 1)
                                        {
                                            if (password == userRepo.GetPassword(username))
                                            {
                                                humanPlayer.SetName(username);
                                                PlayerList.allPlayers.Add(humanPlayer);
                                                Program.Speak("You have successfully logged in.");
                                                Menu.OpenMenu();
                                                validUsername = true;
                                                validPassword = true;
                                            }
                                            else
                                            {
                                                Program.Speak("The password you entered is incorrect.");
                                                continue;
                                            }
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Login.Run();
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    break;
                case ConsoleKey.N:
                    validUsername = false;
                    while (validUsername == false)
                    {
                        Program.Speak("What would you like your username to be?");
                        Program.Speak("Type \"back\" to go back.");
                        var usernameOrBack = Program.hideInput();
                        if (usernameOrBack.ToLower() != "back")
                        {
                            if (usernameOrBack.Length >= 1)
                            {
                                username = usernameOrBack;
                                if (userRepo.CheckForUser(username) == 0)
                                {
                                    while (true)
                                    {
                                        Program.Speak("What would you like your password to be?");
                                        Program.Speak("Type \"back\" to go back.");
                                        var passwordOrBack = Program.hideInput();
                                        if (passwordOrBack.Length >= 1)
                                        {
                                            if (passwordOrBack.ToLower() != "back")
                                            {
                                                var password = passwordOrBack;
                                                userRepo.InsertUser(username, password);
                                                humanPlayer.SetName(username);
                                                PlayerList.allPlayers.Add(humanPlayer);
                                                Menu.OpenMenu();
                                                validUsername = true;
                                                break;
                                            }
                                            else
                                            {
                                                Login.Run();
                                            }
                                        }
                                        else
                                        {
                                            Program.Speak("Sorry, you must enter a password.");
                                            continue;
                                        }
                                    }
                                }
                                else if (userRepo.CheckForUser(username) > 0)
                                {
                                    Program.Speak("Sorry, that username is already taken.");
                                    continue;
                                }
                                else if (userRepo.CheckForUser(username) == -1)
                                {
                                    Environment.Exit(0);
                                }
                            }
                            else
                            {
                                Program.Speak("Sorry, you must enter a username.");
                                continue;
                            }
                        }
                        else
                        {
                            Login.Run();
                        }
                    }
                    break;
                default:
                    Login.Run();
                    break;
            }
        }
    }
}