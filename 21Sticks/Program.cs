using System;
using _21Sticks;



class TwentyOneSticks
{
    // The moves are a list of tuples of moves in this game. The moves contain tuples of (count of sticks, number removed)
    private List<Move> myMoves = new List<Move>();
    private List<Move> hisMoves = new List<Move>();

    // Arrays to keep track of wins and losses:
    private WinLossMatrix WLMat = new(21, 3);

    // Random generator:
    Random rnd = new Random(1);

    public TwentyOneSticks()
    {
        // Load the matrix from our file (if already exists)
        WLMat.Deserialize();
    }

    private int MakeMove(int count, bool isMyPlayer=true)
    {
        // Make a move with count sticks left. Returns the number of sticks to remove
        // Moves are based on stats of previous win/loss with some random added to the win and loss to allow for learning:
        int best_move = 0;
        double best_rate = -1;
        double win_rate; // Win and loss counts for the loop
        int max_move = 3;

        // Find the max we can remove - it is up to count. If only one lest our move is 1 unfortunately...:
        if (count == 1)
        {
            best_move = 1;
        }
        else
        {
            if (count < 3)
            {
                max_move = count + 1;
            }
            // Go over the moves and choose the best one:
            for (int i = 0; i < max_move; i++)
            {
                win_rate = WLMat.GetWinRate(count, i, 2);
                if (win_rate > best_rate)
                {
                    // We found something better!
                    best_move = i + 1; // +1 since we start the loop from 0
                    best_rate = win_rate;
                }
            }
        }

        // Our best move is now in best_move. Do it! Add it as a Move instance to the myMoves list:
        Move the_move = new Move();
        the_move.count = count;
        the_move.move = best_move;
        if (isMyPlayer)
        {
            myMoves.Add(the_move);
        }
        else
        {
            hisMoves.Add(the_move);
        }
        
        return best_move;
    }

    private void OtherMove(int count, int move)
    {
        // Moves for the other player. Adds the move to our move record.
        Move the_move = new Move();
        the_move.count = count;
        the_move.move = move;
        hisMoves.Add(the_move);
    }

    private void RecordWinLoss(List<Move> moves, bool is_win)
    {
        foreach (Move move in moves)
        {
            WLMat.AddWinLoss(move, is_win);
        }
    }

    public void PlayMyself()
    {
        // A single game where the computer plays itself. It then learns by the result
        // to improve its learning.
        int count = 21;
        int move;
        bool isPlayer1;

        isPlayer1 = rnd.Next(0, 100) >= 50;
        while (count > 0)
        {
            isPlayer1 = !isPlayer1;  // Switch players each move
            move = MakeMove(count, isPlayer1);  // Make the move - always use the computer logic
            count -= move;          // Remove the sticks
        }
        // Record the loss and win paths:
        RecordWinLoss(myMoves, !isPlayer1);
        RecordWinLoss(hisMoves, isPlayer1);

        // Clear the lists of moves:
        myMoves.Clear();
        hisMoves.Clear();

        // Write the matrix to the file
        WLMat.Serialize();
    }

    public void PrintMat()
    {
        WLMat.PrintWinsProb();
    }

    public void ClearMat()
    {
        WLMat.Clear();
    }

    public void Game()
    {
        int count;
        int move;
        bool isMyMove;

        while (true)
        {
            // New game! Fill with 21 matches
            Console.Write("-------------------------\n New Game\n---------------------------\n");
            count = 21;

            // Select who moves first
            isMyMove = rnd.Next(0, 100) >= 50;

            while (count > 0)
            {
                // Switch player:
                isMyMove = !isMyMove;

                // Print sticks left:
                Console.Write($"---> Next turn. Sticks left: {count}\n");

                if (isMyMove)
                {
                    // Computer move. Do it.
                    move = MakeMove(count);
                    Console.Write($"Computer move: take {move}\n");
                }
                else
                {
                    // Human player move:
                    move = 0;
                    while ((move < 1) | (move > 3))
                    {
                        Console.Write("Please enter a move of 1-3: ");
                        move = Convert.ToInt32(Console.ReadLine());
                    }
                    OtherMove(count, move);
                }

                // Remove the sticks
                count -= move;
            }

            // Record win and loss:
            if (isMyMove)
            {
                Console.Write("I took the last stick, you won!!!!!! :-)\n\n\n");                
                
            }
            else
            {
                Console.Write("You took the last stick, you lost! :-(\n\n\n");
            }

            // Record the loss and win paths:
            RecordWinLoss(myMoves, !isMyMove);
            RecordWinLoss(hisMoves, isMyMove);

            // Clear the lists of moves:
            myMoves.Clear();
            hisMoves.Clear();

            // Print the matrix:
            Console.WriteLine("Here is our win-loss matrix:");
            WLMat.PrintWinsProb();

            // Write the matrix to the file
            WLMat.Serialize();
        }
    }
}


class Program
{
    static void Main()
    {
        TwentyOneSticks tos = new TwentyOneSticks();

        // Ask whether to clear the matrix:
        Console.Write("Should I clear the learning? [y/N]: ");
        string ans = Console.ReadLine();
        if ((ans[0]=='y') | (ans[0]=='Y'))
        {
            Console.WriteLine("Clearing Matrix...");
            tos.ClearMat();
        }

        // First play N games with myself:
        Console.Write("How many games to play before starting? ");
        int nGames = Convert.ToInt32(Console.ReadLine());
        for (int i=0; i< nGames; i++)
        {
            tos.PlayMyself();
            if ((i % 100) == 0)
            {
                Console.WriteLine($"In cycle {i}...");
                tos.PrintMat();
            }
        }
        
        tos.Game();
    }
}
