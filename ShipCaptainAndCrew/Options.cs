using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShipCaptainAndCrew
{
    public class Options
    {
        public static void Run()
        {
            Program.Speak("Options - on \"Main Menu\"");
            var userRepo = DatabaseHelper.Connect();
            List<string> menuItems = new List<string>() { "Main Menu", "Change Speech Rate", "Change Speech Voice", "Reset Stats", "Delete Account" };
            int currentMenuOption = 0;
            int newMenuOption = 0;
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
                    Menu.OpenMenu();
                    break;
                case 1:
                    Program.Speak("Please select desired speech rate.");
                    List<int> speechRates = new List<int>() { -10, -9, -8, -7, -6, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                    currentMenuOption = 0;
                    enterPressed = false;
                    while (enterPressed == false)
                    {
                        ConsoleKey key = Console.ReadKey(intercept: true).Key;
                        switch (key)
                        {
                            case ConsoleKey.UpArrow:
                                if (currentMenuOption < 10)
                                {
                                    newMenuOption = currentMenuOption + 1;
                                    Program.Speak(newMenuOption.ToString());
                                    currentMenuOption++;
                                    break;
                                }
                                else
                                {
                                    currentMenuOption = 10;
                                    break;
                                }
                            case ConsoleKey.DownArrow:
                                if (currentMenuOption > -10)
                                {
                                    newMenuOption = currentMenuOption - 1;
                                    Program.Speak(newMenuOption.ToString());
                                    currentMenuOption--;
                                    break;
                                }
                                else
                                {
                                    currentMenuOption = -10;
                                    break;
                                }
                            case ConsoleKey.Enter:
                                enterPressed = true;
                                break;
                            default:
                                continue;
                        }
                    }
                    Program.synth.Rate = newMenuOption;
                    Run();
                    break;
                case 2:
                    var voices = Program.synth.GetInstalledVoices();
                    Program.Speak($"Please select desired voice - On {voices[0].VoiceInfo.Name}");
                    int currentVoice = 0;
                    int newVoice;
                    enterPressed = false;
                    while (enterPressed == false)
                    {
                        ConsoleKey key = Console.ReadKey(intercept: true).Key;
                        switch (key)
                        {
                            case ConsoleKey.UpArrow:
                                if (currentVoice > 0)
                                {
                                    newVoice = currentVoice - 1;
                                    Program.Speak(voices[newVoice].VoiceInfo.Name);
                                    currentVoice--;
                                    break;
                                }
                                else
                                {
                                    currentVoice = 0;
                                    break;
                                }
                            case ConsoleKey.DownArrow:
                                if (currentVoice < voices.Count - 1)
                                {
                                    newVoice = currentVoice + 1;
                                    Program.Speak(voices[newVoice].VoiceInfo.Name);
                                    currentVoice++;
                                    break;
                                }
                                else
                                {
                                    currentVoice = voices.Count - 1;
                                    break;
                                }
                            case ConsoleKey.Enter:
                                Program.synth.SelectVoice(voices[currentVoice].VoiceInfo.Name);
                                Run();
                                enterPressed = true;
                                break;
                            default:
                                continue;
                        }
                    }
                    break;
                case 3:
                    Program.Speak("Are you sure you want to reset your stats?");
                    Program.Speak("This will only reset your stats for this game.");
                    menuItems = new List<string>() { "Yes", "No" };
                    currentMenuOption = 0;
                    enterPressed = false;
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
                                if (currentMenuOption < 1)
                                {
                                    newMenuOption = currentMenuOption + 1;
                                    Program.Speak(menuItems[newMenuOption]);
                                    currentMenuOption++;
                                    break;
                                }
                                else
                                {
                                    currentMenuOption = 1;
                                    break;
                                }
                            case ConsoleKey.Enter:
                                if (currentMenuOption == 0)
                                {
                                    userRepo.ResetStats(PlayerList.allPlayers.First().GetName());
                                }
                                else
                                {
                                    Run();
                                }
                                enterPressed = true;
                                break;
                            default:
                                continue;
                        }
                    }
                    break;
                case 4:
                    Program.Speak("Are you sure you want to delete your account? This is permanent and cannot be undone.");
                    Program.Speak("Please note that this will delete your account for all games, not just this one.");
                    menuItems = new List<string>() { "Yes", "No" };
                    currentMenuOption = 0;
                    enterPressed = false;
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
                                if (currentMenuOption < 1)
                                {
                                    newMenuOption = currentMenuOption + 1;
                                    Program.Speak(menuItems[newMenuOption]);
                                    currentMenuOption++;
                                    break;
                                }
                                else
                                {
                                    currentMenuOption = 1;
                                    break;
                                }
                            case ConsoleKey.Enter:
                                if (currentMenuOption == 0)
                                {
                                    userRepo.DeleteUser(PlayerList.allPlayers.First().GetName());
                                }
                                else
                                {
                                    Run();
                                }
                                enterPressed = true;
                                break;
                            default:
                                continue;
                        }
                    }
                    break;
            }
        }
    }
}
