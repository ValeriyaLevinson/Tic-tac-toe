using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    class AIplayer
    {
        const int AIPIECE = 2;
        const int OPONENTPIECE = 1;
        const int EMPTY = 0;
        int DEPTH = 2;
        Board GameBoard;

        public AIplayer(Board GameBoard)
        {
            this.GameBoard = GameBoard;
        }

        public int getOponentPiece(int piece)
        {
            if (piece == AIPIECE)
                return OPONENTPIECE;
            else return AIPIECE;
        }

        int[,] copyBoard(int[,] board)
        {
            int[,] copyBoard = new int[GameBoard.row, GameBoard.col];

            for (int i = 0; i < GameBoard.row; i++)
            {
                for (int j = 0; j < GameBoard.col; j++)
                {
                    copyBoard[i, j] = board[i, j];
                }
            }

            return copyBoard;
        }

        public double Evaluate(Board b)
        {

            if (b.HasWinner())
            {
                if (b.winningPiece == AIPIECE)
                    return double.MaxValue;
                else
                    return double.MinValue;
            }


            double maxValue = EvaluatePiece(b, AIPIECE);
            double minValue = EvaluatePiece(b, OPONENTPIECE);


            return maxValue - minValue;

        }
        private double EvaluatePiece(Board b, int piece)
        {

            return EvaluateRows(b, piece) + EvaluateColumns(b, piece) + EvaluateDiagonals(b, piece);
        }
        private double EvaluateRows(Board b, int p)
        {

            int cols = b.col;
            int rows = b.row;

            double score = 0.0;
            int count;
            // check the rows
            for (int i = 0; i < rows; i++)
            {
                count = 0;
                bool rowClean = true;
                for (int j = 0; j < cols; j++)
                {
                    if (b.board[i, j] == p)
                        count++;

                    else if (b.board[i, j] == getOponentPiece(p))
                    {
                        rowClean = false;
                        break;
                    }
                }

                // if we get here then the row is clean (an open row)
                if (rowClean && count != 0)
                    score += (double)count;
            }

            return score;
        }
        private double EvaluateColumns(Board b, int p)
        {
            int cols = b.col;
            int rows = b.row;

            double score = 0.0;
            int count;
            // check the rows
            for (int j = 0; j < cols; j++)
            {
                count = 0;
                bool rowClean = true;
                for (int i = 0; i < rows; i++)
                {
                    if (b.board[i, j] == p)
                        count++;

                    else if (b.board[i, j] == getOponentPiece(p))
                    {
                        rowClean = false;
                        break;
                    }
                }

                // if we get here then the row is clean (an open row)
                if (rowClean && count != 0)
                    score += (double)count;

            }

            return score;
        }
        private double EvaluateDiagonals(Board b, int p)
        {
            // go down and to the right diagonal first
            int count = 0;
            bool diagonalClean = true;
            int cols = b.col;
            int rows = b.row;
            double score = 0.0;

            for (int i = 0; i < cols; i++)
            {
                if (b.board[i, i] == p)
                    count++;

                if (b.board[i, i] == getOponentPiece(p))
                {
                    diagonalClean = false;
                    break;
                }
            }

            if (diagonalClean && count > 0)
                score += (double)count;// Math.Pow(count, count);

            // now try the other way

            int row = 0;
            int col = 2;
            count = 0;
            diagonalClean = true;

            while (row < rows && col >= 0)
            {
                if (b.board[row, col] == p)
                    count++;

                if (b.board[row, col] == getOponentPiece(p))
                {
                    diagonalClean = false;
                    break;
                }
                row++;
                col--;
            }

            if (count > 0 && diagonalClean)
                score += count;

            return score;
        }

        public void BestMove()
        {
            double BestScore = double.MinValue;
            Move bestMove = new Move();
            for (int i = 0; i < this.GameBoard.row; i++)
            {
                for (int j = 0; j < this.GameBoard.col; j++)
                {
                    if (this.GameBoard.IsValidSquare(i, j))
                    {
                        this.GameBoard.board[i, j] = AIPIECE;
                        double score = MiniMax2(this.GameBoard, DEPTH,  double.MinValue, double.MaxValue, false);
                        this.GameBoard.board[i, j] = EMPTY;
                        if (score > BestScore)
                        {
                            BestScore = score;
                            bestMove.i = i;
                            bestMove.j = j;
                            bestMove.score = score;
                        }
                    }
                }
            }
            this.GameBoard.board[bestMove.i, bestMove.j] = AIPIECE;
        }



        double Minimax(Board b, int depth, bool IsMaximizing)
        {
            double bestscore;

            if (b.HasWinner() || depth == 0 || b.IsDraw())
            {
                return Evaluate(b);
            }


            if (IsMaximizing)
            {
                bestscore = double.MinValue;
                for (int i = 0; i < b.row; i++)
                {
                    for (int j = 0; j < b.col; j++)
                    {
                        if (b.IsValidSquare(i, j))
                        {
                            b.board[i, j] = AIPIECE;
                            double score = Minimax(b, depth - 1, false);
                            b.board[i, j] = EMPTY;
                            if (score > bestscore)
                            {
                                bestscore = score;
                            }
                        }

                    }
                }
                return bestscore;
            }
            else
            {
                bestscore = double.MaxValue;
                for (int i = 0; i < b.row; i++)
                {
                    for (int j = 0; j < b.col; j++)
                    {
                        if (b.IsValidSquare(i, j))
                        {
                            b.board[i, j] = OPONENTPIECE;
                            double score = Minimax(b, depth - 1, true);
                            b.board[i, j] = EMPTY;
                            if (score < bestscore)
                            {
                                bestscore = score;
                            }
                        }

                    }
                }
                return bestscore;
            }
        }

        double MiniMax2(Board b, int depth, double alpha, double beta, bool IsMaximizing)
        {
            double bestscore;
            Board newBoard;
            int[,] board;
           

            if (b.HasWinner() || depth == 0 || b.IsDraw())
            {
                return Evaluate(b);
            }


            if (IsMaximizing)
            {
                bestscore = double.MinValue;
                for (int i = 0; i < b.row; i++)
                {
                    for (int j = 0; j < b.col; j++)
                    {
                        if (b.IsValidSquare(i, j))
                        {
                            if (b.IsValidSquare(i, j))
                            {
                                b.board[i, j] = AIPIECE;
                                double score = MiniMax2(b, depth - 1, alpha,beta, false);
                                b.board[i, j] = EMPTY;
                                if (score > bestscore)
                                {
                                    bestscore = score;
                                }
                                alpha = Math.Max(alpha, score);
                            }


                            if (beta <= alpha)
                            {
                                return bestscore;
                            }

                        }
                       
                    }

                }
                return bestscore;
            }

            else
            {
                bestscore = double.MaxValue;
                for (int i = 0; i < b.row; i++)
                {
                    for (int j = 0; j < b.col; j++)
                    {
                        if (b.IsValidSquare(i, j))
                        {
                            b.board[i, j] = OPONENTPIECE;
                            double score = MiniMax2(b, depth - 1, alpha, beta, true);
                            b.board[i, j] = EMPTY;
                            if (score < bestscore)
                            {
                                bestscore = score;
                            }
                            beta = Math.Min(beta, score);
                            
                        }
                        if (beta <= alpha)
                        {
                            return bestscore;
                        }

                    }

                }
                return bestscore;

            }
        }
    }
}


