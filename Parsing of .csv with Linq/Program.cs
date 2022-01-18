using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Parsing_of_.csv_with_Linq
{

    class Program
    {
        static void Main(string[] args)
        {
            var players = File.ReadAllLines("chess.csv")
                        .Skip(1)
                        .Select(x=>ChessPlayer.ParsedFideCsv(x))
                        .Where(player=>player.Country == "RUS")
                        .OrderBy(player=>player.BirthYear)
                        .ToList();
            foreach (var item in players)
            {
                Console.WriteLine(item);
            }
        }
        static void LinqDemo(string file)
        {
            var lines = File.ReadAllLines(file);
            IEnumerable<ChessPlayer> list = lines
                                            .Skip(1)
                                            .Select(ChessPlayer.ParsedFideCsv)
                                            //old-style anonymous method syntax
                                            //.Where(delegate(ChessPlayer player){return player.BirthYear > 1988;});
                                            .Where(player => player.BirthYear > 1988)
                                            .OrderByDescending(player => player.Rating)
                                            .ThenBy(player => player.Country)
                                            .Take(10)
                                            .ToList();
            IEnumerable<ChessPlayer> list2 = lines
                                           .Skip(1)
                                           .Select(ChessPlayer.ParsedFideCsv);
            
            //sql-like syntax
            //var filtered = from player in list2 
            //               where player.BirthYear > 1988 
            //               orderby player.Rating descending 
            //               select player; 


            Console.WriteLine($"The lowest rating in top 10 = {list.Min(x => x.Rating)}");
            Console.WriteLine($"The higest rating in top 10 = {list.Max(x => x.Rating)}");
            Console.WriteLine($"The average rating in top 10 = {list.Average(x => x.Rating)}");
            
            Console.WriteLine(list.First());
            Console.WriteLine(list.Last());

            Console.WriteLine(list.First(player=>player.Country == "USA"));
            Console.WriteLine(list.Last(player => player.Country == "USA"));

            
            // чтобы избежать исключений
            Console.ReadLine();
            ChessPlayer firstFromBrasil = list.FirstOrDefault(player=>player.Country == "BRA");
            if (firstFromBrasil != null)
            {
                Console.WriteLine(firstFromBrasil.LastName);
            }    
            ChessPlayer lastFromBrasil = list.LastOrDefault(player => player.Country == "BRA");
            if (lastFromBrasil != null)
            {
                Console.WriteLine(lastFromBrasil.LastName);
            }
            
            Console.ReadLine();
            Console.WriteLine(list.SingleOrDefault(player=>player.Country == "FRA"));
            //Console.WriteLine(list.SingleOrDefault(player => player.Country == "USA")); -  исключение из-за того, что их много
        }
    }
}
