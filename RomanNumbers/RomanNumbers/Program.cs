using System;

namespace RomanNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            RomanNumber romanNumber1 = new RomanNumber("MC");
            RomanNumber romanNumber2 = new RomanNumber("MC");
            var res = romanNumber1 + romanNumber2;
            
            Console.WriteLine(res);
            Console.WriteLine(romanNumber1.ToArabicSystem() + romanNumber2.ToArabicSystem());
            Console.WriteLine(RomanNumber.ToRomanSystem(2200));
        }
    }
}
