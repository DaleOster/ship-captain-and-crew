using System.Speech.Synthesis;
using System.Text;

namespace ShipCaptainAndCrew
{
    public class Program
    {
        public static SpeechSynthesizer synth = new SpeechSynthesizer();

        static void Main(string[] args)
        {
            synth.SetOutputToDefaultAudioDevice();
            synth.Speak("Ahoy, matey!");
            Login.Run();
        }

        public static void Speak(string text)
        {
            synth.Speak(text);
        }

        public static string hideInput()
        {
            StringBuilder input = new StringBuilder();
            ConsoleKeyInfo keyInfo;
            while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                if (keyInfo.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input.Length--;
                }
                else if (keyInfo.Key != ConsoleKey.Backspace)
                {
                    input.Append(keyInfo.KeyChar);
                }
            }
            return input.ToString();
        }
    }
}