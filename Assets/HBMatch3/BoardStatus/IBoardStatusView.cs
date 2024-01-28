using System;
using System.Collections.Generic;
using HBMatch3.Tiles;

namespace HBMatch3.BoardStatus
{
    /// <summary>
    /// Description:
    /// The IBoardStatusView interface outlines methods for displaying various aspects of the board status,
    /// including the board itself, the best row and column, matches, and the total number of matches.
    /// </summary>
    public interface IBoardStatusView
    {
        void DisplayBoard(GameTile[] board);
        void DisplayBestRow(int bestRow);
        void DisplayBestColumn(int bestColumn);
        void DisplayMatches(List<Tuple<int, int>> matches);
        void DisplayTotalMatches(int totalMatches);
    }
}