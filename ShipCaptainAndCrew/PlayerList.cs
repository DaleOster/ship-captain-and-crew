namespace ShipCaptainAndCrew
{
    public static class PlayerList
    {
        public static List<Player> allPlayers = new List<Player>();

        public static void Scoreboard()
        {
            Console.WriteLine("");
            Console.WriteLine("Current scores:");
            foreach (var player in allPlayers)
            {
                Console.WriteLine($"{player.GetName()}: {player.GetScore()}");
            }
        }
    }
}