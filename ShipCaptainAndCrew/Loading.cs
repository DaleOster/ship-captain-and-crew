namespace ShipCaptainAndCrew
{
    public static class Loading
    {
        public static void Run()
        {
            var percentage = 0;
            for (int i = 0; i < 4; i++)
            {
                percentage += 25;
                Thread.Sleep(500);
                Program.Speak($"Loading is {percentage} percent complete.");
            }
        }
    }
}
