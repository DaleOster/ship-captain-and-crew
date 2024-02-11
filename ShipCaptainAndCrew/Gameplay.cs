namespace ShipCaptainAndCrew
{
    public class Gameplay
    {
        public static int ChooseOponents()
        {
            Console.WriteLine();
            Console.WriteLine("How many oponents would you like to play against?");
            var awaitingInput = true;
            var oponents = 0;
            while (awaitingInput == true)
            {
                var input = Console.ReadLine();
                var validInput = int.TryParse(input, out oponents);
                if (validInput == true)
                {
                    if (oponents >= 1)
                    {
                        awaitingInput = false;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Please choose 1 or more other players.");
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter a valid number.");
                }
            }
            return oponents;
        }

        public static void SetUpGame(int oponents)
        {
            for (int i = 0; i <= oponents - 1; i++)
            {
                Console.WriteLine();
                Console.WriteLine($"What would you like player {i + 2}'s name to be?");
                var player = new Player();
                var name = Console.ReadLine();
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
                Console.WriteLine();
                Console.WriteLine("It's your turn. What would you like to do?");
                Console.WriteLine("1) View scoreboard");
                Console.WriteLine("2) Roll the dice");
                var input = Console.ReadLine();
                int choice;
                var validInput = int.TryParse(input, out choice);
                if (validInput == true)
                {
                    if (choice == 1)
                    {
                        PlayerList.Scoreboard();
                    }
                    else if (choice == 2)
                    {
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
                                    Console.WriteLine();
                                    Console.WriteLine($"It's turn {i + 1} for {player.GetName()}.");
                                    var dice = RollDice(numberOfDice);
                                    Console.WriteLine($"{player.GetName()} rolled the following dice:");
                                    for (int j = 0; j < dice.Count; j++)
                                    {
                                        Console.WriteLine(dice[j]);
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
                                            Console.WriteLine($"You currently have {treasure} pieces of treasure.");
                                            Console.WriteLine("Would you like to keep it or try for more?");
                                            Console.WriteLine();
                                            Console.WriteLine("1) Keep what I've got.");
                                            Console.WriteLine("2) Plunder more treasure!");
                                            while (true)
                                            {
                                                var userInput = Console.ReadLine();
                                                int decision;
                                                var validChoice = int.TryParse(userInput, out decision);
                                                if (validChoice == true)
                                                {
                                                    if (decision == 1)
                                                    {
                                                        Console.WriteLine($"{player.GetName()} has gained {treasure} pieces of treasure!");
                                                        player.SetScore(treasure);
                                                        i = 3;
                                                        break;
                                                    }
                                                    else if (decision == 2)
                                                    {
                                                        Console.WriteLine($"May your blade always be wet, and powder dry!");
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Please make a valid selection.");
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Please make a valid selection.");
                                                }
                                            }
                                        }
                                        else if (i == 2)
                                        {
                                            Console.WriteLine($"{player.GetName()} has gained {treasure} pieces of treasure!");
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
                                                Console.WriteLine($"{player.GetName()} has gained {treasure} pieces of treasure!");
                                                player.SetScore(treasure);
                                                i = 3;
                                            }
                                            else if (treasure >= 9 && treasure <= 11)
                                            {
                                                if (i < 2)
                                                {
                                                    if (decision == 5)
                                                    {
                                                        Console.WriteLine($"{player.GetName()} has {treasure} pieces of treasure but decided to search for more.");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine($"{player.GetName()} has gained {treasure} pieces of treasure!");
                                                        player.SetScore(treasure);
                                                        // treasureSound.Play();
                                                        i = 3;
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"{player.GetName()} has gained {treasure} pieces of treasure!");
                                                    player.SetScore(treasure);
                                                }
                                            }
                                            else if (treasure >= 3 && treasure <= 8)
                                            {
                                                if (i < 2)
                                                {
                                                    if (decision <= 3)
                                                    {
                                                        Console.WriteLine($"{player.GetName()} has {treasure} pieces of treasure but decided to search for more.");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine($"{player.GetName()} has gained {treasure} pieces of treasure!");
                                                        player.SetScore(treasure);
                                                        i = 3;
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"{player.GetName()} has gained {treasure} pieces of treasure!");
                                                    player.SetScore(treasure);
                                                }
                                            }
                                            else if (treasure == 2)
                                            {
                                                if (i < 2)
                                                {
                                                    Console.WriteLine($"{player.GetName()} has {treasure} pieces of treasure but decided to search for more.");
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"{player.GetName()} has gained {treasure} pieces of treasure!");
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
                                Console.WriteLine($"The current leader is {leader.Name} with {leader.Score} pieces of treasure.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Please make a valid selection.");
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Please make a valid selection.");
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
            Console.WriteLine();
            Console.WriteLine($"Congrats to the winner, {winner}!");
            Console.WriteLine();
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
                    Console.WriteLine("You got your ship!");
                    hasShip = true;
                    dice.RemoveAt(i);
                    break;
                }
                else if (dice[i] == 6 && player.Name != PlayerList.allPlayers.First().GetName())
                {
                    Console.WriteLine($"{player.Name} got their ship!");
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
                    Console.WriteLine("You got your captain!");
                    dice.RemoveAt(i);
                    hasCaptain = true;
                    break;
                }
                else if (dice[i] == 5 && player.Name != PlayerList.allPlayers.First().GetName())
                {
                    Console.WriteLine($"{player.Name} got their captain!");
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
                    Console.WriteLine("You got your crew!");
                    dice.RemoveAt(i);
                    hasCrew = true;
                    break;
                }
                else if (dice[i] == 4 && player.Name != PlayerList.allPlayers.First().GetName())
                {
                    Console.WriteLine($"{player.Name} got their crew!");
                    dice.RemoveAt(i);
                    hasCrew = true;
                    break;
                }
            }
            return hasCrew;
        }
    }
}