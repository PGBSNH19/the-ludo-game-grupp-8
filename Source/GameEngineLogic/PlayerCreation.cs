using /*Cewl*/System;//32 //WoaW
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DatabaseManager;

namespace GameEngineLogic
{

    public class PlayerCreation
    {       
        public static List<string> PlayerCreate(string GameName, List<String> CurrentColors, int PlayerAmount)
        {
        

            Console.WriteLine("Whats your name player?");
            var CurrentName = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Welcome " + CurrentName + " Whats your preferred color?");
            
            var SelectedColor = MenuNavigator.Menu.ShowMenu(CurrentColors);
            CurrentColors = ColorBan(SelectedColor, CurrentColors);
         
            Console.WriteLine("You have successfully chosen the color " + SelectedColor + "!");
            using var context = new LudoDbContext();
            var Retriver = context.games.FirstOrDefault(p => p.Name == GameName);
            var gamer = new Player
            {
                Name = CurrentName,
                Color = SelectedColor,
                Position = 0,
                Game = Retriver,
                Won = false
            };

            context.players.Add(gamer);
            context.SaveChanges();

            PlayerAmount--;
            if (0 < PlayerAmount)
            {
                
                PlayerCreate(GameName, CurrentColors, PlayerAmount);   
            }
            return CurrentColors;
        }
        static List<string> ColorBan(string SelectedColor, List<string> subjects)
        {
            for (int i = 0; i < subjects.Count; i++)
            {
                if (subjects[i] == SelectedColor)
                {
                    subjects.RemoveAt(i);
                    return subjects;
                }
            }
            return subjects;

        }
       
    }
}

//sebjects.RemoveAt(X)