using System;
using System.Collections.Generic;
using System.Drawing;
using TicTacToe.NewFolder;

namespace DevConsole {
    class Program {

        static void Main(string[] args) {
            Console.WriteLine("Hello World!");

            Dictionary<string, object> data = new Dictionary<string, object>() {
                { "LastName" , "Ich" },
                { "FirstName", "Du"},
                { "Address", "Wo?"}
            };

            Class1 o = new Class1();

            DbColAttr.SetValues<Class1>(o, data);

            Console.ReadKey();

            //using (TicTacToe.Toe myToe = new TicTacToe.Toe()) {

            //    while(!myToe.exit) {
            //        Point result = myToe.SelectCoordinates();
            //        myToe.SetField(result, myToe.NextPlayer());
            //    }

            //    TicTacToe.NewFolder.Class2 obj = new TicTacToe.NewFolder.Class2();
            //    obj.bla();

            //}

            Console.ReadLine();
        }

        [System.Diagnostics.DebuggerHidden]
        static void test() {
            Console.WriteLine("Click");
            Console.WriteLine("Click");
            Console.WriteLine("Click");
            Console.WriteLine("Click");
            Console.WriteLine("Click");
            Console.WriteLine("Click");

        }

        private static void Click_Obj(object sender, EventArgs e) {
            Console.WriteLine("Click");
        }

    }
}
