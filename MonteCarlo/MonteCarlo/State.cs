using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarlo
{
    public class State
    {
        private Board board;
        private int playerNo;
        private int visitCount;
        private double winScore;

        public State()
        {
            this.board = new Board();
        }

        public State(State state)
        {
            this.board = new Board(state.getBoard());
            this.playerNo = state.getPlayerNo();
            this.visitCount = state.getVisitCount();
            this.winScore = state.getWinScore();
        }

        public State(Board board)
        {
            this.board = new Board(board);
        }

        public Board getBoard()
        {
            return board;
        }

        public void setBoard(Board board)
        {
            this.board = board;
        }

        public int getPlayerNo()
        {
            return playerNo;
        }

        public void setPlayerNo(int playerNo)
        {
            this.playerNo = playerNo;
        }

        public int getOpponent()
        {
            return 3 - playerNo;
        }

        public int getVisitCount()
        {
            return visitCount;
        }

        public void setVisitCount(int visitCount)
        {
            this.visitCount = visitCount;
        }

        public double getWinScore()
        {
            return winScore;
        }

        public void setWinScore(double winScore)
        {
            this.winScore = winScore;
        }

        public List<State> getAllPossibleStates()
        {
            List<State> possibleStates = new List<State>();
            List<Position> availablePositions = this.board.getEmptyPositions();
            foreach (Position p in availablePositions)
            {
                State newState = new State(this.board);
                newState.setPlayerNo(3 - this.playerNo);
                newState.getBoard().performMove(newState.getPlayerNo(), p);
                possibleStates.Add(newState);
            }

            return possibleStates;
        }

        public void incrementVisit()
        {
            this.visitCount++;
        }

        public void addScore(double score)
        {
            if (this.winScore != Int32.MinValue)
                this.winScore += score;
        }

        public void randomPlay()
        {
            List<Position> availablePositions = this.board.getEmptyPositions();
            int totalPossibilities = availablePositions.Count;
            int selectRandom = (int)(Randomizer.getLong((totalPossibilities - 1) + 1));
            this.board.performMove(this.playerNo, availablePositions.ElementAt(selectRandom));
        }

        public void togglePlayer()
        {
            this.playerNo = 3 - this.playerNo;
        }
    }
}
