using System;

namespace MonteCarlo
{
    class Program
    {
        static int numOfMCiterations = 10;
        static int numOfSimulations = 100;
        const int numOfTests = 100;
        static int draws = 0;
        static int p1w = 0;
        static int p2w = 0;


        static void Main(string[] args)
        {

            Console.WriteLine("Please enter the desired number of simulations: ");
            string input = Console.ReadLine();
            Int32.TryParse(input, out numOfSimulations);

            Console.WriteLine("Please enter the desired of Monte Carlo iterations: ");
            input = Console.ReadLine();
            Int32.TryParse(input, out numOfMCiterations);

            for (int i = 0; i < numOfTests; i++)
            {
                Console.WriteLine("Starting sim #" + i + " with parameters: NumOfMC = " + numOfMCiterations + " and NumOfSims " + numOfSimulations + " ... ");
                start();
            }

            Console.WriteLine();

            double numOfT = (double)numOfTests;
            double avgDraw = (double)draws / numOfT;
            double avgP1Win = (double)p1w / numOfT;
            double avgP2Win = (double)p2w / numOfT;

            Console.WriteLine("Average draws: " + (avgDraw).ToString("N2"));
            Console.WriteLine("Average P1 Wins: " + (avgP1Win).ToString("N2"));
            Console.WriteLine("Average P2 Wins: " + (avgP2Win).ToString("N2"));

            Console.WriteLine("Average abs. deviaton from 33.3: " + Math.Round(Math.Abs(avgDraw - 33.33) + Math.Abs(avgP1Win - 33.33) + Math.Abs(avgP2Win - 33.33) / 3, 2));

            Console.ReadLine();
        }

        public static void start()
        {


            Console.WriteLine();

            for (int i = 0; i < numOfSimulations; i++)
            {

                switch (sim((i % 2) + 1))
                {
                    case 0: Console.Write("-"); draws++; break;
                    case 1: Console.Write("1"); p1w++; break;
                    case 2: Console.Write("2"); p2w++; break;
                }
            }
            Console.WriteLine();

            Console.WriteLine("Draws: " + draws + " | " + "P1 Win: " + p1w + " | " + "P2 Win: " + p2w);
            Console.WriteLine();

        }

        public static int sim(int player)
        {
            MonteCarloTreeSearch mcts = new MonteCarloTreeSearch(numOfMCiterations);
            Board board = new Board();

            int totalMoves = Board.DEFAULT_BOARD_SIZE * Board.DEFAULT_BOARD_SIZE;

            for (int i = 0; i < totalMoves; i++)
            {
                board = mcts.findNextMove(board, player);
                if (board.checkStatus() != -1)
                {
                    break;
                }
                player = 3 - player;
            }

            return board.checkStatus();

        }
    }
}
