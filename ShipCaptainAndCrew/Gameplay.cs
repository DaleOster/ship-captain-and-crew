using System.Speech.Recognition;

namespace ShipCaptainAndCrew
{
    public class Gameplay
    {
        public static int ChooseOponents()
        {
            Program.Speak("How many oponents would you like to play against? 1 currently selected.");
            int oponents = 1;
            bool enterPressed = false;
            while (enterPressed == false)
            {
                ConsoleKey key = Console.ReadKey(intercept: true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (oponents < 3)
                        {
                            oponents += 1;
                            Program.Speak(oponents.ToString());
                            break;
                        }
                        else
                        {
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        if (oponents > 1)
                        {
                            oponents -= 1;
                            Program.Speak(oponents.ToString());
                            break;
                        }
                        else
                        {
                            break;
                        }
                    case ConsoleKey.Enter:
                        enterPressed = true;
                        break;
                    default:
                        continue;
                }
            }
            return oponents;
        }

        public static void SetUpGame(int oponents)
        {
            for (int i = 0; i <= oponents - 1; i++)
            {
                Program.Speak($"What would you like player {i + 2}'s name to be?");
                var player = new Player();
                var name = Program.hideInput();
                player.SetName(name);
                player.SetScore(0);
                PlayerList.allPlayers.Add(player);
            }
        }

        public static void Play()
        {
            var userRepo = DatabaseHelper.Connect();
            var score = 0;
            do
            {
                var random = new Random();
                var treasure = 0;
                Program.Speak("It's your turn. What would you like to do?");
                ConsoleKey key = Console.ReadKey(intercept: true).Key;
                switch (key)
                {
                    case ConsoleKey.S:
                        PlayerList.Scoreboard();
                        break;
                    case ConsoleKey.R:
                        foreach (var player in PlayerList.allPlayers)
                        {
                            if (score < 50 || player.GetScore() <= score - 12)
                            {
                                var performShipCheck = true;
                                var performCaptainCheck = true;
                                var performCrewCheck = true;
                                var numberOfDice = 5;
                                for (int i = 0; i < 3; i++)
                                {
                                    Program.Speak($"It's roll {i + 1} for {player.GetName()}.");
                                    var dice = RollDice(numberOfDice);
                                    Program.Speak($"{player.GetName()} rolled the following dice:");
                                    for (int j = 0; j < dice.Count; j++)
                                    {
                                        Program.Speak(dice[j].ToString());
                                    }
                                    if (performShipCheck == true)
                                    {
                                        if (CheckForShip(player, dice) == true)
                                        {
                                            numberOfDice = dice.Count;
                                            performShipCheck = false;
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                    if (performCaptainCheck == true)
                                    {
                                        if (CheckForCaptain(player, dice) == true)
                                        {
                                            numberOfDice = dice.Count;
                                            performCaptainCheck = false;
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                    if (performCrewCheck == true)
                                    {
                                        if (CheckForCrew(player, dice) == true)
                                        {
                                            numberOfDice = dice.Count;
                                            performCrewCheck = false;
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                    var humanPlayer = PlayerList.allPlayers.First().GetName();
                                    var currentRoller = player.GetName();
                                    if (humanPlayer == currentRoller)
                                    {
                                        if (i < 2)
                                        {
                                            treasure = 0;
                                            for (int k = 0; k < dice.Count; k++)
                                            {
                                                treasure += dice[k];
                                            }
                                            Program.Speak($"You currently have {treasure} pieces of treasure.");
                                            Program.Speak("Would you like to keep it or try for more?");
                                            while (true)
                                            {
                                                key = Console.ReadKey(intercept: true).Key;
                                                switch (key)
                                                {
                                                    case ConsoleKey.K:
                                                        Program.Speak($"{player.GetName()} has gained {treasure} pieces of treasure!");
                                                        player.SetScore(treasure);
                                                        i = 3;
                                                        break;
                                                    case ConsoleKey.P:
                                                        Program.Speak($"May your blade always be wet, and powder dry!");
                                                        break;
                                                }
                                                break;
                                            }
                                        }
                                        else if (i == 2)
                                        {
                                            Program.Speak($"{player.GetName()} has gained {treasure} pieces of treasure!");
                                            player.SetScore(treasure);
                                        }
                                    }
                                    else
                                    {
                                        if (i <= 2)
                                        {
                                            var decision = random.Next(1, 6);
                                            treasure = 0;
                                            for (int k = 0; k < dice.Count; k++)
                                            {
                                                treasure += dice[k];
                                            }
                                            if (treasure == 12)
                                            {
                                                Program.Speak($"{player.GetName()} has gained {treasure} pieces of treasure!");
                                                player.SetScore(treasure);
                                                i = 3;
                                            }
                                            else if (treasure >= 9 && treasure <= 11)
                                            {
                                                if (i < 2)
                                                {
                                                    if (decision == 5)
                                                    {
                                                        Program.Speak($"{player.GetName()} has {treasure} pieces of treasure but decided to search for more.");
                                                    }
                                                    else
                                                    {
                                                        Program.Speak($"{player.GetName()} has gained {treasure} pieces of treasure!");
                                                        player.SetScore(treasure);
                                                        // treasureSound.Play();
                                                        i = 3;
                                                    }
                                                }
                                                else
                                                {
                                                    Program.Speak($"{player.GetName()} has gained {treasure} pieces of treasure!");
                                                    player.SetScore(treasure);
                                                }
                                            }
                                            else if (treasure >= 3 && treasure <= 8)
                                            {
                                                if (i < 2)
                                                {
                                                    if (decision <= 3)
                                                    {
                                                        Program.Speak($"{player.GetName()} has {treasure} pieces of treasure but decided to search for more.");
                                                    }
                                                    else
                                                    {
                                                        Program.Speak($"{player.GetName()} has gained {treasure} pieces of treasure!");
                                                        player.SetScore(treasure);
                                                        i = 3;
                                                    }
                                                }
                                                else
                                                {
                                                    Program.Speak($"{player.GetName()} has gained {treasure} pieces of treasure!");
                                                    player.SetScore(treasure);
                                                }
                                            }
                                            else if (treasure == 2)
                                            {
                                                if (i < 2)
                                                {
                                                    Program.Speak($"{player.GetName()} has {treasure} pieces of treasure but decided to search for more.");
                                                }
                                                else
                                                {
                                                    Program.Speak($"{player.GetName()} has gained {treasure} pieces of treasure!");
                                                    player.SetScore(treasure);
                                                    i = 3;
                                                }
                                            }
                                        }
                                    }
                                }
                                var temporaryScore = 0;
                                temporaryScore += player.GetScore();
                                if (temporaryScore > score)
                                {
                                    score = temporaryScore;
                                }
                                var leader = PlayerList.allPlayers.OrderByDescending(p => p.Score).First();
                                Program.Speak($"The current leader is {leader.Name} with {leader.Score} pieces of treasure.");
                            }
                        }
                        break;
                    default:
                        Program.Speak("Please make a valid selection.");
                        break;
                }
            } while (score < 50);
            string winner = "";
            var scoreToBeat = 0;
            foreach (Player player in PlayerList.allPlayers)
            {
                if (player.GetScore() > scoreToBeat)
                {
                    scoreToBeat = player.GetScore();
                    winner = player.Name;
                }
            }
            if (winner == PlayerList.allPlayers.First().GetName())
            {
                userRepo.UpdateUser(PlayerList.allPlayers.First().GetName(), 1, 1, PlayerList.allPlayers.First().GetScore());
            }
            else
            {
                userRepo.UpdateUser(PlayerList.allPlayers.First().GetName(), 1, 0, PlayerList.allPlayers.First().GetScore());
            }
            Program.Speak($"Congrats to the winner, {winner}!");
            for (int i = PlayerList.allPlayers.Count - 1; i > 0; i--)
            {
                PlayerList.allPlayers.RemoveAt(i);
            }
            PlayerList.allPlayers.First().Score = 0;
            Menu.OpenMenu();
        }

        public static List<int> RollDice(int numberOfDice)
        {
            var dice = new List<int>();
            var random = new Random();
            for (int i = 0; i < numberOfDice; i++)
            {
                dice.Add(random.Next(1, 7));
            }
            return dice;
        }

        public static bool CheckForShip(Player player, List<int> dice)
        {
            var hasShip = false;
            for (int i = 0; i < dice.Count; i++)
            {
                if (dice[i] == 6 && player.Name == PlayerList.allPlayers.First().GetName())
                {
                    Program.Speak("You got your ship!");
                    hasShip = true;
                    dice.RemoveAt(i);
                    break;
                }
                else if (dice[i] == 6 && player.Name != PlayerList.allPlayers.First().GetName())
                {
                    Program.Speak($"{player.Name} got their ship!");
                    hasShip = true;
                    dice.RemoveAt(i);
                    break;
                }
            }
            return hasShip;
        }

        public static bool CheckForCaptain(Player player, List<int> dice)
        {
            var hasCaptain = false;
            for (int i = 0; i < dice.Count; i++)
            {
                if (dice[i] == 5 && player.Name == PlayerList.allPlayers.First().GetName())
                {
                    Program.Speak("You got your captain!");
                    dice.RemoveAt(i);
                    hasCaptain = true;
                    break;
                }
                else if (dice[i] == 5 && player.Name != PlayerList.allPlayers.First().GetName())
                {
                    Program.Speak($"{player.Name} got their captain!");
                    dice.RemoveAt(i);
                    hasCaptain = true;
                    break;
                }
            }
            return hasCaptain;
        }

        public static bool CheckForCrew(Player player, List<int> dice)
        {
            var hasCrew = false;
            for (int i = 0; i < dice.Count; i++)
            {
                if (dice[i] == 4 && player.Name == PlayerList.allPlayers.First().GetName())
                {
                    Program.Speak("You got your crew!");
                    dice.RemoveAt(i);
                    hasCrew = true;
                    break;
                }
                else if (dice[i] == 4 && player.Name != PlayerList.allPlayers.First().GetName())
                {
                    Program.Speak($"{player.Name} got their crew!");
                    dice.RemoveAt(i);
                    hasCrew = true;
                    break;
                }
            }
            return hasCrew;
        }
    }
}