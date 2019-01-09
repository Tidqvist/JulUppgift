using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGame
{
    class HigherOrLower
    {
        public PlayingCardDeck Deck { get; set; }
        public PlayingCard PlayersCard { get; set; }
        public PlayingCard TableCard { get; set; }
        public Player Player { get; set; }

        public void Start()
        {
            while (true)
            {
                ConsoleIO.PrintStartMenue();

                StartMenueOptions UsersChoice = ConsoleIO.GetUserStartMenueChoice();

                switch (UsersChoice)
                {
                    case StartMenueOptions.StartGame:
                        NewRound();
                        break;

                    case StartMenueOptions.Instructions:
                        ConsoleIO.AwaitKeypress();
                        break;

                    case StartMenueOptions.Highscores:
                        ConsoleIO.PrintHighScores();
                        ConsoleIO.AwaitKeypress();
                        break;

                    case StartMenueOptions.Exit:
                        return;

                    case StartMenueOptions.WipeHighscore:
                        ConsoleIO.WipeHighscores();
                        break;

                    default:
                        break;
                }
            }
        }

        private void NewRound()
        {
            AddPlayer();
            ConsoleIO.PrintShuffelScreen();
            SetUpNewDeck();
            GameLoop();
            SavePlayerHighscore();
            ConsoleIO.PrintHighScores();
            ConsoleIO.AwaitKeypress();
        }

        private void GameLoop()
        {
            while (true)
            {
                if (!DrawNewCards())
                {
                    ConsoleIO.PrintNoMoreCards();
                    break;
                }

                ConsoleIO.PrintGuessScreen(TableCard, PlayersCard, Player.TopScore);

                bool guessWasCorrect = GetUserGuessResult();

                if (guessWasCorrect)
                    Player.incPlayerScore();
                else
                    Player.ResetPlayerScore();

                PlayersCard.Flip();

                ConsoleIO.PrintRevealScreen(TableCard, PlayersCard, Player.TopScore, guessWasCorrect);

                ConsoleIO.PrintPlayerScore(Player.Score, Player.TopScore);

                if (ConsoleIO.UserWantsToExit())
                    break;
            }

            ConsoleIO.PrintPlayerTopScore(Player.TopScore);
            ConsoleIO.AwaitKeypress();
        }

        private  bool GetUserGuessResult()
        {
            HighOrLow playersGuess = ConsoleIO.GetGuess();
            HighOrLow correctAnswere = CompareCards();
            return playersGuess == correctAnswere;
        }

        private void SetUpNewDeck()
        {
            Deck = new PlayingCardDeck(true);
            Deck.ShuffleDeck();
        }

        internal void SavePlayerHighscore()
        {
            ConsoleIO.AddHighscore(Player.Name, Player.TopScore);
        }

        internal void AddPlayer()
        {
            Player = new Player();
        }

        public bool DrawNewCards()
        {
            try
            {
                PlayersCard = Deck.DrawTopCard();
                TableCard = Deck.DrawTopCard();
                TableCard.FaceDown = false;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            return true;

        }

        public HighOrLow CompareCards()
        {
            return PlayersCard.Value > TableCard.Value ? HighOrLow.High : HighOrLow.Low;
        }
    }

    public enum StartMenueOptions
    {
        StartGame = 1,
        Instructions,
        Highscores,
        Exit,
        WipeHighscore
    }
    public enum HighOrLow
    {
        Low,
        Equal,
        High
    }

}

