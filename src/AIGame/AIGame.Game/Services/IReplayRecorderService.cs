namespace AIGame.Game
{
    internal interface IReplayRecorderService
    {
        public void RecordInitialGameState(GameState gameState);

        public void RecordTurn(Turn turn);

        public void RecordFinalGameState(GameState gameState);

        public void Save();
    }
}
