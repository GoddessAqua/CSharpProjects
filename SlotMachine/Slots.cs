using System;

namespace proj_1
{
    class Slots
    {
        public static int StartBalance;
        private int _rate;
        public int[] ScoreBoard;
        public static Random rnd;
        public Slots()
        {
            StartBalance = 100;
            CurrentBalance = StartBalance;
            Rate = 0;
            ScoreBoard = new int[3];
            rnd = new Random();
        }
        public int CurrentBalance
        {
            get;
            set;
        }
        public int Rate
        {
            get
            { return _rate; }
            set
            {
                if (value > CurrentBalance)
                {
                    Console.WriteLine("нельзя поставить больше чем у вас есть");
                }
                else
                {
                    _rate = value;
                }
            }
        }
        public void Rotate()
        {
            for (int i = 0; i < ScoreBoard.Length; i++)
            {
                ScoreBoard[i] = rnd.Next(0, 9);
            }
            for (int i = 0; i < ScoreBoard.Length; i++)
            {
                if (ScoreBoard[i] == 0)
                {
                    Console.Write("* ");
                }
                else
                {
                    Console.Write(ScoreBoard[i] + " ");

                }
            }
            Console.WriteLine();
        }
        public void MakeRate(int rate)
        {
            Rate = rate;
            CurrentBalance -= Rate;

        }
        public void CountResult()
        {
            if (ScoreBoard[0] == ScoreBoard[1] && ScoreBoard[0] == ScoreBoard[2])
            {
                CurrentBalance += Rate * 2;
            }
            else if (ScoreBoard[0] == ScoreBoard[1] && ScoreBoard[2] == 0)
            {
                CurrentBalance += Rate;
            }
            else if (ScoreBoard[0] == ScoreBoard[1] || ScoreBoard[0] == ScoreBoard[2] || ScoreBoard[1] == ScoreBoard[2])
            {
                CurrentBalance += Rate / 2;
            }
            else
            {
                CurrentBalance += 0;
            }
        }
        public void TotalCount()
        {
            if (CurrentBalance > StartBalance)
            {
                Console.WriteLine("вы выиграли");
            }
            else if (CurrentBalance == StartBalance)
            {
                Console.WriteLine("вы ничего не получили");
            }
            else
            {
                Console.WriteLine("вы проиграли");
            }
        }

    }
}
