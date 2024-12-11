namespace ShipCaptainAndCrew
{
    public static class PlayerList
    {
        public static List<Player> allPlayers = new List<Player>();

        public static void Scoreboard()
        {
            Program.Speak("Current scores:");
            foreach (var player in allPlayers)
            {
                Program.Speak($"{player.GetName()}: {player.GetScore()}");
            }
        }
    }
}