using System;
using System.Linq.Expressions;
using System.Numerics;

namespace TicTacToe
{
    public class Toe
    {
        public static void InitializeGame()
        {
            Console.Write("Input your name \n");
            string name = Console.ReadLine();
            Console.WriteLine("Hello " + name);
        }

        public static Vector2 SetCoordinates()
        {
            Console.WriteLine("Put in your wanted coordinates (whole numbers between 1 and 3): ");
            Console.Write("X coord = ");
            string posXString = Console.ReadLine();
            int posX = 0;
            Int32.TryParse(posXString, out posX);
            Console.Write("And Y coord = ");
            string posYString = Console.ReadLine();
            int posY = 0;
            Int32.TryParse(posYString, out posY);

            if (posX > 3 || posY > 3 || posX < 1 || posY < 1)
            {
                Console.WriteLine("The input does not match the grid. Please try again. ");
                
                SetCoordinates();
            }
            return new Vector2(x: posX, y: posY);
        }

        private static void CreateField()
        {
            /*
             Function creates 9 fields which can later be
             replaced with eiter X or O during the play. 
             --Deprecated
            */
            int size = 5;
            for (int i = 0; i <= size; i++)
            {
                for (int j = 0; j <= size; j++)
                {
                    bool evenY = (i % 2 == 0);
                    bool evenX = (j % 2 == 0);
                    switch (evenY)
                    {
                        case true when evenX:
                            Console.Write("| ");
                            break;
                        case true when !evenX:
                            Console.Write("___");
                            break;
                    }
                }
                Console.Write("\n");
                
            }
        }

        public static void drawFieldGrid(Vector2 vect, int player)
        {
            /*
             * Method draws X or O's at by vector determined
             * positions.
             * TODO Vector2 should be an Array with positions from the player,
             * TODO which gets updated every round
             * TODO then another Array should be created and updated every turn
             * TODO for the NPC turns
             */
            string sign = " ";
            if (player == 0)
            {
                sign = " X ";
            }
            else
            {
                sign = " O ";
            }
            for (int y = 1; y <= 3; y++)
            {
                for (int x = 1; x <= 3; x++)
                {
                    if (Convert.ToInt32(vect.X) == x &
                        Convert.ToInt32(vect.Y) == y)
                    {
                        Console.Write(sign);
                    }
                    else
                    {
                        Console.Write(" _ ");
                    }
                }
                Console.Write("\n");
            }
        }
    }
}