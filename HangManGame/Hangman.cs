using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace HangManGame
{
    class Hangman
    {
        private readonly string word;
        private int mistakes;
        private string _alreadyUsedLetters = "";
        private bool gameOn = true;

        public Hangman()
        {
            word = GenerateWord();
            this.mistakes = 8;
        }

        private string GenerateWord()
        {
            string[] words = File.ReadAllLines("WordsStockRus.txt");
            Random r = new Random(DateTime.Now.Millisecond);
            var chosenWord = words[r.Next(0,words.Length-1)];
            if (!chosenWord.Any(char.IsLetter) && chosenWord.Any(char.IsWhiteSpace))
            {
                GenerateWord();
            }
            return chosenWord;
        }
        private bool isRightGuessed(char letter, out List<int> pos)
        {
            var flag = false;
            pos = new List<int>();
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == letter)
                {
                    flag = true;
                    pos.Add(i);
                }
            }
            return flag;
        }
        private void prettyPrint(string s)
        {
            foreach(var elem in s)
            {
                Console.Write(elem + " ");
            }
            Console.Write($"({s.Length}) букв");
            Console.WriteLine();
        }
        public void Game()
        {
            //Console.WriteLine(word);
            var res = "";
            var winState = false;
            foreach (var elem in word)
            {
                res += "_";
            }
            prettyPrint(res);
            
            while (mistakes != 0 && gameOn)
            {
                char userAnswer = new char();
                Console.WriteLine("Введите букву>");
                while (!char.TryParse(Console.ReadLine(), out userAnswer))
                {
                    Console.WriteLine("Введите букву>");
                    prettyPrint(res);
                }

                List<int> buffer;
                if (!_alreadyUsedLetters.Contains(userAnswer))
                {
                    if (isRightGuessed(userAnswer, out buffer))
                    {
                        _alreadyUsedLetters += userAnswer;
                        for (int i = 0; i < buffer.Count; i++)
                        {
                            res = res.Insert(buffer[i], userAnswer.ToString());
                            res = res.Remove(buffer[i] + 1, 1);
                        }
                        prettyPrint(res);
                    }
                    else
                    {
                        Console.WriteLine("В слове нет такой буквы!");
                        mistakes--;
                    }
                }
                else
                {
                    Console.WriteLine("Эта буква уже была!");
                }

                if (!res.Contains("_"))
                {
                    gameOn = false;
                    winState = true;
                    Console.WriteLine("Слово угадано!");
                }
            }

            if (!winState)
            {
                Console.WriteLine($"Загаданное слово было таким: {word}");
            }
        }
    }
}
