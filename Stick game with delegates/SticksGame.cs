/*
 * 
 * Реализуйте игру таким образом, чтобы против человек играл компьютер. 
 * Изначально есть 10 палочек. На каждом ходу выводите на консоль текущее 
 * количество оставшихся палочек и просите ввести количество палочек, которое 
 * хочет взять игрок (который делает ход. машина делает ход автоматически при 
 * своей очереди, её просить не надо :)).Не забывайте менять очерёдность игроков 
 * и сокращать кол-во палочек. В конце надо вывести кто победил - человек или машина.
 * 
 */
using System;

namespace Sticks_game_with_delegates
{

    public class SticksGame
    {
        private readonly Random randomizer;
        public int InitialSticksNumber { get; } // начальное число палочек
        public Player Turn { get; private set; } // Чей ход сейчас?
        public int RemainingSticks { get; private set; } // Оставшиеся палочки
        public GameStatus GameStatus { get; private set; }  // Статус игры
        
        public event Action<int> MachinePlayed; // Ход машины
        public event EventHandler<int> HumanTurnToMakeMove; // Ход человека
        public event Action<Player> EndOfGame; // Конец игры

        public SticksGame(int initialSticksNumber, Player whoMakesFirstMove)
        {
            if (initialSticksNumber < 7 || initialSticksNumber>30) 
                throw new ArgumentException("Initial number of sticks should be >= 7 && <=30");
            
            InitialSticksNumber = initialSticksNumber; // начальное состояние передаётся параметром при создании объекта класса
            Turn = whoMakesFirstMove; // С кого начинается ход. Указывается при создании объекта.
            GameStatus = GameStatus.NotStarted; // Статус игры в самом начале
            RemainingSticks = InitialSticksNumber;  // (?)
            randomizer = new Random(); 
        }
        
        public void HumanTakes(int sticks)
        {
            if (sticks < 1 || sticks > 3)
            {
                throw new ArgumentException("You can take from 1 to 3 sticks in a single move");
            }
            if (sticks > RemainingSticks)
            {
                throw new ArgumentException($"You can't take more than remaining. Remains: {RemainingSticks}");
            }
            TakeSticks(sticks); //указываем, сколько палочек берёт человек
        }
        
        public void Start()
        {
            if (GameStatus == GameStatus.GameIsOver)
                RemainingSticks = InitialSticksNumber; //для перезапуска игры
            
            if (GameStatus == GameStatus.InProgress)
                throw new InvalidOperationException("Can't call start when the game is already in progress");
            
            GameStatus = GameStatus.InProgress;
            
            while( GameStatus == GameStatus.InProgress)
            {
                if (Turn == Player.Computer)
                {
                    ComputerMakesMove(); //Метод хода компьютера
                }
                else
                {
                    HumanMakesMove(); //Метод хода человека
                }

                FiredEndOfGameIfRequired(); //End Game - проверка на окончание прежде, чем передавать ход другому игроку

                Turn = Turn == Player.Computer ? Player.Human : Player.Computer; //инверсия хода - передача хода другому игроку
            }
        }

        private void FiredEndOfGameIfRequired() //конец игры - перевод в статус конца игры
        {
            if(RemainingSticks == 0)
            {
                GameStatus = GameStatus.GameIsOver;
                if (EndOfGame != null)
                    EndOfGame(Turn == Player.Computer ? Player.Human : Player.Computer); //Определяем, кто победил (?)
            }
        }

        private void HumanMakesMove()
        {
            if (HumanTurnToMakeMove != null)
                HumanTurnToMakeMove(this, RemainingSticks); // (?)
        }
        
        private void ComputerMakesMove()
        {
            int maxNumber = RemainingSticks >= 3 ? 3 : RemainingSticks;
            int sticks = randomizer.Next(1, maxNumber);

            TakeSticks(sticks); 
            
            if(MachinePlayed != null)
            {
                MachinePlayed(sticks); // (?)
            }
        }

        private void TakeSticks(int sticks)
        {
            RemainingSticks -= sticks;
        }
    }
}
