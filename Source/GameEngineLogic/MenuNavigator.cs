using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngineLogic
{
    public class MenuNavigator
    {
        public class Menu
        {
            public static string ShowMenu(List<string> options)
            {
                int selected = 0;

                // Hide the cursor that will blink after calling ReadKey.
                Console.CursorVisible = false;

                ConsoleKey? key = null;
                while (key != ConsoleKey.Enter)
                {
                    // If this is not the first iteration, move the cursor to the first line of the menu.
                    if (key != null)
                    {
                        Console.CursorLeft = 0;
                        Console.CursorTop = Console.CursorTop - options.Count;
                    }

                    // Print all the options, highlighting the selected one.
                    for (int i = 0; i < options.Count; i++)
                    {
                        var option = options[i];
                        if (i == selected)
                        {
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.WriteLine("- " + option);
                        Console.ResetColor();
                    }

                    // Read another key and adjust the selected value before looping to repeat all of this.
                    key = Console.ReadKey().Key;
                    if (key == ConsoleKey.DownArrow)
                    {
                        selected = Math.Min(selected + 1, options.Count - 1);
                    }
                    else if (key == ConsoleKey.UpArrow)
                    {
                        selected = Math.Max(selected - 1, 0);
                    }
                }

                // Reset the cursor and run the selected option.
                Console.CursorVisible = true;
                //Return Value
                return (options[selected]);
            }
        }
    }
}
