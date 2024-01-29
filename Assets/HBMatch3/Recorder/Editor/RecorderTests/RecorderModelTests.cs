using NUnit.Framework;

namespace HBMatch3.Recorder.Editor.RecorderTests
{
    public class RecorderModelTests
    {
        [Test]
        public void RecordTileDestroyed_IncrementsTilesDestroyedCount()
        {
            // Arrange
            var recorder = new RecorderModel();

            // Act
            recorder.RecordTileDestroyed(); // Record one tile destroyed

            // Assert
            Assert.AreEqual(1, recorder.GetTilesDestroyed(), "Tiles destroyed count should be 1 after recording one tile destroyed.");
        }

        [Test]
        public void RecordMove_IncrementsMovesPlayedCount()
        {
            // Arrange
            var recorder = new RecorderModel();

            // Act
            recorder.RecordMove(); // Record one move

            // Assert
            Assert.AreEqual(1, recorder.GetMovesPlayed(), "Moves played count should be 1 after recording one move.");
        }

        [Test]
        public void MultipleRecords_CorrectlyTracksTilesDestroyedAndMoves()
        {
            // Arrange
            var recorder = new RecorderModel();
            int expectedTilesDestroyed = 5;
            int expectedMovesPlayed = 3;

            // Act
            for (int i = 0; i < expectedTilesDestroyed; i++)
            {
                recorder.RecordTileDestroyed();
            }
            for (int i = 0; i < expectedMovesPlayed; i++)
            {
                recorder.RecordMove();
            }

            // Assert
            Assert.AreEqual(expectedTilesDestroyed, recorder.GetTilesDestroyed(), "Tiles destroyed count should match the number of recorded destroyed tiles.");
            Assert.AreEqual(expectedMovesPlayed, recorder.GetMovesPlayed(), "Moves played count should match the number of recorded moves.");
        }
    }
}