using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace DatabaseManager
{

    public class PlayerCreation
    {       
        public static void PlayerCreate(string GameName, string[] CurrentColors)
        {
            
            Console.WriteLine("Whats your name player?");
            var CurrentName = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Welcome " + CurrentName + " Whats your preferred color?");
            DrawColors(CurrentColors);
            
            var SelectedColor = Console.ReadLine();



            CurrentColors = ColorBan(SelectedColor, CurrentColors);

            if (CurrentColors[0] == "True")
            {
                Console.WriteLine("You have successfully chosen the color " + SelectedColor + "!");
            }
            else
            {
                Console.WriteLine("That is not a valid choice");
            }


            using var context = new LudoDbContext();
            var Retriver = context.games.FirstOrDefault(p => p.Name == GameName);
            var Id = Retriver.Id;
            var gamer = new Player
            {
                Name = CurrentName,
                Color = SelectedColor,
                Position = 0,
                GameId = Id
            };



            context.players.Add(gamer);
            context.SaveChanges();


            PlayerCreate(GameName, CurrentColors);
        }
        static string[] ColorBan(string SelectedColor, string[] subjects)
        {
            for (int i = 1; i < subjects.Length; i++)
            {
                if (subjects[i] == SelectedColor)
                {
                    subjects[i] = null;
                    subjects[0] = "True";
                    return subjects;
                }
            }
            subjects[0] = "False";
            return subjects;

        }
        public static void DrawColors(string[] CurrentColors)
        {
            for (int i = 1; i < CurrentColors.Length; i++)
            {
                var CurrentColor = CurrentColors[i];

                switch (CurrentColor)
                {
                    case "Red":
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(CurrentColor);
                        break;
                    case "Yellow":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(CurrentColor);
                        break;
                    case "Blue":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(CurrentColor);
                        break;
                    case "Green":
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(CurrentColor);
                        break;
                }

            }
            Console.ResetColor();
        }
    }
}
