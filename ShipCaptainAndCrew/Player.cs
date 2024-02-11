namespace ShipCaptainAndCrew
{
    public class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }

        public Player()
        {

        }

        public Player(string name)
        {
            Name = name;
        }

        public string GetName()
        {
            var name = Name;
            return name;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public int GetScore()
        {
            var score = Score;
            return score;
        }

        public void SetScore(int scoreChange)
        {
            Score += scoreChange;
        }
    }
}