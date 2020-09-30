using System;
using System.Drawing;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Runtime.InteropServices;

namespace TicTacToe {

    public class Toe :IDisposable {

        #region Consts
        public const int WIDTH = 3;
        public const int HEIGHT = 3;
        public const int SIZE = 3;
        #endregion

        public enum Player {
            None,
            Circle,
            Cross
        }

        private Player currentPlayer = Player.Circle;
        private Point selection = new Point(0, 0);
        private Player[,] field = new Player[3, 3];

        private bool _exit = false;
        public bool exit {
            get {
                return _exit;
            }
        }

        public Toe() {
            for(int x = 0; x < WIDTH; x++) {
                for(int y = 0; y < HEIGHT; y++) {
                    field[x, y] = Player.None;
                }
            }
            InitializeGame();
        }

        private void InitializeGame() {
            Console.Write("Input your name \n");
            string name = Console.ReadLine();
            Console.WriteLine("Hello " + name);
            Console.Clear();
            Console.CursorVisible = false;
        }

        Player checkWinner() {
            throw new NotImplementedException();
        }

        void Reset() {
            throw new NotImplementedException();
        }

        void DisplayWinner() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Lässt den User ein Feld selectieren.
        /// </summary>
        /// <returns></returns>
        public Point SelectCoordinates() {
            ConsoleKeyInfo input = new ConsoleKeyInfo('x', ConsoleKey.L, true, true, true);
            while ((input.Key != ConsoleKey.Enter) || (field[selection.X, selection.Y] != Player.None)) {
                Print();

                input = Console.ReadKey(true);

                switch(input.Key) {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                    case ConsoleKey.K:
                        selection.Y -= 1;
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                    case ConsoleKey.J:
                        selection.Y += 1;
                        break;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                    case ConsoleKey.H:
                        selection.X -= 1;
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                    case ConsoleKey.L:
                        selection.X += 1;
                        break;
                    case ConsoleKey.Q:
                        _exit = true;
                        return selection;
                }

                if(selection.X < 0)
                    selection.X += 3;
                if(selection.Y < 0)
                    selection.Y += 3;
                if(selection.X >= 3)
                    selection.X %= 3;
                if(selection.Y >= 3)
                    selection.Y %= 3;
            }

            return selection;
        }

        public void Print() {
            Console.CursorTop = 0;
            Console.CursorLeft = 0;

            for(int y = 0; y < HEIGHT; y++) {
                if(y == 0) {
                    line('┌', '─', '┬', '┐', WIDTH, SIZE);
                } else {
                    line('├', '─', '┼', '┤', WIDTH, SIZE);
                }
                for(int i = 0; i < SIZE; i++) {
                    line('│', ' ', '│', '│', WIDTH, SIZE);
                }
            }
            line('└', '─', '┴', '┘', WIDTH, SIZE);

            for(int x = 0; x < WIDTH; x++) {
                for(int y = 0; y < HEIGHT; y++) {
                    int top = (y * (SIZE + 1)) + (int)Math.Floor((double)(SIZE / 2)) + 1;
                    int left = (x * (SIZE + 1)) + (int)Math.Floor((double)(SIZE / 2)) + 1;
                    Console.CursorTop = top;
                    Console.CursorLeft = left;
                    ConsoleColor startColor = Console.ForegroundColor;
                    ConsoleColor startBgColor = Console.BackgroundColor;
                    if (selection.X == x && selection.Y == y) {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                    } else {
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    switch(field[x,y]) {
                        case Player.None:
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(" ");
                            break;
                        case Player.Circle:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("O");
                            break;
                        case Player.Cross:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("X");
                            break;
                    }
                    Console.ForegroundColor = startColor;
                    Console.BackgroundColor = startBgColor;
                }
            }
        }

        public void SetField(Point target, Player player) {
            field[target.X, target.Y] = player;
        }

        public Player NextPlayer() {
            currentPlayer = (currentPlayer == Player.Circle) ? Player.Cross : Player.Circle;
            return currentPlayer;
        }

        private static void line(char start, char middle, char cross, char end, int length, int size) {
            for(int i = 0; i < length; i++) {
                char first = cross;
                if(i == 0)
                    first = start;
                Console.Write(first + "".PadLeft(size, middle));
            }
            Console.WriteLine(end);
        }

        public static Vector2 SetCoordinates() {
            Console.WriteLine("Put in your wanted coordinates (whole numbers between 1 and 3): ");
            Console.Write("X coord = ");
            string posXString = Console.ReadLine();
            int posX = 0;
            Int32.TryParse(posXString, out posX);
            Console.Write("And Y coord = ");
            string posYString = Console.ReadLine();
            int posY = 0;
            Int32.TryParse(posYString, out posY);

            if(posX > 3 || posY > 3 || posX < 1 || posY < 1) {
                Console.WriteLine("The input does not match the grid. Please try again. ");

                SetCoordinates();
            }
            return new Vector2(x: posX, y: posY);
        }

        private static void CreateField() {
            /*
             Function creates 9 fields which can later be
             replaced with eiter X or O during the play. 
             --Deprecated
            */
            int size = 5;
            for(int i = 0; i <= size; i++) {
                for(int j = 0; j <= size; j++) {
                    bool evenY = (i % 2 == 0);
                    bool evenX = (j % 2 == 0);
                    switch(evenY) {
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

        public static void drawFieldGrid(Vector2 vect, int player) {
            /*
             * Method draws X or O's at by vector determined
             * positions.
             * TODO Vector2 should be an Array with positions from the player,
             * TODO which gets updated every round
             * TODO then another Array should be created and updated every turn
             * TODO for the NPC turns
             */
            string sign = " ";
            if(player == 0) {
                sign = " X ";
            } else {
                sign = " O ";
            }
            for(int y = 1; y <= 3; y++) {
                for(int x = 1; x <= 3; x++) {
                    if(Convert.ToInt32(vect.X) == x &
                        Convert.ToInt32(vect.Y) == y) {
                        Console.Write(sign);
                    } else {
                        Console.Write(" _ ");
                    }
                }
                Console.Write("\n");
            }
        }

        public void Dispose() {
            Console.Clear();
        }
    }
}