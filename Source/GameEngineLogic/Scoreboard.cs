using DatabaseManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GameEngineLogic
{
    public class Scoreboard
    {
        public static void CreateScoreboard()
        {
            List<string> Values = new List<string>();
            Values.Add("1");
            Values.Add("0");

            using var context = new LudoDbContext();
            List<Player> Players = context.players.ToList();
            var results = Players.GroupBy(p => p.Name)
                          .Select(grp => grp.First())
                          .ToList();

            for (int i = 0; i < results.Count; i++)
            {
                
                string CurrentName = results[i].Name;
                AddCorrespondingNames(Values, CurrentName);       
                var Wins = Int32.Parse(Values[1]);
                var Games = Int32.Parse(Values[0]);               
                var Losses = Math.Max((Games - Wins), 1);
                decimal WinRate;
                
                WinRate = (decimal)Wins / Games;  
     
                WinRate = decimal.Round(WinRate, 2);
                Values[1] = "0";
                Values[0] = "0";
                Console.WriteLine(i + 1 + ".");
                Console.WriteLine("Name:" + CurrentName);
                Console.WriteLine("Games:" + Games);
                Console.WriteLine("Wins:" + Wins);
                Console.WriteLine("Losses:" + Losses); 
                Console.WriteLine("WinRate:" + WinRate);


            }
        }
        public static List<String> AddCorrespondingNames(List<String> Values, string CurrentName)
        {
            int Games = 0;
            int Wins = 0;
            using var context = new LudoDbContext();
            List<Player> Players = context.players.ToList();
            
            if (!Values.Contains(CurrentName))
            {
                for (int j = 0; j < Players.Count; j++)
                {
                    if (CurrentName == Players[j].Name)
                    {
                        Games++;

                        if (Players[j].Won == true)
                        {
                            Wins++;
                        }

                    }
                }
                Values.Add(CurrentName);
                Values[0] = Games.ToString();
                Values[1] = Wins.ToString();
                
            }
            return Values;
        }
    }
}
/*
1.
Name:
Games:
Wins:
Losses:
WinRate:
2.
*/