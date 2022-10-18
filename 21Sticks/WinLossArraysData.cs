using System;
namespace _21Sticks
{
    public class DeserializedArray
    {
        // This class is a hack - it is used to load the data from the JSON serialization
        // Loading directly to the WinLossArraysData fails on exception (the array somehow isnt initialized)
        public int[] wins = new int[100000];
        public int[] losses = new int[100000];
        public int sticks { get; set; }
        public int moves { get; set; }
    }

    public class WinLossArraysData
    {
        // Arrays to keep track of wins and losses:
        public int[] wins { get; set; }
        public int[] losses { get; set; }
        public int sticks { get; set; }
        public int moves { get; set; }

        public WinLossArraysData(int numSticks = 21, int numMoves = 3)
        {
            sticks = numSticks;
            moves = numMoves;

            wins = new int[(sticks+1) * moves];
            losses = new int[(sticks+1) * moves];
        }

        // Constructor from a DeserializedArray - see above re hack
        public WinLossArraysData(DeserializedArray des)
        {
            sticks = des.sticks;
            moves = des.moves;

            wins = new int[(sticks + 1) * moves];
            losses = new int[(sticks + 1) * moves];

            for (int i=0; i<(sticks+1)*moves; i++)
            {
                wins[i] = des.wins[i];
                losses[i] = des.losses[i];
            }
        }

        public int getWins(int count, int i)
        {
            // Get wins[count][i] value:
            int res = wins[count * moves + i];
            return res;
        }

        public int getLosses(int count, int i)
        {
            // Get losses[count][i] value:
            return losses[count * moves + i];
        }

        public void addWins(int count, int i, int toAdd)
        {
            // add toAdd to the wins in [count, i]:
            wins[count * moves + i] += toAdd;
        }

        public void addLosses(int count, int i, int toAdd)
        {
            // add toAdd to the losses in [count, i]:
            losses[count * moves + i] += toAdd;
        }

        public void Clear()
        {
            for (int i = 0; i < (sticks + 1) * moves; i++)
            {
                wins[i] = 0;
                losses[i] = 0;
            }
        }
    }
}

