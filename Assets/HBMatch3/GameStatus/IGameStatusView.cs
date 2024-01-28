// Interface for the GameStatusView.

using HBMatch3.Tiles;

namespace HBMatch3.GameStatus
{
    public interface IGameStatusView
    {
        void UpdateStatusDisplay(/* parameters */);
        void DisplayBoard(GameTile[] board);
        void DisplayTilesDestroyed(int count);
        void DisplayMovesPlayed(int count);
    }
}