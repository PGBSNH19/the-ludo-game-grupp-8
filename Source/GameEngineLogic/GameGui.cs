using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using DatabaseManager;

namespace GameEngineLogic
{



    public class GameGui
    {

        public void ShowStats(Game game , string Color)
        {
            var rows = new List<string>();

            foreach (var player in game.Players)
            {
                rows.Add($"Player: {player.Name} Color: {player.Color} Pawns in Base: {player.Pawns.Count(p => p.PawnState == Pawn.State.Base)}");
            }

            foreach (var row in rows)
            {
                Console.WriteLine(row);
            }
            Console.WriteLine($"Its {Color}'s turn!");

        }

        public void ShowBoard(Game game, string previousColor)
        {
            var board = new List<string>
            {
                "    ...    ",
                "    ...    ",
                "    ...    ",
                "    ...    ",
                "...........",
                "...........",
                "...........",
                "    ...    ",
                "    ...    ",
                "    ...    ",
                "    ...    ",
            };

            foreach (var player in game.Players)
            {
                foreach (var pawn in player.Pawns.Where(p => p.PawnState == Pawn.State.Playing))
                {
                   
                    if (player.MovementPattern.Count- 1 == pawn.Position)
                    {
                        pawn.PawnState = Pawn.State.Finished;
                    }
                    else
                    {
                        PlacePawnOnBoard(board, player.MovementPattern[pawn.Position], player.Color[0].ToString(), game, previousColor);
                    }

                }

            }

            foreach (var line in board)
            {
                Console.WriteLine(line);
            }

        }

        private void PlacePawnOnBoard(List<string> board, string position, string marker, Game game , string previousColor)
        {
            var letters = "ABCDEFGHIJK";
            var letterIndex = letters.IndexOf(position[0]);
            var index = Convert.ToInt32(position.Substring(1, position.Length - 1));
            var row = board[letterIndex];
            StringBuilder sb = new StringBuilder(row);
            var firstLetterColor = sb[index].ToString();
            if (sb[index] != '.' && firstLetterColor != marker && marker != previousColor[0].ToString())
            {
                RemovePawn(game, marker, position);
                
            }
            else
            {
                sb[index] = marker[0];
            }
            
            row = sb.ToString();
            board[letterIndex] = row.ToString();


        }
        public void RemovePawn(Game game, string firstLetterColor, string location)
        {
            var playerColor = "";
            switch(firstLetterColor)
            {
                case "R":
                    playerColor = "Red";
                    break;
                case "G":
                    playerColor = "Green";
                    break;
                case "B":
                    playerColor = "Blue";
                    break;
                case "Y":
                    playerColor = "Yellow";
                    break;
            }
 
            var player = game.Players.First(p => p.Color == playerColor);
            var pawns = player.Pawns;
            var movementPattern = player.MovementPattern;
            foreach (Pawn pawn in pawns)
            {
                var position = pawn.Position;

                if(movementPattern[pawn.Position] == location)
                {
                    pawn.PawnState = Pawn.State.Base;
                    pawn.Position = 0;
                }

            }
        }
        public void Clear()
        {
            Console.Clear();
        }

        public bool CheckFinishedStatus(Game game)
        {
            var players = game.Players;
            foreach (var player in players)
            {
                var pawnsFinished = 0;
                foreach(var pawn in player.Pawns)
                {
                    if(pawn.PawnState == Pawn.State.Finished)
                    {
                        pawnsFinished++;
                    }                   
                }

                if (pawnsFinished == 4)
                {
                    player.Won = true;
                    return true;
                }

            }

            return false;
        }

        public Pawn RollDiceNextPlayer(LudoDbContext context, Game game, string color)
        {
            var currentPlayer = game.Players.First(p => p.Color == color);
            var Bot = false;
            if (currentPlayer.Bot)
            {
                Bot = true;
            }
            var pawn = new Pawn();
            Console.WriteLine("Press any key for next dice roll!");
            var RandomNumber = new Random();
            var r = 0;
            if(Bot)
            {
                r = RandomNumber.Next(0, 7);
            }
            else
            {
                r = VisualWidgets.MainFunction();
                
                if (r == 0)
                {
                    r = 1;
                }
            }
            Thread.Sleep(500);
            Console.WriteLine($"You got {r}");
            
          
            var currentPawns = currentPlayer.Pawns;
            var currentPawnsInBase = currentPawns.Where(p => p.PawnState == Pawn.State.Base);
            var CurrentPawnsInPlay = currentPawns.Where(p => p.PawnState == Pawn.State.Playing);
            

            if (r == 6 || r == 1 && currentPawnsInBase.Any())
            {
                
                List<string> Options = new List<string>();
                var Choice = "";
                if (r == 1)
                {
                    if (!CurrentPawnsInPlay.Any())
                    {
                        MovePawnOutOfNest(currentPawns);
                    }
                    else
                    {                       
                        Options.AddRange(new string[] { "Move one pawn out of the nest", "Move an existing pawn" });
                        if (Bot)
                        {
                            Choice = Randomizer.ListRandomizer(Options);
                            Console.WriteLine(Choice);
                        }
                        else
                        {
                            Console.WriteLine("The Bot will: \n" + Choice);
                            Choice = MenuNavigator.Menu.ShowMenu(Options);
                        }
                        
                        
                    }
                    
                }
                else
                {          
                    if (currentPawnsInBase.Any())
                    {
                        Options.Add("Move one pawn out of the nest and 6 steps");
                        if (currentPawnsInBase.Count() > 1)
                        {
                            Options.Add("Move two pawns out of the nest");
                        }
                       
                    }                   
                    if (CurrentPawnsInPlay.Any())
                    {
                        Options.Add("Move an existing pawn");
                    }
                    
                    if (Bot)
                    {
                        Choice = Randomizer.ListRandomizer(Options);
                        Console.WriteLine("The Bot will: \n" + Choice);
                    }
                    else
                    {
                        Console.WriteLine("What Would you like to do?");
                        Choice = MenuNavigator.Menu.ShowMenu(Options);
                    }
                }
                switch (Choice)
                {
                    case "Move two pawns out of the nest":
                        MovePawnOutOfNest(currentPawns);
                        pawn = MovePawnOutOfNest(currentPawns);
                        break;
                    case "Move one pawn out of the nest and 6 steps":
                        pawn = MovePawnOutOfNest(currentPawns);
                        MovePlayingPawn(pawn, r);
                        break;
                    case "Move an existing pawn":
                        DeterminePawn(currentPawns);
                        break;
                    case "Move one pawn out of the nest":
                        MovePawnOutOfNest(currentPawns);
                        break;
                }
            }
            else if(CurrentPawnsInPlay.Any())
            {
                DeterminePawn(currentPawns);
            }

            Pawn MovePawnOutOfNest(IEnumerable<Pawn> pawns)
            {
               pawn = pawns.First(p => p.PawnState == Pawn.State.Base);
               pawn.PawnState = Pawn.State.Playing;
               pawn.Position = 0;
               return pawn;

            }

            Pawn DeterminePawn(IEnumerable<Pawn> pawns)
            {
                var pawn = new Pawn();
                var playingPawns = pawns.Where(p => p.PawnState == Pawn.State.Playing);
                if (playingPawns.Count() == 1)
                {
                    pawn = pawns.First(p => p.PawnState == Pawn.State.Playing);
                }
                else
                {
                     
                    var pawnsPlaying = playingPawns.ToList();
                    List<string> pawnPositions = new List<string>();
                    for (int i = 0; playingPawns.Count() > i; i++)
                    {
                        pawnPositions.Add(pawnsPlaying[i].Position.ToString());
                    }
                    var choice = "";
                    if (Bot)
                    {
                        
                        choice = Randomizer.ListRandomizer(pawnPositions);
                        Console.WriteLine("The bot will move the pawn at position: " + choice);
                    }
                    else
                    {
                        Console.WriteLine("Which Pawn do you want to move?");
                        choice = MenuNavigator.Menu.ShowMenu(pawnPositions);
                    }
                    

                    pawn = pawns.First(p => p.Position == Int32.Parse(choice));
                }

                return MovePlayingPawn(pawn, r);

            }
            Console.ReadLine();
            return pawn;
        }
            Pawn MovePlayingPawn(Pawn pawn, int DiceRoll)
            {
                pawn.Position += DiceRoll;
                if (pawn.Position > 44)
                {
                    var backTrack = pawn.Position - 44;
                    pawn.Position = 44 - backTrack;

                }
                return pawn;
            }          

    }
}
