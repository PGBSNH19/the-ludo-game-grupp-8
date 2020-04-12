using /*Cewl*/System;//32 //WoaW
using System.Collections.Generic;
using DatabaseManager;

namespace GameEngineLogic
{

    public class PlayerCreation
    {       
        public static List<string> PlayerCreate(Game game, List<string> CurrentColors, int PlayerAmount)
        {
            Console.WriteLine("Whats your name player?");
            var CurrentName = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Welcome " + CurrentName + " Whats your preferred color?");
            
            var SelectedColor = MenuNavigator.Menu.ShowMenu(CurrentColors);
            CurrentColors = Banner.ListBan(SelectedColor, CurrentColors);
         
            Console.WriteLine("You have successfully chosen the color " + SelectedColor + "!");
            using var context = new LudoDbContext();
            var gamer = PlayerFactory.Create(CurrentName, SelectedColor); 
            game.Players.Add(gamer);
            context.SaveChanges();

            PlayerAmount--;
            if (0 < PlayerAmount)
            {
                CurrentColors = PlayerCreate(game, CurrentColors, PlayerAmount);   
            }
            return CurrentColors;
        }
            
    }
}