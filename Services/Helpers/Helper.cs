using System.Media;
using System.Text.RegularExpressions;

namespace Services.Helpers;

public static class Helper
{
    public static void ColorWrite(ConsoleColor color, string text)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    public static string CheckString(string errorMsg)
    {
        while (true)
        {
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input) && Regex.IsMatch(input, @"^[a-zA-Z\s]+$"))
                return input;

            ColorWrite(ConsoleColor.Red, errorMsg);
        }
    }

    public static string CheckLetterOrDigit(string errorMsg)
    {
        while (true)
        {
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input) && Regex.IsMatch(input, @"^[a-zA-Z0-9\s]+$"))
                return input;

            ColorWrite(ConsoleColor.Red, errorMsg);
        }
    }

    public static int CheckInt(string errorMsg)
    {
        while (true)
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out int result) && result >= 0)
                return result;

            ColorWrite(ConsoleColor.Red, errorMsg);
        }
    }

    public static void PlaySound(string fileName, bool async = true)
    {
        try
        {
            // Səs faylının yolu
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds", fileName);

            if (!File.Exists(path))
            {
                // Əgər səs tapılmasa, fallback beep
                Console.Beep(700, 150);
                return;
            }

            using (SoundPlayer player = new SoundPlayer(path))
            {
                if (async)
                    player.Play();      // arxa planda oxuyur
                else
                    player.PlaySync();  // proqramı dayandırır, səs bitəndə davam edir
            }
        }
        catch
        {
            // fallback (səs oynatmaq alınmasa)
            Console.Beep(500, 200);
        }
    }
}
