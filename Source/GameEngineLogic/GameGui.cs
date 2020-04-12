using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseManager;
using Microsoft.EntityFrameworkCore.Internal;

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

        public void ShowBoard(Game game)
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
                        PlacePawnOnBoard(board, player.MovementPattern[pawn.Position], player.Color[0].ToString());
                    }

                }

            }

            foreach (var line in board)
            {
                Console.WriteLine(line);
            }

        }

        private void PlacePawnOnBoard(List<string> board, string position, string marker)
        {
            var letters = "ABCDEFGHIJK";
            var letterIndex = letters.IndexOf(position[0]);
            var index = Convert.ToInt32(position.Substring(1, position.Length - 1));


            var row = board[letterIndex];
            StringBuilder sb = new StringBuilder(row);
            sb[index] = marker[0];
            row = sb.ToString();
            var Hej = row.ToString();
            board[letterIndex] = row.ToString();


        }

        public void Clear()
        {
            Console.Clear();
        }

        public bool CheckFinishedStatus(Game game)
        {
            foreach (var player in game.Players)
            {
                if (player.Won)
                {
                    return true;
                }
            }

            return false;
        }

        public void RollDiceNextPlayer(Game game, string Color)
        {
            Console.WriteLine("Press any key for next dice roll!");
            var RandomNumber = new Random();
            int r = RandomNumber.Next(1, 7);
            Console.WriteLine($"You got {r}");
            var currentPlayer = game.Players.First(p => p.Color == Color);
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
                        Console.WriteLine("What Would you like to do?");
                        Options.AddRange(new string[] { "Move one pawn out of the nest", "Move an existing pawn" });
                        Choice = MenuNavigator.Menu.ShowMenu(Options);
                    }
                    
                }
                else
                {
                    Console.WriteLine("What Would you like to do?");
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

                    Choice = MenuNavigator.Menu.ShowMenu(Options);
                }
                switch (Choice)
                {
                    case "Move two pawns out of the nest":
                        MovePawnOutOfNest(currentPawns);
                        MovePawnOutOfNest(currentPawns);
                        break;
                    case "Move one pawn out of the nest and 6 steps":
                        var pawn = MovePawnOutOfNest(currentPawns);
                        MovePlayingPawn(pawn, r);
                        break;
                    case "Move an existing pawn":
                        DeterminePawn(currentPawns);
                        break;
                }
            }
            else if(CurrentPawnsInPlay.Any())
            {
                DeterminePawn(currentPawns);
            }

            Pawn MovePawnOutOfNest(IEnumerable<Pawn> pawns)
            {
               var pawn = pawns.First(p => p.PawnState == Pawn.State.Base);
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
                    Console.WriteLine("Which Pawn do you want to move?");
                    var pawnsPlaying = playingPawns.ToList();
                    List<string> pawnPositions = new List<string>();
                    for (int i = 0; playingPawns.Count() > i; i++)
                    {
                        pawnPositions.Add(pawnsPlaying[i].Position.ToString());
                    }

                    var Choice = MenuNavigator.Menu.ShowMenu(pawnPositions);
                    pawn = pawns.First(p => p.Position == Int32.Parse(Choice));
                }

                return MovePlayingPawn(pawn, r);

            }
            Console.ReadLine();
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

            void Knuff(Pawn pawn)
            {

            }

    }
}
