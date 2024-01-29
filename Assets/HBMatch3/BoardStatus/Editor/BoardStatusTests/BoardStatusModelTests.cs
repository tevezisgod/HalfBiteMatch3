using HBMatch3.Tiles;
using NUnit.Framework;

namespace HBMatch3.BoardStatus.Editor.BoardStatusTests
{
    public class BoardStatusTests
    {
        private const int gridSize = 8; // Assuming an 8x8 grid

        [Test]
        public void BestRow_ShouldReturnCorrectRow()
        {
            GameTile[] testBoard = SetupBoardForDistinctBestRow();
            BoardStatusModel model = new BoardStatusModel(testBoard);
            int bestRow = model.GetBestRow();
            Assert.AreEqual(3, bestRow);
        }

        private GameTile[] SetupBoardForDistinctBestRow()
        {
            GameTile[] board = new GameTile[gridSize * gridSize];
            for (int i = 0; i < board.Length; i++)
            {
                int row = i / gridSize;
                int col = i % gridSize;
                if (row != 3)
                {
                    board[i] = (col % 2 == 0) 
                        ? new GameTile(type: TileType.Green, score: 1200)
                        : new GameTile(type: TileType.Yellow, score: 1600);
                }
            }
            SetupHighScoringRow3(board);
            return board;
        }

        private void SetupHighScoringRow3(GameTile[] board)
        {
            int row3StartIndex = 3 * gridSize;
            for (int col = 0; col < gridSize; col++)
            {
                if (col == 2 || col == 4)
                    board[row3StartIndex + col] = new GameTile(type: TileType.Ice, score: 6000);
                else if (col == 3)
                    board[row3StartIndex + col] = new GameTile(type: TileType.Blue, score: 1000);
                else
                    board[row3StartIndex + col] = new GameTile(type: TileType.Ice, score: 6000);
            }
        }
        
        
        [Test]
        public void BestColumn_ShouldReturnCorrectColumn()
        {
            GameTile[] testBoard = SetupBoardForBestColumnTest();
            BoardStatusModel model = new BoardStatusModel(testBoard);
            int bestColumn = model.GetBestColumn();
            Assert.AreEqual(3, bestColumn); // Expecting column 3 to be the best
        }

        private GameTile[] SetupBoardForBestColumnTest()
        {
            GameTile[] board = new GameTile[gridSize * gridSize];
            for (int i = 0; i < board.Length; i++)
            {
                int row = i / gridSize;
                int col = i % gridSize;
                if (col != 3)
                {
                    board[i] = (row % 2 == 0) 
                        ? new GameTile(type: TileType.Green, score: 1200)
                        : new GameTile(type: TileType.Yellow, score: 1600);
                }
            }
            SetupHighScoringColumn3(board);
            return board;
        }

        private void SetupHighScoringColumn3(GameTile[] board)
        {
            for (int row = 0; row < gridSize; row++)
            {
                int index = row * gridSize + 3;
                if (row == 2 || row == 4)
                    board[index] = new GameTile(type: TileType.Ice, score: 6000);
                else if (row == 3)
                    board[index] = new GameTile(type: TileType.Blue, score: 1000);
                else
                    board[index] = new GameTile(type: TileType.Ice, score: 6000);
            }
        }
        
        
        [Test]
        public void DetectMatches_ShouldFindCorrectMatches()
        {
            GameTile[] testBoard = SetupBoardWithKnownMatches();
            BoardStatusModel model = new BoardStatusModel(testBoard);
            var matches = model.GetMatches(3);
            Assert.IsTrue(matches.Count > 0); // There should be matches of size 3
        }

        private GameTile[] SetupBoardWithKnownMatches()
        {
            GameTile[] board = new GameTile[gridSize * gridSize];
            // Setting up a board with a specific pattern that ensures matches
            for (int i = 0; i < board.Length; i++)
            {
                int row = i / gridSize;
                int col = i % gridSize;

                if (row == 2 && (col == 2 || col == 3 || col == 4))
                    board[i] = new GameTile(type: TileType.Ice, score: 6000); // Creating a match-3 of Ice tiles
                else
                    board[i] = new GameTile(type: TileType.Yellow, score: 1600); // Non-matching tiles elsewhere
            }
            return board;
        }
        
        [Test]
        public void TotalMatches_ShouldReturnCorrectCount()
        {
            GameTile[] testBoard = SetupBoardWithMultipleMatches();
            BoardStatusModel model = new BoardStatusModel(testBoard);
            int totalMatches = model.GetTotalMatches();
            int expectedMatchesCount = 1; // Set this based on your test setup
            Assert.AreEqual(expectedMatchesCount, totalMatches);
        }

        private GameTile[] SetupBoardWithMultipleMatches()
        {
            GameTile[] board = new GameTile[gridSize * gridSize];

            // Fill the board with alternating tile types to prevent accidental matches
            for (int i = 0; i < board.Length; i++)
            {
                int row = i / gridSize;
                int col = i % gridSize;
                TileType type = (row + col) % 2 == 0 ? TileType.Green : TileType.Yellow;
                board[i] = new GameTile(type: type, score: DetermineScoreByType(type));
            }

            // Create a single distinct horizontal match
            int matchRow = gridSize / 2; // Middle row for the distinct match
            for (int col = 1; col <= 3; col++)
            {
                board[matchRow * gridSize + col] =
                    new GameTile(type: TileType.Ice, score: DetermineScoreByType(TileType.Ice));
            }

            return board;
        }




        private int DetermineScoreByType(TileType type)
        {
            switch (type)
            {
                case TileType.Blue: return 1000;
                case TileType.Green: return 1200;
                case TileType.Purple: return 1400;
                case TileType.Grey: return 1300;
                case TileType.Yellow: return 1600;
                case TileType.Box: return 2000;
                case TileType.Grass: return 4000;
                case TileType.Ice: return 6000;
                default: return 0;
            }
        }
        
    }
}