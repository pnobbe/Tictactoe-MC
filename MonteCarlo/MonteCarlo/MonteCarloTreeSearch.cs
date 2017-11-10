using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarlo
{
    public class MonteCarloTreeSearch
    {
        const int WIN_SCORE = 10;
        int level;
        int opponent;
        int iterations;

        public MonteCarloTreeSearch(int iterations)
        {
            this.iterations = iterations;
            this.level = 3;
        }

        public int getLevel()
        {
            return level;
        }

        public void setLevel(int level)
        {
            this.level = level;
        }

        private int getTicksForCurrentLevel()
        {
            return 2 * (this.level - 1) + 1;
        }

        public Board findNextMove(Board board, int playerNo)
        {

            opponent = 3 - playerNo;
            Tree tree = new Tree();
            Node rootNode = tree.getRoot();
            rootNode.getState().setBoard(board);
            rootNode.getState().setPlayerNo(opponent);

            for (int i = 0; i < iterations; i++)
            {
                Node promisingNode = selectPromisingNode(rootNode);
                if (promisingNode.getState().getBoard().checkStatus()
                  == Board.IN_PROGRESS)
                {
                    expandNode(promisingNode);
                }
                Node nodeToExplore = promisingNode;
                if (promisingNode.getChildArray().Count > 0)
                {
                    nodeToExplore = promisingNode.getRandomChildNode();
                }
                int playoutResult = simulateRandomPlayout(nodeToExplore);
                backPropogation(nodeToExplore, playoutResult);
            }

            Node winnerNode = rootNode.getChildWithMaxScore();
            tree.setRoot(winnerNode);
            return winnerNode.getState().getBoard();
        }

        private Node selectPromisingNode(Node rootNode)
        {
            Node node = rootNode;
            while (node.getChildArray().Count != 0)
            {
                node = UCT.findBestNodeWithUCT(node);
            }
            return node;
        }

        private void expandNode(Node node)
        {
            List<State> possibleStates = node.getState().getAllPossibleStates();
            foreach(State state in possibleStates)
            {
                Node newNode = new Node(state);
                newNode.setParent(node);
                newNode.getState().setPlayerNo(node.getState().getOpponent());
                node.getChildArray().Add(newNode);
            }
        }

        private void backPropogation(Node nodeToExplore, int playerNo)
        {
            Node tempNode = nodeToExplore;
            while (tempNode != null)
            {
                tempNode.getState().incrementVisit();
                if (tempNode.getState().getPlayerNo() == playerNo)
                {
                    tempNode.getState().addScore(WIN_SCORE);
                }
                tempNode = tempNode.getParent();
            }
        }
        private int simulateRandomPlayout(Node node)
        {
            Node tempNode = new Node(node);
            State tempState = tempNode.getState();
            int boardStatus = tempState.getBoard().checkStatus();
            if (boardStatus == opponent)
            {
                tempNode.getParent().getState().setWinScore(Int32.MinValue);
                return boardStatus;
            }
            while (boardStatus == Board.IN_PROGRESS)
            {
                tempState.togglePlayer();
                tempState.randomPlay();
                boardStatus = tempState.getBoard().checkStatus();
            }
            return boardStatus;
        }

    }
}
