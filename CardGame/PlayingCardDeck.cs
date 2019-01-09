using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGame
{
    class PlayingCardDeck
    {
        public Queue<PlayingCard> Deck { get; set; }

        public PlayingCardDeck(bool backSideUp)
        {
            Deck = new Queue<PlayingCard>();

            foreach (var card in GenerateDeck(backSideUp))
                Deck.Enqueue(card);
        }

        public PlayingCard DrawTopCard()
        {
            return Deck.Dequeue();
        }

        public void PlaceCardAtBottom(PlayingCard card)
        {
            Deck.Enqueue(card);
        }

        public void ShuffleDeck()
        {
            List<PlayingCard> DeckAsList = this.Deck.ToList();
            this.Deck = new Queue<PlayingCard>();

            Random rnd = new Random();

            while (DeckAsList.Count > 0) //puts a random card from the inputdeck into the returndeck and removes the random card from the inputdeck
            {
                int cardIndex = rnd.Next(0, DeckAsList.Count);
                Deck.Enqueue(DeckAsList[cardIndex]);
                DeckAsList.RemoveAt(cardIndex);
            }
        }

        public static IEnumerable<PlayingCard> GenerateDeck(bool backSideUp)
        {
            foreach (var suit in GenerateSuits())
                foreach (var rank in GenerateRanks())
                    yield return new PlayingCard(suit, rank, backSideUp);
        }

        public static IEnumerable<Ranks> GenerateRanks()
        {
            foreach (var rank in (Ranks[]) Enum.GetValues(typeof(Ranks)))
                yield return rank;
        }

        public static IEnumerable<Suits> GenerateSuits()
        {
            foreach (var suit in (Suits[])Enum.GetValues(typeof(Suits)))
                yield return suit;
        }
    }

}
