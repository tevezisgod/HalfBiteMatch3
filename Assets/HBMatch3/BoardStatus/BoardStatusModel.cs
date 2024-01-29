using System;
using System.Collections.Generic;
using HBMatch3.Tiles;
using UnityEngine;

namespace HBMatch3.BoardStatus
{
    public class BoardStatusModel : IBoardStatusModel
    {
        private GameTile[] board;
        private const int gridSize = 8; // Assuming an 8x8 grid

        public BoardStatusModel(GameTile[] initialBoardState)
        {
            Initialize(initialBoardState);
        }

        private void Initialize(GameTile[] initialBoardState)
        {
            if (initialBoardState.Length != gridSize * gridSize)
                throw new ArgumentException("Initial board size does not match the specified grid size.");
        
            board = initialBoardState;
        }

        public GameTile[] GetBoard() => board;
    
        public List<Tuple<int, int>> GetMatches(int matchSize)
        {
            List<Tuple<int, int>> matches = new List<Tuple<int, int>>();

            // Check horizontally
            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x <= gridSize - matchSize; x++)
                {
                    if (IsMatchAtPosition(x, y, matchSize, true))
                    {
                        matches.Add(new Tuple<int, int>(x, y));
                    }
                }
            }

            // Check vertically
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y <= gridSize - matchSize; y++)
                {
                    if (IsMatchAtPosition(x, y, matchSize, false))
                    {
                        matches.Add(new Tuple<int, int>(x, y));
                    }
                }
            }

            return matches;
        }
        public int GetTotalMatches()
        {
            bool[,] matched = new bool[gridSize, gridSize];
            int totalMatches = 0;

            // Check for horizontal matches
            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize; x++)
                {
                    if (!matched[y, x] && IsPartOfHorizontalMatch(x, y, matched))
                    {
                        totalMatches++;
                        // Mark the entire horizontal match
                        MarkHorizontalMatch(x, y, matched);
                    }
                }
            }

            // Check for vertical matches
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    if (!matched[y, x] && IsPartOfVerticalMatch(x, y, matched))
                    {
                        totalMatches++;
                        // Mark the entire vertical match
                        MarkVerticalMatch(x, y, matched);
                    }
                }
            }

            return totalMatches;
        }

        private bool IsPartOfHorizontalMatch(int x, int y, bool[,] matched)
        {
            if (x >= gridSize - 2) return false; // Not enough space for a horizontal match
            TileType type = board[y * gridSize + x].Type;
            return !matched[y, x] && board[y * gridSize + x + 1].Type == type && board[y * gridSize + x + 2].Type == type;
        }

        private void MarkHorizontalMatch(int x, int y, bool[,] matched)
        {
            TileType type = board[y * gridSize + x].Type;
            for (int i = x; i < gridSize && board[y * gridSize + i].Type == type; i++)
            {
                matched[y, i] = true;
            }
        }

        private bool IsPartOfVerticalMatch(int x, int y, bool[,] matched)
        {
            if (y >= gridSize - 2) return false; // Not enough space for a vertical match
            TileType type = board[y * gridSize + x].Type;
            return !matched[y, x] && board[(y + 1) * gridSize + x].Type == type && board[(y + 2) * gridSize + x].Type == type;
        }

        private void MarkVerticalMatch(int x, int y, bool[,] matched)
        {
            TileType type = board[y * gridSize + x].Type;
            for (int i = y; i < gridSize && board[i * gridSize + x].Type == type; i++)
            {
                matched[i, x] = true;
            }
        }


        private bool IsMatchAtPosition(int x, int y, int matchSize, bool horizontal)
        {
            TileType type = board[y * gridSize + x].Type;
            for (int i = 1; i < matchSize; i++)
            {
                int checkX = horizontal ? x + i : x;
                int checkY = horizontal ? y : y + i;

                if (checkX >= gridSize || checkY >= gridSize || board[checkY * gridSize + checkX].Type != type)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsPartOfMatch(int x, int y)
        {
            TileType type = board[y * gridSize + x].Type;

            // Check horizontally and vertically
            return (IsLineMatch(x, y, type, true) || IsLineMatch(x, y, type, false));
        }

        private bool IsLineMatch(int x, int y, TileType type, bool horizontal)
        {
            int matchCount = 1;

            // Check one direction
            matchCount += CheckLine(x, y, type, horizontal, 1);
            // Check the opposite direction
            matchCount += CheckLine(x, y, type, horizontal, -1);

            return matchCount >= 3;
        }

        private int CheckLine(int x, int y, TileType type, bool horizontal, int direction)
        {
            int count = 0;
            while (true)
            {
                x += horizontal ? direction : 0;
                y += horizontal ? 0 : direction;

                if (x < 0 || y < 0 || x >= gridSize || y >= gridSize || board[y * gridSize + x].Type != type)
                    break;

                count++;
            }
            return count;
        }
    
        public int GetBestRow()
        {
            int bestRowIndex = -1;
            int highestPotentialScore = 0;

            for (int y = 0; y < gridSize; y++)
            {
                int potentialScore = CalculatePotentialScoreInRow(y);
                if (potentialScore > highestPotentialScore)
                {
                    highestPotentialScore = potentialScore;
                    bestRowIndex = y;
                }
            }
            Debug.Log($"this is the board {board}");
            return bestRowIndex;
        }

        public int GetBestColumn()
        {
            int bestColumnIndex = -1;
            int highestPotentialScore = 0;

            for (int x = 0; x < gridSize; x++)
            {
                int potentialScore = CalculatePotentialScoreInColumn(x);
                if (potentialScore > highestPotentialScore)
                {
                    highestPotentialScore = potentialScore;
                    bestColumnIndex = x;
                }
            }

            return bestColumnIndex;
        }

        // Other methods (GetMatches, GetTotalMatches, IsMatchAtPosition, etc.)
        // ...

        private int CalculatePotentialScoreInRow(int rowIndex)
        {
            int potentialScore = 0;
            for (int x = 0; x < gridSize; x++)
            {
                // Check horizontal swaps (left and right)
                if (x > 0) { // Can swap with left
                    potentialScore = Math.Max(potentialScore, CheckPotentialMatchInRow(rowIndex, x, x - 1));
                }
                if (x < gridSize - 1) { // Can swap with right
                    potentialScore = Math.Max(potentialScore, CheckPotentialMatchInRow(rowIndex, x, x + 1));
                }

                // Check vertical swaps (up and down)
                if (rowIndex > 0) { // Can swap with tile above
                    potentialScore = Math.Max(potentialScore, CheckPotentialMatchInRow(rowIndex - 1, x, rowIndex));
                }
                if (rowIndex < gridSize - 1) { // Can swap with tile below
                    potentialScore = Math.Max(potentialScore, CheckPotentialMatchInRow(rowIndex + 1, x, rowIndex));
                }
            }
            return potentialScore;
        }



        private int CalculatePotentialScoreInColumn(int columnIndex)
        {
            int potentialScore = 0;
            for (int y = 0; y < gridSize - 1; y++)
            {
                // Check potential score by swapping with the tile below
                int scoreWithSwapDown = CheckPotentialMatchInColumn(columnIndex, y, y + 1);

                // Check potential score by swapping with the tile above (for all except the first row)
                int scoreWithSwapUp = y > 0 ? CheckPotentialMatchInColumn(columnIndex, y, y - 1) : 0;

                // Determine the highest potential score from either swap
                potentialScore = Math.Max(potentialScore, Math.Max(scoreWithSwapDown, scoreWithSwapUp));
            }
            return potentialScore;
        }

// Overload for vertical swaps
        private int CheckPotentialMatchInRow(int swapRow1, int col, int swapRow2)
        {
            // Swap vertically
            SwapTiles(swapRow1, col, swapRow2, col);

            int score = 0;
            // Calculate potential score after the swap
            score += CheckMatchesAtPosition(swapRow1, col);
            score += CheckMatchesAtPosition(swapRow2, col);

            // Swap back
            SwapTiles(swapRow1, col, swapRow2, col);

            return score;
        }
    
        private int CheckPotentialMatchInColumn(int columnIndex, int rowIndex1, int rowIndex2)
        {
            // Swap tiles
            SwapTiles(rowIndex1, columnIndex, rowIndex2, columnIndex);

            // Calculate the potential score based on matches formed by the swap
            int scoreAfterSwap = CheckMatchesAtPosition(rowIndex1, columnIndex) + CheckMatchesAtPosition(rowIndex2, columnIndex);

            // Swap back to the original state
            SwapTiles(rowIndex1, columnIndex, rowIndex2, columnIndex);

            return scoreAfterSwap;
        }

        private void SwapTiles(int row1, int col1, int row2, int col2)
        {
            var temp = board[row1 * gridSize + col1];
            board[row1 * gridSize + col1] = board[row2 * gridSize + col2];
            board[row2 * gridSize + col2] = temp;
        }

        private int CheckMatchesAtPosition(int row, int col)
        {
            int totalScore = 0;

            // Check for horizontal matches
            totalScore += CheckMatchInDirection(row, col, 1, 0) + CheckMatchInDirection(row, col, -1, 0);

            // Check for vertical matches
            var matchesInDir1 = CheckMatchInDirection(row, col, 0, 1);
            var matchesInDir2 = CheckMatchInDirection(row, col, 0, -1);
            totalScore += matchesInDir1+ matchesInDir2;

            // Add the score of the starting tile itself
            if (totalScore > 0) totalScore += DetermineScoreByType(board[row * gridSize + col].Type);

            return totalScore;
        }

        private int CheckMatchInDirection(int row, int col, int dx, int dy)
        {
            TileType currentType = board[row * gridSize + col].Type;
            int matchLength = 1; // Include the current tile

            // Check in the specified direction
            matchLength += CountMatchingTilesInDirection(row, col, dx, dy, currentType);

            // Check in the opposite direction
            matchLength += CountMatchingTilesInDirection(row, col, -dx, -dy, currentType);

            // Calculate score only if a match is formed (3 or more tiles)
            if (matchLength >= 3)
            {
                return matchLength * DetermineScoreByType(currentType);
            }
            return 0;
        }

        private int CountMatchingTilesInDirection(int row, int col, int dx, int dy, TileType type)
        {
            int count = 0;
            row += dy;
            col += dx;
            while (IsValidPosition(row, col) && board[row * gridSize + col].Type == type)
            {
                count++;
                row += dy;
                col += dx;
            }
            return count;
        }


        private bool IsValidPosition(int row, int col)
        {
            return row >= 0 && row < gridSize && col >= 0 && col < gridSize;
        }


        private int CheckLineMatch(int row, int col, bool isHorizontal)
        {
            TileType currentTileType = board[row * gridSize + col].Type;
            int matchCount = 1; // Start with the current tile

            // Check in one direction (left or up)
            matchCount += CheckInDirection(row, col, -1, currentTileType, isHorizontal);

            // Check in the opposite direction (right or down)
            matchCount += CheckInDirection(row, col, 1, currentTileType, isHorizontal);

            if (matchCount >= 3) // Minimum number of tiles for a match
            {
                return matchCount * DetermineScoreByType(currentTileType);
            }

            return 0;
        }

        private int CheckInDirection(int row, int col, int step, TileType type, bool isHorizontal)
        {
            int count = 0;
            int dx = isHorizontal ? step : 0;
            int dy = isHorizontal ? 0 : step;

            while (true)
            {
                col += dx;
                row += dy;

                if (col < 0 || col >= gridSize || row < 0 || row >= gridSize)
                    break;

                if (board[row * gridSize + col].Type != type)
                    break;

                count++;
            }

            return count;
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
