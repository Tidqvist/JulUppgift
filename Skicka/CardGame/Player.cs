using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame
{
    class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public int TopScore { get; set; }


        public Player()
        {
            Name = ConsoleIO.AskUserForUsername();
            Score = 0;
            TopScore = 0;
        }

        public void incPlayerScore()
        {
            Score = Score + 1;

            if (TopScore < Score)
                TopScore = Score;
        }

        public void ResetPlayerScore()
        {
            Score = 0;
        }

    }
}
