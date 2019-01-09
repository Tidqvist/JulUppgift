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
            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    return HighOrLow.High;
                case ConsoleKey.DownArrow:
                    return HighOrLow.Low;
                default:
                    return GetGuess();
            }
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
            var highscoreToPrint = new List<string[]>();

            try
            {

                List<HighScoreElement> highscore = new List<HighScoreElement>();

                string[] rows = System.IO.File.ReadAllLines("highscore.txt");

                foreach (string row in rows)
                {
                    string[] splittedRow = row.Split('*');

                    var highScoreElement = new HighScoreElement()
                    {
                        Name = splittedRow[0],
                        Score = int.Parse(splittedRow[1])
                    };

                    highscore.Add(highScoreElement);
                }

                highscore = highscore.OrderByDescending(x => x.Score).ToList();

                highscoreToPrint.Add(new string[] { $"{"Player"}", $"{"Score"}" });
                highscoreToPrint.Add(new string[] { $"{"----------"}", $"{"----------"}" });
                highscore.ForEach(x => highscoreToPrint.Add(new string[] { $"{x.Name}", $"{x.Score}" }));

            Console.Clear();
                PrintVerticalyCenterdText(highscoreToPrint.Select(x => $"{x[0],-10} {x[1],10}").ToArray());

                //Console.Clear();
                //PrintVerticalyCenterdText(highscoreToPrint.Select(x => $"{x[0],-10} {x[1],10}").ToArray());
                //highscore.ForEach(x => Console.WriteLine($"{x.Name,-10} {x.Score,10}"));
            }
            catch (System.IO.FileNotFoundException)
            {

            Console.Clear();

            PrintVerticalyCenterdText("The highscore is empty, carry on!");
            }
        }

        internal static void PrintNoMoreCards()
        {
            Console.Clear();
            PrintHorizontalyCenterdText("The deck is empty!");
            AwaitKeypress();
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

        internal static void PrintRevealScreen(PlayingCard tableCard, PlayingCard playersCard, int topScore, bool userGuessWasCorrect)
        {
            Console.Clear();
            PrintHorizontalyCenterdText($"Your best streak is {topScore} correct guesses");
            CardPrinter.PrintCards(tableCard, playersCard);
            PrintHorizontalyCenterdText(userGuessWasCorrect ? "Correct!" : "Wrong");
        }

        internal static void PrintPlayerScore(int score, int topScore)
        {
            PrintHorizontalyCenterdText($"Your current streak is {score} guesses");
        }
        internal static void PrintPlayerTopScore(int topScore)
        {
            Console.Clear();
            PrintVerticalyCenterdText($"Your best streak was {topScore} correct guesses");
        }

        internal static void PrintGuessScreen(PlayingCard tableCard, PlayingCard playersCard, int topScore)
        {
            Console.Clear();
            PrintHorizontalyCenterdText($"Your best streak is {topScore} correct guesses");
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
            PrintHorizontalyCenterdText("", "Press any key to play again or esc to quit...");
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
