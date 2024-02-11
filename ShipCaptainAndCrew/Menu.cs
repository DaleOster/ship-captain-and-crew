using System.Data;

namespace ShipCaptainAndCrew
{
    public class Menu
    {
        public static void OpenMenu()
        {
            var userRepo = DatabaseHelper.Connect();
            while (true)
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1) Play");
                Console.WriteLine("2) View my stats");
                Console.WriteLine("3) View the leaderboards");
                Console.WriteLine("4) Read the rules");
                Console.WriteLine("5) Reset stats");
                Console.WriteLine("6) Delete account");
                var input = Console.ReadLine();
                int choice;
                var validInput = int.TryParse(input, out choice);
                if (validInput == true)
                {
                    if (choice >= 1 && choice <= 6)
                    {
                        if (choice == 1)
                        {
                            Gameplay.SetUpGame(Gameplay.ChooseOponents());
                            Gameplay.Play();
                        }
                        else if (choice == 2)
                        {
                            if (userRepo.GetUser(PlayerList.allPlayers.First().GetName()) == null)
                            {
                                Environment.Exit(0);
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine($"Account created: {userRepo.GetUser(PlayerList.allPlayers.First().GetName()).UserSince} (UTC)");
                                Console.WriteLine();
                                Console.WriteLine("Your stats for Ship, Captain, and Crew:");
                                Console.WriteLine($"Total games played: {userRepo.GetUser(PlayerList.allPlayers.First().GetName()).GamesPlayed}");
                                Console.WriteLine($"Total games won: {userRepo.GetUser(PlayerList.allPlayers.First().GetName()).GamesWon}");
                                Console.WriteLine($"All-time treasure collected: {userRepo.GetUser(PlayerList.allPlayers.First().GetName()).AllTimeTreasure}");
                                Console.WriteLine();
                                OpenMenu();
                            }
                        }
                        else if (choice == 3)
                        {
                            if (userRepo.GetAllUsers() == null)
                            {
                                Environment.Exit(0);
                            }
                            else
                            {
                                var mostGamesWon = userRepo.GetAllUsers().OrderByDescending(u => u.GamesWon).ToList();
                                Console.WriteLine();
                                Console.WriteLine("Players with the most wins:");
                                var position = 1;
                                foreach (var user in mostGamesWon)
                                {
                                    Console.WriteLine($"{position}. {user.Username} ({user.GamesWon})");
                                    position++;
                                }
                                var mostTreasure = userRepo.GetAllUsers().OrderByDescending(u => u.AllTimeTreasure).ToList();
                                Console.WriteLine();
                                Console.WriteLine("Players with the most treasure collected:");
                                position = 1;
                                foreach (var user in mostTreasure)
                                {
                                    Console.WriteLine($"{position}. {user.Username} ({user.AllTimeTreasure})");
                                    position++;
                                }
                                Console.WriteLine();
                                OpenMenu();
                            }
                        }
                        else if (choice == 4)
                        {
                            ReadRules();
                        }
                        else if (choice == 5)
                        {
                            while (true)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Are you sure you want to reset your stats?");
                                Console.WriteLine("This will only reset your stats for this game.");
                                Console.WriteLine("1) Reset stats");
                                Console.WriteLine("2) Cancel");
                                var userInput = Console.ReadLine();
                                int decision;
                                var validChoice = int.TryParse(userInput, out decision);
                                if (validChoice == true)
                                {
                                    Console.WriteLine();
                                    if (decision >= 1 && decision <= 2)
                                    {
                                        if (decision == 1)
                                        {
                                            userRepo.ResetStats(PlayerList.allPlayers.First().GetName());
                                        }
                                        else
                                        {
                                            OpenMenu();
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Please make a valid selection.");
                                        Console.WriteLine();
                                        continue;
                                    }
                                }
                            }
                        }
                        else if (choice == 6)
                        {
                            while (true)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Are you sure you want to delete your account? This is permanent and cannot be undone.");
                                Console.WriteLine("Please note that this will delete your account for all games, not just this one.");
                                Console.WriteLine("1) Delete account");
                                Console.WriteLine("2) Cancel");
                                var userInput = Console.ReadLine();
                                int decision;
                                var validChoice = int.TryParse(userInput, out decision);
                                if (validChoice == true)
                                {
                                    Console.WriteLine();
                                    if (decision >= 1 && decision <= 2)
                                    {
                                        if (decision == 1)
                                        {
                                            userRepo.DeleteUser(PlayerList.allPlayers.First().GetName());
                                        }
                                        else
                                        {
                                            OpenMenu();
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Please make a valid selection.");
                                        continue;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Please make a valid selection.");
                                    continue;
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Please make a valid selection.");
                        Console.WriteLine();
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Please make a valid selection.");
                    Console.WriteLine();
                    continue;
                }
            }
        }

        public static void ReadRules()
        {

        }
    }
}