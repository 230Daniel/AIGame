namespace AIGame.Game
{
    internal interface ITurnGeneratorService
    {
        public Turn GetTurn(GameState gameState);
    }
}
