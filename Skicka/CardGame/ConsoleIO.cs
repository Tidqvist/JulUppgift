using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CardGame
{
    public static class ConsoleIO
    {

        internal static HighOrLow GetGuess()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.UpArrow)
                return HighOrLow.High;
            else if (key.Key == ConsoleKey.DownArrow)
                return HighOrLow.Low;
            else
                return GetGuess();
        }

        internal static StartMenueOptions GetUserStartMenueChoice()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.D1:
                    return StartMenueOptions.StartGame;
                case ConsoleKey.D2:
                    return StartMenueOptions.Instructions;
                case ConsoleKey.D3:
                    return StartMenueOptions.Highscores;
                case ConsoleKey.D4:
                    return StartMenueOptions.Exit;
                case ConsoleKey.D5:
                    return StartMenueOptions.WipeHighscore;
                default:
                    return GetUserStartMenueChoice();
            }
        }

        internal static void PrintStartMenue()
        {
            Console.Clear();
            PrintVerticalyCenterdText(Enum.GetNames(typeof(StartMenueOptions)).Select((x, i) => $"{i + 1}. {x}").ToArray());
        }

        internal static void PrintHighScores()
        {
            List<string> highscore = new List<string>();

            try
            {
                string[] rows = System.IO.File.ReadAllLines("highscore.txt");

                highscore.Add($"{"Player",-10} {"Score",10}");
                highscore.Add($"{"----------",-10} {"----------",10}");

                foreach (string row in rows)
                {
                    string[] splittedRow = row.Split('*');
                    highscore.Add($"{splittedRow[0],-10} {splittedRow[1],10}");
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                highscore.Add("The highscore is empty, carry on!");
            }

            Console.Clear();
            PrintVerticalyCenterdText(highscore.ToArray());
        }

        internal static void PrintShuffelScreen()
        {
            Console.Clear();
            PrintVerticalyCenterdText("");
            int leftMargin = Console.WindowWidth / 2 - "Shuffeling new cards".Length / 2;
            Console.CursorLeft = leftMargin;
            Console.Write("Shuffeling new cards");
            for (int i = 0; i < 4; i++)
            {
                Thread.Sleep(400);
                Console.Write(".");
            }
        }

        internal static void PrintRevealScreen(PlayingCard tableCard, PlayingCard playersCard, bool userGuessWasCorrect)
        {
            CardPrinter.PrintCards(tableCard, playersCard);

            PrintHorizontalyCenterdText(userGuessWasCorrect ? "Correct!" : "Wrong");
        }

        internal static void PrintPlayerScore(int score, int topScore)
        {
            PrintHorizontalyCenterdText($"Your current streak is {score} guesses",$"Your best streak is {topScore} guesses");
        }

        internal static void PrintGuessScreen(PlayingCard tableCard, PlayingCard playersCard)
        {
            CardPrinter.PrintCards(tableCard, playersCard);
            PrintHorizontalyCenterdText("Is the hidden card higher or lower?", "(Use the up or down arrow to guess)");
        }

        internal static void AddHighscore(string name, int score)
        {
            if (System.IO.File.Exists("highscore.txt"))
                System.IO.File.AppendAllLines("highscore.txt", new List<string> { $"{name}*{score}" });
            else
                System.IO.File.WriteAllLines("highscore.txt", new List<string> { $"{name}*{score}" });
        }

        internal static void WipeHighscores()
        {
            if (System.IO.File.Exists("highscore.txt"))
                System.IO.File.Delete("highscore.txt");
        }

        internal static string AskUserForUsername()
        {
            Console.Clear();
            PrintVerticalyCenterdText("Enter username");
            int leftMargin = Console.WindowWidth / 2 - "Enter username".Length / 2;
            Console.CursorLeft = leftMargin;
            string userInput = Console.ReadLine();
            return userInput;
        }

        internal static bool UserWantsToExit()
        {
            PrintHorizontalyCenterdText("","Press any key to play again or esc to quit...");
            var pressedKey = Console.ReadKey();
            return pressedKey.Key == ConsoleKey.Escape;
        }
        internal static void AwaitKeypress()
        {
            Console.ReadKey();
        }


        //////////// PRINTING 
        public static void PrintVerticalyCenterdText(params string[] strings)
        {
            int topMargin = Console.WindowHeight / 2 - strings.Length / 2;
            Console.CursorTop = topMargin;
            PrintHorizontalyAlignedText(strings);
        }

        public static void PrintHorizontalyCenterdText(params string[] strings)
        {
            foreach (var s in strings)
            {
                Console.CursorLeft = Console.WindowWidth / 2 - s.Length / 2;
                Console.WriteLine(s);
            }
        }

        public static void PrintHorizontalyAlignedText(params string[] strings)
        {
            int lengthOfLongestString = strings.Max(s => s.Length);
            int leftMargin = Console.WindowWidth / 2 - lengthOfLongestString / 2;
            foreach (var s in strings)
            {
                Console.CursorLeft = leftMargin;
                Console.WriteLine(s);
            }
        }

    }
}
