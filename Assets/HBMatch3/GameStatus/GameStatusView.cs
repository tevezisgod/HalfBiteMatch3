// Visual representation of the game's status.

using HBMatch3.Tiles;
using UnityEngine;

namespace HBMatch3.GameStatus
{
    public class GameStatusView : MonoBehaviour, IGameStatusView
    {
        public void UpdateStatusDisplay(/* Additional parameters as needed */)
        {
            // Update general status display, if needed
        }

        public void DisplayBoard(GameTile[] board)
        {
            // Implement logic to visually represent the board state
        }

        public void DisplayTilesDestroyed(int count)
        {
            // Implement logic to display the number of tiles destroyed
        }

        public void DisplayMovesPlayed(int count)
        {
            // Implement logic to display the number of moves played
        }
    }
}