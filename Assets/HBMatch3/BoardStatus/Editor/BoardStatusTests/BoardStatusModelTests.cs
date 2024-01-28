using HBMatch3.Tiles;
using NUnit.Framework;

namespace HBMatch3.BoardStatus.Editor.BoardStatusTests
{
    public class BoardStatusModelTests
    {
        private const int gridSize = 8; // Assuming an 8x8 grid
        [Test]
        public void BestRow_ShouldReturnCorrectRow()
        {
            // Arrange: Create a board setup
            GameTile[] testBoard = new GameTile[64]; // Assuming an 8x8 grid
            for (int i = 0; i < testBoard.Length; i++)
            {
                int row = i / gridSize;
                int col = i % gridSize;

                // Set most tiles to a non-scoring type, but avoid creating matches
                if (row != 3)
                {
                    testBoard[i] = (col % 2 == 0) 
                        ? new GameTile(type: TileType.Green, score: 1200)
                        : new GameTile(type: TileType.Yellow, score: 1600);
                }
            }

            // Create a unique high-scoring setup in row 3
            SetupHighScoringRow3(testBoard);

            BoardStatusModel model = new BoardStatusModel(testBoard);

            // Act: Get the best row
            int bestRow = model.GetBestRow();

            // Assert: Verify that row 3 is identified as the best row
            Assert.AreEqual(3, bestRow);
        }

        private void SetupHighScoringRow3(GameTile[] board)
        {
            int row3StartIndex = 3 * gridSize;
            for (int col = 0; col < gridSize; col++)
            {
                // Place a specific pattern that allows for a high score in row 3
                if (col == 2 || col == 4)
                    board[row3StartIndex + col] = new GameTile(type: TileType.Ice, score: 6000);
                else if (col == 3)
                    board[row3StartIndex + col] = new GameTile(type: TileType.Blue, score: 1000); // Swap candidate
                else
                    board[row3StartIndex + col] = new GameTile(type: TileType.Ice, score: 6000); // Filler tiles
            }
        }
    }
}