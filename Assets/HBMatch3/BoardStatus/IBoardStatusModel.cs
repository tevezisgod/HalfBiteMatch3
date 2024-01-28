using System;
using System.Collections.Generic;
using HBMatch3.Tiles;

namespace HBMatch3.BoardStatus
{
    public interface IBoardStatusModel
    {
        GameTile[] GetBoard();
        int GetBestRow();
        int GetBestColumn();
        List<Tuple<int, int>> GetMatches(int matchSize);
        int GetTotalMatches();
    }
}