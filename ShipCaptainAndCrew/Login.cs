namespace ShipCaptainAndCrew
{
    public class Login
    {
        public static void Run()
        {
            var userRepo = DatabaseHelper.Connect();
            Console.WriteLine();
            Console.WriteLine("Do you have an existing account?");
            Console.WriteLine("1) Yes");
            Console.WriteLine("2) No");
            var username = "";
            var humanPlayer = new Player();
            var input = Console.ReadLine();
            int choice;
            var validInput = int.TryParse(input, out choice);
            if (validInput == true)
            {
                if (choice >= 1 && choice <= 2)
                {
                    if (choice == 1)
                    {
                        var validUsername = false;
                        while (validUsername == false)
                        {
                            Console.WriteLine();
                            Console.WriteLine("What is your username?");
                            Console.WriteLine("Type \"back\" to go back.");
                            var usernameOrBack = Console.ReadLine();
                            if (usernameOrBack.Length >= 1)
                            {
                                if (usernameOrBack.ToLower() != "back")
                                {
                                    Loading.Run();
                                    username = userRepo.GetUser(usernameOrBack).Username;
                                    if (userRepo.CheckForUser(username) == -1)
                                    {
                                        Environment.Exit(0);
                                    }
                                    else if (userRepo.CheckForUser(username) == 0)
                                    {
                                        Console.WriteLine("Sorry, that username was not found.");
                                        continue;
                                    }
                                    else
                                    {
                                        var validPassword = false;
                                        while (validPassword == false)
                                        {
                                            Console.WriteLine("What is your password?");
                                            var password = Console.ReadLine();
                                            if (password.Length >= 1)
                                            {
                                                if (password == userRepo.GetPassword(username))
                                                {
                                                    humanPlayer.SetName(username);
                                                    PlayerList.allPlayers.Add(humanPlayer);
                                                    Console.WriteLine();
                                                    Console.WriteLine("You have successfully logged in.");
                                                    Console.WriteLine();
                                                    Menu.OpenMenu();
                                                    validUsername = true;
                                                    validPassword = true;
                                                }
                                                else
                                                {
                                                    Console.WriteLine();
                                                    Console.WriteLine("The password you entered is incorrect. Please try again.");
                                                    Console.WriteLine();
                                                    continue;
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Please enter a password.");
                                                Console.WriteLine();
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
                    }
                    else if (choice == 2)
                    {
                        var validUsername = false;
                        while (validUsername == false)
                        {
                            Console.WriteLine();
                            Console.WriteLine("What would you like your username to be?");
                            Console.WriteLine("Type \"back\" to go back.");
                            var usernameOrBack = Console.ReadLine();
                            if (usernameOrBack.ToLower() != "back")
                            {
                                if (usernameOrBack.Length >= 1)
                                {
                                    username = usernameOrBack;
                                    if (userRepo.CheckForUser(username) == 0)
                                    {
                                        while (true)
                                        {
                                            Console.WriteLine();
                                            Console.WriteLine("What would you like your password to be?");
                                            Console.WriteLine("Type \"back\" to go back.");
                                            var passwordOrBack = Console.ReadLine();
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
                                                Console.WriteLine("Sorry, you must enter a password.");
                                                continue;
                                            }
                                        }
                                    }
                                    else if (userRepo.CheckForUser(username) > 0)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("Sorry, that username is already taken.");
                                        continue;
                                    }
                                    else if (userRepo.CheckForUser(username) == -1)
                                    {
                                        Environment.Exit(0);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Sorry, you must enter a username.");
                                    continue;
                                }
                            }
                            else
                            {
                                Login.Run();
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Please make a valid selection.");
                    Login.Run();
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Please make a valid selection.");
                Login.Run();
            }
        }
    }
}