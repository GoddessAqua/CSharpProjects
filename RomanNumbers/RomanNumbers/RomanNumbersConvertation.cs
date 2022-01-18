using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;

namespace RomanNumbers
{
    class RomanNumber
    {
        private readonly string _romanNumber;
        private static readonly Dictionary<char, int> _dict = new Dictionary<char, int>
        {
            ['I'] = 1,
            ['V'] = 5,
            ['X'] = 10,
            ['L'] = 50,
            ['C'] = 100,
            ['D'] = 500,
            ['M'] = 1000
        };
        private static readonly Dictionary<string, int> _extDict = new Dictionary<string, int>
        {
            ["I"] = 1,
            ["II"] = 2,
            ["III"] = 3,
            ["IV"] = 4,
            ["V"] = 5,
            ["VI"] = 6,
            ["VII"] = 7,
            ["VIII"] = 8,
            ["IX"] = 9,
            ["X"] = 10,
            ["XX"] = 20,
            ["XXX"] = 30,
            ["XL"] = 40,
            ["L"] = 50,
            ["LX"] = 60,
            ["LXX"] = 70,
            ["LXXX"] = 80,
            ["XC"] = 90,
            ["C"] = 100,
            ["CC"] = 200,
            ["CCC"] = 300,
            ["CD"] = 400,
            ["D"] = 500,
            ["DC"] = 600,
            ["DCC"] = 700,
            ["DCCC"] = 800,
            ["CM"] = 900,
            ["M"] = 1000,
            ["MM"] = 2000,
            ["MMM"] = 3000
        };
        
        public RomanNumber(string number)
        {
            _romanNumber = number;
        }
        public int ToArabicSystem()
        {
            int res = 0;
            for (int i = 0; i < _romanNumber.Length; i++)
            {
                if (i + 1 < _romanNumber.Length && _romanNumber[i] < _romanNumber[i + 1])
                {
                    res -= _dict[_romanNumber[i]];
                }
                else
                {
                    res += _dict[_romanNumber[i]];
                }
            }
            return res;
        }
        public static string ToRomanSystem(int number)
        {
            var _reversed = _extDict.ToDictionary(x => x.Value, x => x.Key);
            var mod = 0;
            var step = 1;
            var res = "";
            var myStack = new Stack<string>();
            
            while (number != 0)
            {
                mod = number % 10;
                if (_reversed.ContainsKey(step*mod))
                {
                    myStack.Push(_reversed[step * mod]);
                }
                else
                {
                    myStack.Push("");
                }
                number /= 10;
                step *= 10;
            }

            return FromStackToString(ref myStack);
        }
        private static string FromStackToString(ref Stack<string> myStack)
        {
            var res = "";
            foreach(string elem in myStack)
            {
                res += elem;
            }
            return res;
        }
        public static string operator +(RomanNumber f, RomanNumber s) => ToRomanSystem(f.ToArabicSystem() + s.ToArabicSystem());
        public static string operator -(RomanNumber f, RomanNumber s) => ToRomanSystem(f.ToArabicSystem() - s.ToArabicSystem());
        public static string operator *(RomanNumber f, RomanNumber s) => ToRomanSystem(f.ToArabicSystem() * s.ToArabicSystem());
        public static string operator /(RomanNumber f, RomanNumber s) => ToRomanSystem(f.ToArabicSystem() / s.ToArabicSystem());
    }
}
