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

        public HigherOrLower()
        {
            SetUpNewDeck();
        }

        public void Start()
        {
            while (true)
            {
                ConsoleIO.PrintStartMenue();

                StartMenueOptions UsersChoice = ConsoleIO.GetUserStartMenueChoice();

                switch (UsersChoice)
                {
                    case StartMenueOptions.StartGame:
                        AddPlayer();
                        ConsoleIO.PrintShuffelScreen();
                        GameLoop();
                        SavePlayerHighscore();
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

        private void GameLoop()
        {
            while (true)
            {
                if (Deck.Deck.Count < 2)
                {
                    ConsoleIO.PrintShuffelScreen();
                    SetUpNewDeck();
                }

                SetUpNewRound();

                ConsoleIO.PrintGuessScreen(TableCard, PlayersCard);

                HighOrLow guess = ConsoleIO.GetGuess();
                HighOrLow correctAnswere = CompareCards();
                bool guessWasCorrect = guess == correctAnswere;

                PlayersCard.Flip();

                ConsoleIO.PrintRevealScreen(TableCard, PlayersCard, guessWasCorrect);

                if (guessWasCorrect)
                    Player.incPlayerScore();
                else
                    Player.ResetPlayerScore();

                ConsoleIO.PrintPlayerScore(Player.Score, Player.TopScore);

                if (ConsoleIO.UserWantsToExit())
                    return;
            }
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

        public void SetUpNewRound() 
        {
            PlayersCard = Deck.DrawTopCard();
            TableCard = Deck.DrawTopCard();
            TableCard.FaceDown = false;
        }

        public HighOrLow CompareCards()
        {
            if (PlayersCard.Rank > TableCard.Rank)
                return HighOrLow.High;
            else if (PlayersCard.Rank < TableCard.Rank)
                return HighOrLow.Low;
            else if (PlayersCard.Suit > TableCard.Suit)
                return HighOrLow.High;
            else if (PlayersCard.Suit < TableCard.Suit)
                return HighOrLow.Low;
            else
                return HighOrLow.Equal;
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

