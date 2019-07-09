using System;
using System.Collections.Generic;

public class Game
{
    private int[] _data = new int[9];

    public Game()
    {
        for (int i = 0; i < _data.Length; i++)
        {
            _data[i] = -1;
        }
    }

    public void Start()
    {
        int moveCount = 0;
        int WhoseTurn = 1;
        bool win = false;
        int input = -1;
        int whoWon = -1;
        Console.WriteLine("Starting Game Now ...");
        do
        {
            Console.WriteLine();
            DrawBoard();
            Console.WriteLine();
            ShowMenu(WhoseTurn);

            // Player Move
            if (WhoseTurn == 1)
            {
                //try
                //{
                //    input = int.Parse(Console.ReadLine());
                //}
                //catch (Exception)
                //{
                //    input = -1;
                //}
                input = ComputerMove(WhoseTurn);
            }

            // Computer Move
            else
            {
                input = ComputerMove(WhoseTurn);
            }

            if (input != -1)
            {
                bool validResult = false;
                validResult = PutValue(input, WhoseTurn, _data);

                if (!validResult)
                {
                    Console.WriteLine("\nInvalid Move!");
                }
                else
                {
                    WhoseTurn = WhoseTurn == 0 ? 1 : 0;
                    moveCount++;
                    whoWon = CheckWinCondition(_data);
                    win = whoWon != -1;
                }
            }
        }
        while (moveCount < 9 && !win);

        Console.WriteLine("Thanks for playing!\n");

        DrawBoard();

        Console.WriteLine("\n" + "Game Output: " + (win ? (XorYFromInt(whoWon) + " Won") : "DRAW"));
    }

    private int CheckWinCondition(int[] board)
    {
        for (int i = 0; i < 9;)
        {
            // Horizontal checking
            if (board[i] != -1 && board[i] == board[i + 1] && board[i + 1] == board[i + 2])
            {
                return board[i];
            }
            i += 3;
        }

        for (int i = 0; i < 3; i++)
        {
            // Vertical checking
            if (board[i] != -1 && board[i] == board[i + 3] && board[i + 3] == board[i + 6])
            {
                return board[i];
            }
        }

        // Diagonal Checking
        if (board[0] != -1 && board[0] == board[4] && board[4] == board[8])
        {
            return board[0];
        }

        if (board[2] != -1 && board[2] == board[4] && board[4] == board[6])
        {
            return board[2];
        }

        return -1;
    }

    private int ComputerMove(int WhoseTurn)
    {
        for (int i = 0; i < _data.Length; i++)
        {
            int[] dataCopy = (int[])_data.Clone();
            if (dataCopy[i] == -1)
            {
                PutValue(i, (WhoseTurn == 1 ? 0 : 1), dataCopy);
                if (CheckWinCondition(dataCopy) == (WhoseTurn == 1 ? 0 : 1))
                    return i;
            }

            dataCopy = (int[])_data.Clone();

            if (dataCopy[i] == -1)
            {
                PutValue(i, WhoseTurn, dataCopy);
                if (CheckWinCondition(dataCopy) == WhoseTurn)
                    return i;
            }
        }

        // Try taking the corners
        Random rand = new Random();
        List<int> Corners = new List<int> { 0, 2, 6, 8 };
        do
        {
            int j = rand.Next(Corners.Count);
            if (_data[Corners[j]] == -1)
                return Corners[j];
            else
                Corners.RemoveAt(j);
        } while (Corners.Count == 0);

        // Take the middle
        if (_data[4] == -1)
            return 4;

        List<int> sides = new List<int> { 1, 3, 5, 7 };
        do
        {
            int j = rand.Next(sides.Count);
            if (_data[sides[j]] == -1)
                return sides[j];
            else
                sides.RemoveAt(j);
        } while (sides.Count == 0);

        return -1;
    }

    private void ShowMenu(int XorO)
    {
        Console.Write("Put a number here to put an " + XorYFromInt(XorO) + " there: ");
    }

    private bool PutValue(int at, int what, int[] array)
    {
        if (array[at] == -1)
        {
            array[at] = what;
            return true;
        }
        else
            return false;
    }

    private void DrawBoard()
    {
        Console.WriteLine("|-" + XorYFromInt(_data[0]) + "-|-" + XorYFromInt(_data[1]) + "-|-" + XorYFromInt(_data[2]) + "-|");
        Console.WriteLine("|-" + XorYFromInt(_data[3]) + "-|-" + XorYFromInt(_data[4]) + "-|-" + XorYFromInt(_data[5]) + "-|");
        Console.WriteLine("|-" + XorYFromInt(_data[6]) + "-|-" + XorYFromInt(_data[7]) + "-|-" + XorYFromInt(_data[8]) + "-|");
    }

    private string XorYFromInt(int number)
    {
        switch (number)
        {
            case 0:
                return "X";

            case 1:
                return "O";

            default:
                return "-";
        }
    }
}
