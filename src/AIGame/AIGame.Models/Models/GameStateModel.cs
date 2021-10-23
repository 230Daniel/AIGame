namespace AIGame.Models
{
    public class GameStateModel
    {
        public int TurnNumber { get; set; }
        public AgentModel[] Agents { get; set; }
    }
}
