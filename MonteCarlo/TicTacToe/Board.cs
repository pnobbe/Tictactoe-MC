using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarlo
{
    public class Board
    {
        int[,] boardValues;
        int totalMoves;

        public const int DEFAULT_BOARD_SIZE = 3;

        public const int IN_PROGRESS = -1;
        public const int DRAW = 0;
        public const int P1 = 1;
        public const int P2 = 2;

        public Board()
        {
            boardValues = new int[DEFAULT_BOARD_SIZE, DEFAULT_BOARD_SIZE];
        }

        public Board(int boardSize)
        {
            boardValues = new int[boardSize, boardSize];
        }

        public Board(int[,] boardValues)
        {
            this.boardValues = boardValues;
        }

        public Board(int[,] boardValues, int totalMoves)
        {
            this.boardValues = boardValues;
            this.totalMoves = totalMoves;
        }

        public Board(Board board)
        {
            int boardLength = (int) Math.Sqrt(board.getBoardValues().Length);
            this.boardValues = new int[boardLength, boardLength];
            int[,] boardValues = board.getBoardValues();
            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    this.boardValues[i, j] = boardValues[i, j];
                }
            }
        }

        public void performMove(int player, Position p)
        {
            this.totalMoves++;
            boardValues[p.getX(), p.getY()] = player;
        }

        public int[,] getBoardValues()
        {
            return boardValues;
        }

        public void setBoardValues(int[,] boardValues)
        {
            this.boardValues = boardValues;
        }

        public int checkStatus()
        {
            int boardSize = (int) Math.Sqrt(boardValues.Length);
            int maxIndex = boardSize - 1;
            int[] diag1 = new int[boardSize];
            int[] diag2 = new int[boardSize];

            for (int i = 0; i < boardSize; i++)
            {
                int[] row = new int[boardSize];
                int[] col = new int[boardSize];
                for (int j = 0; j < boardSize; j++)
                {
                    row[j] = boardValues[i, j];
                    col[j] = boardValues[j, i];
                }

                int checkRowForWin = checkForWin(row);
                if (checkRowForWin != 0)
                    return checkRowForWin;

                int checkColForWin = checkForWin(col);
                if (checkColForWin != 0)
                    return checkColForWin;

                diag1[i] = boardValues[i, i];
                diag2[i] = boardValues[maxIndex - i, i];
            }

            int checkDiag1ForWin = checkForWin(diag1);
            if (checkDiag1ForWin != 0)
                return checkDiag1ForWin;

            int checkDiag2ForWin = checkForWin(diag2);
            if (checkDiag2ForWin != 0)
                return checkDiag2ForWin;

            if (getEmptyPositions().Count > 0)
                return IN_PROGRESS;
            else
                return DRAW;
        }

        private int checkForWin(int[] row)
        {
            Boolean isEqual = true;
            int size = row.Length;
            int previous = row[0];
            for (int i = 0; i < size; i++)
            {
                if (previous != row[i])
                {
                    isEqual = false;
                    break;
                }
                previous = row[i];
            }
            if (isEqual)
                return previous;
            else
                return 0;
        }

        public void printBoard()
        {
            int size = this.boardValues.Length;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(boardValues[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public List<Position> getEmptyPositions()
        {
            int size = (int) Math.Sqrt(this.boardValues.Length);
            List<Position> emptyPositions = new List<Position>();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (boardValues[i, j] == 0)
                        emptyPositions.Add(new Position(i, j));
                }
            }
            return emptyPositions;
        }

        public void printStatus()
        {
            switch (this.checkStatus())
            {
                case P1:
                    Console.WriteLine("Player 1 wins");
                    break;
                case P2:
                    Console.WriteLine("Player 2 wins");
                    break;
                case DRAW:
                    Console.WriteLine("Game Draw");
                    break;
                case IN_PROGRESS:
                    Console.WriteLine("Game In rogress");
                    break;
            }
        }
    }
}