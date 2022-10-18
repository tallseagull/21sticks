using System;
using System.Text.Json;

namespace _21Sticks
{
    public class WinLossMatrix
    {
        // Random generator:
        Random rnd = new Random(42);
        private WinLossArraysData matrix;

        public WinLossMatrix()
        {
            matrix = new WinLossArraysData(21, 3);
        }

        public WinLossMatrix(int numSticks, int numMoves)
        {
            // Constructor for a non-default array size
            matrix = new WinLossArraysData(numSticks, numMoves);
        }

        public void AddWinLoss(Move move, bool isWin)
        {
            // Add a win / loss (according to the isWin bool - true means a win)
            if (isWin)
            {
                matrix.addWins(move.count, move.move - 1, 1);
            }
            else
            {
                matrix.addLosses(move.count, move.move - 1, 1);
            }
        }

        public double GetWinRate(int cnt, int i, int rand_size)
        {
            double w, l, win_rate;

            w = matrix.getWins(cnt, i) + rnd.Next(0, rand_size);
            l = matrix.getLosses(cnt, i) + rnd.Next(0, rand_size);
            if (w + l > 0)
            {
                win_rate = w / (w + l);
            }
            else
            {
                if (rand_size == 0)
                {
                    win_rate = 0.5;
                }
                else
                {
                    win_rate = 0.25 + rnd.Next(0, rand_size) / (rand_size * 2.0);
                }
            }
            return win_rate;
        }

        public void PrintWinsProb()
        {
            double win_rate;

            // Print the win-loss prob matrix:
            for (int cnt = 21; cnt > 0; cnt--)
            {
                Console.Write($"{cnt}:\t");
                for (int i = 0; i < 3; i++)
                {
                    win_rate = GetWinRate(cnt, i, 0);
                    Console.Write($"{win_rate:0.00}\t | ");
                }
                Console.Write("\n");
            }
        }

        public void Clear()
        {
            matrix.Clear();
        }

        public void Serialize(string fileName = "matrix.json")
        {
            string jsonString = JsonSerializer.Serialize(matrix);
            File.WriteAllText(fileName, jsonString);
        }

        public void Deserialize(string fileName = "matrix.json")
        {
            if (File.Exists(fileName))
            {
                string jsonString = File.ReadAllText(fileName);
                DeserializedArray des = JsonSerializer.Deserialize<DeserializedArray>(jsonString)!;
                matrix = new WinLossArraysData(des);
            }
        }
    }

}

