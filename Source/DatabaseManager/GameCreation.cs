using System;
using System.Collections.Generic;
using System.Text;


namespace DatabaseManager
{
    public class GameCreation
    {
        public static void GameCreate()
        {
            using var context = new LudoDbContext();
            Console.WriteLine("What do you want your session to be called?");
            var GameName = Console.ReadLine();
            var game = new Game
            {
                Name = GameName,
                CreationTime = DateTime.Now,
                GameEnded = false,
                Turn = 1,
            };

            context.games.Add(game);
            context.SaveChanges();

            string[] CurrentColors = { "false", "Red", "Blue", "Yellow", "Green" };
            PlayerCreation.PlayerCreate(GameName, CurrentColors);

        }
    }
}
