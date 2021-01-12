using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    class Move
    {
        public int i;
        public int j;
        public int piece;
        public double score;

        public Move()
        { }

        public Move(int i, int j, int piece, double score)
        {
            this.i = i;
            this.j = j;
            this.piece = piece;
            this.score = score;
        }
        public Move(int i, int j, int piece)
        {
            this.i = i;
            this.j = j;
            this.piece = piece;
        }

    }


    class Board
    {
       public int row = 3;
       public  int col = 3;
        int X = 1;
        int O = 2;
        int Empty = 0;

       public  bool isGameOver;
       public int winningPiece;

       public int[,] board; 
         
       
        public Board()
        {
            board = new int[row, col];
            this.isGameOver = false;
            this.winningPiece = Empty;
            initBoard();
        }

        public Board(int[,] b)
        {
            this.board = b;
            this.isGameOver = false;
            this.winningPiece = Empty;
            initBoard();
        }

        public void initBoard()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    board[i, j] = Empty;
                }
            }
        }

        public bool IsValidSquare(int i, int j)
        {
            if (i < row &&  i >= 0 && j < col && j >= 0 && this.board[i, j] == Empty)
                return true;
            return false;
        }

        public void MakeMove(Move move, int piece)
        {
            if (IsValidSquare(move.i, move.j))
                board[move.i, move.j] = piece;
            else Console.WriteLine("cant make move");
        }

        public void printBoard()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (board[i, j] == Empty)
                        Console.Write(" - ");
                    else
                    {
                        if (board[i, j] == X)
                        {
                            Console.Write(" X ");
                        }
                        else Console.Write(" O ");
                    }
                }
                Console.WriteLine();
            }
        }

        public void CheckIfGameOver()
        {
            if (HasWinner() == true)
                this.isGameOver = true;
            else
            {
                if (IsDraw() == true)
                    this.isGameOver = true;

                else this.isGameOver = false;
            }

        }

        public bool HasWinner()
        {
            if(HasHorizontalLine(0))
            {
                winningPiece = board[0, 0];
                return true;
            }
            if (HasHorizontalLine(1))
            {
                winningPiece = board[1, 0];
                return true;
            }
            if (HasHorizontalLine(2))
            {
                winningPiece = board[2, 0];
                return true;
            }
            //--------------------------------------------------------
            if (HasVerticalLine(0))
            {
                winningPiece = board[0, 0];
                return true;
            }
            if (HasVerticalLine(1))
            {
                winningPiece = board[0, 1];
                return true;
            }
            if (HasVerticalLine(2))
            {
                winningPiece = board[0, 2];
                return true;
            }
            //--------------------------------------------------------
            if (HasDiagonalLine1(0))
            {
                winningPiece = board[0, 0];
                return true;
            }
            if (HasDiagonalLine2(0))
            {
                winningPiece = board[0, 2];
                return true;
            }
            //--------------------------------------------------------------

            return false;
        }

        public bool IsDraw()
        {

            if (HasWinner())
                return false;

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if(this.board[i,j] == Empty)
                    return false;
                }
               
            }

            return true;
        }

        public bool HasHorizontalLine(int startPoint)
        {
            if (this.board[startPoint, 0] == this.board[startPoint, 1] && this.board[startPoint, 1] == this.board[startPoint, 2] &&
                this.board[startPoint, 0] != Empty)
                return true;
            return false;
        }

        public bool HasVerticalLine(int startPoint)
        {
            if (this.board[0, startPoint] == this.board[1, startPoint] && this.board[1, startPoint] == this.board[2, startPoint] &&
                this.board[0, startPoint] != Empty)
                return true;
            return false;
        }

        public bool HasDiagonalLine1(int startPoint)
        {
            if (board[startPoint, startPoint] == board[startPoint + 1, startPoint + 1] && board[startPoint + 1, startPoint + 1] == board[startPoint + 2, startPoint + 2] &&
               board[startPoint, startPoint] != Empty)
                return true;

            return false;
        }

        public bool HasDiagonalLine2(int startPoint)
        {
            if (board[startPoint, row - startPoint - 1] == board[startPoint + 1, row - (startPoint + 1) - 1] && board[startPoint + 1, row - (startPoint + 1) -1] == board[startPoint + 2, row - (startPoint + 2) - 1] &&
               board[startPoint, row - startPoint - 1] != Empty)
                return true;

            return false;
        }

    }
}
