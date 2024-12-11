using Microsoft.Identity.Client;
using System.ComponentModel;
using System.Data;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace ShipCaptainAndCrew
{
    public class Menu
    {
        public static void OpenMenu()
        {
            Program.Speak("Main Menu - on \"Play\"");
            var userRepo = DatabaseHelper.Connect();
            List<string> menuItems = new List<string>() { "Play", "View stats", "View Leaderboards", "Read Rules", "Options" };
            int currentMenuOption = 0;
            int newMenuOption;
            bool enterPressed = false;
            while (enterPressed == false)
            {
                ConsoleKey key = Console.ReadKey(intercept: true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (currentMenuOption > 0)
                        {
                            newMenuOption = currentMenuOption - 1;
                            Program.Speak(menuItems[newMenuOption]);
                            currentMenuOption--;
                            break;
                        }
                        else
                        {
                            currentMenuOption = 0;
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        if (currentMenuOption < 4)
                        {
                            newMenuOption = currentMenuOption + 1;
                            Program.Speak(menuItems[newMenuOption]);
                            currentMenuOption++;
                            break;
                        }
                        else
                        {
                            currentMenuOption = 4;
                            break;
                        }
                    case ConsoleKey.Enter:
                        enterPressed = true;
                        break;
                    default:
                        continue;
                }
            }
            switch (currentMenuOption)
            {
                case 0:
                    Gameplay.SetUpGame(Gameplay.ChooseOponents());
                    Gameplay.Play();
                    break;
                case 1:
                    if (userRepo.GetUser(PlayerList.allPlayers.First().GetName()) == null)
                    {
                        Environment.Exit(0);
                        break;
                    }
                    else
                    {
                        Program.Speak($"Account created: {userRepo.GetUser(PlayerList.allPlayers.First().GetName()).UserSince} (UTC)");
                        Program.Speak("Your stats for Ship, Captain, and Crew:");
                        Program.Speak($"Total games played: {userRepo.GetUser(PlayerList.allPlayers.First().GetName()).GamesPlayed}");
                        Program.Speak($"Total games won: {userRepo.GetUser(PlayerList.allPlayers.First().GetName()).GamesWon}");
                        Program.Speak($"All-time treasure collected: {userRepo.GetUser(PlayerList.allPlayers.First().GetName()).AllTimeTreasure}");
                        OpenMenu();
                        break;
                    }
                case 2:
                    if (userRepo.GetAllUsers() == null)
                    {
                        Environment.Exit(0);
                        break;
                    }
                    else
                    {
                        var mostGamesWon = userRepo.GetAllUsers().OrderByDescending(u => u.GamesWon).ToList();
                        Program.Speak("Players with the most wins:");
                        var position = 1;
                        foreach (var user in mostGamesWon)
                        {
                            Program.Speak($"{position}. {user.Username} ({user.GamesWon})");
                            position++;
                        }
                        var mostTreasure = userRepo.GetAllUsers().OrderByDescending(u => u.AllTimeTreasure).ToList();
                        Program.Speak("Players with the most treasure collected:");
                        position = 1;
                        foreach (var user in mostTreasure)
                        {
                            Program.Speak($"{position}. {user.Username} ({user.AllTimeTreasure})");
                            position++;
                        }
                        OpenMenu();
                        break;
                    }
                case 3:
                    ReadRules();
                    break;
                case 4:
                    Options.Run();
                    break;
            }
        }

        public static void ReadRules()
        {

        }
    }
}