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
        private readonly ILogger<TurnGeneratorService> _logger;
        private readonly GameConfiguration _options;
        private readonly Type _defaultAgentType;

        public TurnGeneratorService(IServiceProvider serviceProvider, ILogger<TurnGeneratorService> logger, IOptions<GameConfiguration> options)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _options = options.Value;
            
            _defaultAgentType = _options.DefaultAgentType ?? _options.AgentTypes.FirstOrDefault();
            
            if (_options.DefaultAgentType is null && _options.AgentTypes.Length > 1)
            {
                _logger.LogWarning("More than one agent type is registered but no default was set, using {AgentType} as the default", _defaultAgentType.FullName);
            }
        }
        
        public Turn GetTurn(GameState gameState)
        {
            var turn = new Turn();
            
            // Convert all brainless agents to the user's default agent
            for (var i = 0; i < gameState.Agents.Count; i++)
            {
                var agent = gameState.Agents[i];
                if (agent is not DefaultAgent)
                    continue;
                
                var newAgent = _serviceProvider.GetService(_defaultAgentType) as Agent;
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
