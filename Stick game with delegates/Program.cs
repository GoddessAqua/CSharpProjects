using System;

namespace Sticks_game_with_delegates
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new SticksGame(10, Player.Human);
            game.MachinePlayed += Game_MachinePlayed;
            game.HumanTurnToMakeMove += Game_HumanToMakeMove;
            game.EndOfGame += Game_EndOfGame;
            game.Start();
        }

        private static void Game_EndOfGame(Player player)
        {
            Console.WriteLine($"Winner {player}");
        }

        private static void Game_HumanToMakeMove(object sender, int remainingSticks)
        {
            Console.WriteLine($"Remaining sticks: {remainingSticks}");
            Console.WriteLine("Take some sticks");

            bool takenCorrectly = false;
            while(!takenCorrectly)
            {
                if (int.TryParse(Console.ReadLine(), out int takenSticks))
                {
                    var game = (SticksGame)sender;
                    try
                    {
                        game.HumanTakes(takenSticks);
                        takenCorrectly = true;
                    }
                    catch(ArgumentException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }

        private static void Game_MachinePlayed(int sticksTaken)
        {
            Console.WriteLine($"Machine took: {sticksTaken}");
        }
    }
}
