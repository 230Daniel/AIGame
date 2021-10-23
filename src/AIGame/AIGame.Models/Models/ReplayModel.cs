namespace AIGame.Models
{
    public class ReplayModel
    {
        public GameStateModel InitialGameState { get; set; }
        public TurnModel[] Turns { get; set; }
        public GameStateModel FinalGameState { get; set; }
    }
}
