using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        Game game = new Game();
        Console.WriteLine("Welcome To Tic Tac Toe Game!");
        Console.Write("Press Enter to start the game");
        Console.ReadLine();
        game.Start();
        Console.ReadLine();
    }
}
