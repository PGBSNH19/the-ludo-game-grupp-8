using System;
using System.Collections.Generic;
using DatabaseManager;

namespace GameEngineLogic
{
    public class PlayerFactory
    {
        public static Player Create(string name, string color, bool bot)
        {
            var pattern = CreateMovementPattern(color);

            var player = new Player
            {
                Name = name,
                Color = color,
                Won = false,
                Bot = bot,
                MovementPattern = pattern,
                Pawns = CreatePawns(color)
            };
            return player;
        }

        private static ICollection<Pawn> CreatePawns(string color)
        {
            var pawns = new List<Pawn>();

            for (int i = 0; i < 4; i++)
            {
                pawns.Add(new Pawn
                {
                    PawnState = Pawn.State.Base,
                    Position = 0
                });
            }

            return pawns;
        }

        private static List<string> CreateMovementPattern(string color)
        {
            switch (color)
            {
                case "Red":
                    return new List<string>
                    {
                        "A6", "B6", "C6", "D6", "E6", "E7", "E8", "E9", "E10", "F10", "G10", "G9", "G8", "G7", "G6", "H6", "I6", "J6", "K6", "K5", "K4", "J4",
                        "I4", "H4", "G4", "G3", "G2", "G1", "G0", "F0", "E0", "E1", "E2", "E3", "E4", "D4", "C4", "B4", "A4", "A5", "B5", "C5", "D5", "E5", "F5",
                    };
                case "Green":
                    return new List<string>
                    {
                        "G10", "G9", "G8", "G7", "G6", "H6", "I6", "J6", "K6", "K5", "K4", "J4", "I4", "H4", "G4", "G3", "G2", "G1", "G0", "F0", "E0", "E1",
                        "E2", "E3", "E4", "D4", "C4", "B4", "A4", "A5", "A6", "B6", "C6", "D6", "E6", "E7", "E8", "E9", "E10", "F10", "F9", "F8", "F7", "F6", "F5"
                    };
                case "Blue":
                    return new List<string>
                    {
                        "E0", "E1", "E2", "E3", "E4", "D4", "C4", "B4", "A4", "A5", "A6", "B6", "C6", "D6", "E6", "E7", "E8", "E9", "E10", "F10", "G10", "G9",
                        "G8", "G7", "G6", "H6", "I6", "J6", "K6", "K5", "K4", "J4", "I4", "H4", "G4", "G3", "G2", "G1", "G0", "F0", "F1", "F2", "F3", "F4", "F5"
                    };
                case "Yellow":
                    return new List<string>
                    {
                        "K4", "J4", "I4", "H4", "G4", "G3", "G2", "G1", "G0", "F0", "E0", "E1", "E2", "E3", "E4", "D4", "C4", "B4", "A4", "A5", "A6", "B6", "C6",
                        "D6", "E6", "E7", "E8", "E9", "E10", "F10", "G10", "G9", "G8", "G7", "G6", "H6", "I6", "J6", "K6", "K5", "J5", "I5", "H5", "G5", "F5"
                    };
                default: throw new ArgumentException(nameof(color));

            }
        }
    }
}
