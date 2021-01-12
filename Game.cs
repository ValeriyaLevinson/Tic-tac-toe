using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    class Game
    {
        int Player; //1 - human 2 - computer 
        Board PlayBoard;
        AIplayer compPlayer;

        public Game(Board PlayBoard)
        {
            this.PlayBoard = PlayBoard;
            Player = 1;
            this.compPlayer = new AIplayer(this.PlayBoard);
        }

        public void runGame()
        {
            Move m = new Move();
            int r, c;
            while(!(PlayBoard.isGameOver))
            {


                switch (Player)
                {
                    case 1:
                        //Console.WriteLine("enter the row");
                        r = Convert.ToInt32(Console.ReadLine());

                     //   Console.WriteLine("enter the col");
                        c = Convert.ToInt32(Console.ReadLine());

                        m.i = r;
                        m.j = c;
                        PlayBoard.MakeMove(m, 1);
                        PlayBoard.printBoard();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();

                        Player = 2;
                        break;

                    case 2:
                        compPlayer.BestMove();
                        this.PlayBoard.printBoard();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();

                        Player = 1;

                        break;
                }

                PlayBoard.CheckIfGameOver();
            }



            if (this.PlayBoard.IsDraw())
                Console.WriteLine("Draw");
            if (this.PlayBoard.HasWinner())
                Console.WriteLine("the winner is" + PlayBoard.winningPiece);
           

            Console.ReadLine();
        }
    }
}
