using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proj_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Slots Player = new Slots();

            while (Player.CurrentBalance > 0)
            {
                Console.WriteLine($"ваш баланс: {Player.CurrentBalance}");
                Console.WriteLine("ваша ставка:");
                int rate = Convert.ToInt32(Console.ReadLine());
                if (rate <= Player.CurrentBalance)
                {

                    Player.MakeRate(rate);
                    Player.Rotate();
                    Player.CountResult();
                    Console.WriteLine($"ваш текущий баланс: {Player.CurrentBalance}");
                    if (Player.CurrentBalance > 0)
                    {
                        Console.WriteLine("будете еще играть? если да, введите 1, если нет 0");
                        int a = Convert.ToInt32(Console.ReadLine());
                        if (a == 0)
                        {
                            Player.TotalCount();
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Нельзя ставить больше, чем есть у вас");
                }
            }
        }
    }
}
