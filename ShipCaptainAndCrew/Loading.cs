namespace ShipCaptainAndCrew
{
    public static class Loading
    {
        public static void Run()
        {
            Console.WriteLine();
            Console.Write("Loading ");
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(1000);
                Console.Write(".");
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
