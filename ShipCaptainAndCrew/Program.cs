using System.Media;
using System.Numerics;
using System.Speech.Synthesis;
using System.Text;

namespace ShipCaptainAndCrew
{
    public class Program
    {
        public static SpeechSynthesizer synth = new SpeechSynthesizer();
        public static SoundPlayer player = new SoundPlayer();

        static void Main(string[] args)
        {
            PlaySound(@"Audio\736852__xkeril__transition-hit-and-whoosh.wav");
            Thread.Sleep(2500);
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

        public static void PlaySound(string soundFilePath)
        {
            player.SoundLocation = soundFilePath;
            player.Load();
            player.Play();
        }
    }
}