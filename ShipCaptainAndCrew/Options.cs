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
                    Program.Speak("Please select desired voice.");
                    var voices = Program.synth.GetInstalledVoices();
                    bool voiceChanged = false;
                    while (voiceChanged == false)
                    {
                        foreach (var voice in voices)
                        {
                            bool validSelection = false;
                            do
                            {
                                Program.Speak($"Would you like to use {voice.VoiceInfo.Name}?");
                                ConsoleKey input = Console.ReadKey(intercept: true).Key;
                                switch (input)
                                {
                                    case ConsoleKey.Y:
                                        Program.synth.SelectVoice(voice.VoiceInfo.Name.ToString());
                                        validSelection = true;
                                        voiceChanged = true;
                                        Run();
                                        break;
                                    case ConsoleKey.N:
                                        validSelection = true;
                                        break;
                                    case ConsoleKey.B:
                                        validSelection = true;
                                        voiceChanged = true;
                                        Menu.OpenMenu();
                                        break;
                                    default:
                                        Program.Speak("Please make a valid selection.");
                                        continue;
                                }
                            }
                            while (validSelection == false);
                        }
                    }
                    break;
                case 3:
                    Program.Speak("Are you sure you want to reset your stats?");
                    Program.Speak("This will only reset your stats for this game.");
                    bool validChoice = false;
                    while (validChoice == false)
                    {
                        ConsoleKey key = Console.ReadKey(intercept: true).Key;
                        switch (key)
                        {
                            case ConsoleKey.Y:
                                userRepo.ResetStats(PlayerList.allPlayers.First().GetName());
                                validChoice = true;
                                break;
                            case ConsoleKey.N:
                                Run();
                                validChoice = true;
                                break;
                            default:
                                break;
                        }
                    };
                    break;
                case 4:
                    Program.Speak("Are you sure you want to delete your account? This is permanent and cannot be undone.");
                    Program.Speak("Please note that this will delete your account for all games, not just this one.");
                    validChoice = false;
                    while (validChoice == false)
                    {
                        ConsoleKey key = Console.ReadKey(intercept: true).Key;
                        switch (key)
                        {
                            case ConsoleKey.Y:
                                userRepo.DeleteUser(PlayerList.allPlayers.First().GetName());
                                validChoice = true;
                                break;
                            case ConsoleKey.N:
                                Run();
                                validChoice = true;
                                break;
                            default:
                                break;
                        }
                    };
                    break;
            }
        }
    }
}
