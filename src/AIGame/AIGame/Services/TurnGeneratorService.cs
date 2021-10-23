using System;
using System.Linq;
using AIGame.Game;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AIGame.Services
{
    internal class TurnGeneratorService : ITurnGeneratorService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Type _defaultAgentType;

        public TurnGeneratorService(IServiceProvider serviceProvider, ILogger<TurnGeneratorService> logger, IOptions<GameConfiguration> options)
        {
            _serviceProvider = serviceProvider;
            _defaultAgentType = options.Value.DefaultAgentType ?? options.Value.AgentTypes.FirstOrDefault();
            
            if (options.Value.DefaultAgentType is null && options.Value.AgentTypes.Length > 1)
            {
                logger.LogWarning("More than one agent type is registered but no default was set, using {AgentType} as the default", _defaultAgentType.FullName);
            }
        }
        
        public Turn GetTurn(GameState gameState)
        {
            var turn = new Turn
            {
                AgentActions = new()
            };
            
            // Convert all default agents to the user's default agent
            for (var i = 0; i < gameState.Agents.Count; i++)
            {
                var agent = gameState.Agents[i];
                if (agent.GetType() != typeof(DummyAgent))
                    continue;
                
                var newAgent = _serviceProvider.GetService(_defaultAgentType) as Agent;
                newAgent.Id = agent.Id;
                newAgent.Position = agent.Position;
                
                gameState.Agents[i] = newAgent;
            }

            foreach (var agent in gameState.Agents)
            {
                var action = agent.GetAction(gameState);
                turn.AgentActions.Add(action);
            }

            return turn;
        }
    }
}
