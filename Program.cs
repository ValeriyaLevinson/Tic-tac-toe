using System;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            Board PlayBoard = new Board();
            Game game = new Game(PlayBoard);
            game.runGame();
        }
    }
}
