using System;
using System.Collections.Generic;
using HBMatch3.Tiles;

namespace HBMatch3.BoardStatus
{
    /// <summary>
    /// Description:
    /// The BoardStatusView class implements the IBoardStatusView interface and provides console-based methods for
    /// visually displaying the board status, including the board itself, the best row and column, matches, and the total number of matches.
    /// </summary>
    public class BoardStatusView : IBoardStatusView
    {
        public void DisplayBoard(GameTile[] board)
        {
            Console.WriteLine("Displaying Board:");
            foreach (var tile in board)
            {
                Console.Write($"{tile.Type}({tile.Score}) ");
            }
            Console.WriteLine();
        }

        public void DisplayBestRow(int bestRow)
        {
            Console.WriteLine($"Best Row: {bestRow}");
        }

        public void DisplayBestColumn(int bestColumn)
        {
            Console.WriteLine($"Best Column: {bestColumn}");
        }

        public void DisplayMatches(List<Tuple<int, int>> matches)
        {
            Console.WriteLine("Matches:");
            foreach (var match in matches)
            {
                Console.Write($"({match.Item1},{match.Item2}) ");
            }
            Console.WriteLine();
        }

        public void DisplayTotalMatches(int totalMatches)
        {
            Console.WriteLine($"Total Matches: {totalMatches}");
        }
    }
}