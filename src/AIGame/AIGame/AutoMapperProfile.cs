using System.Linq;
using System.Reflection;
using AIGame.Game;
using AIGame.Models;
using AutoMapper;

namespace AIGame
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Agent, AgentModel>();
            foreach (var agentActionType in typeof(AgentAction).Assembly.GetTypes().Where(x => x.IsAssignableTo(typeof(AgentAction)) && !x.IsAbstract))
                CreateMap(agentActionType, typeof(AgentActionModel));
            CreateMap<Turn, TurnModel>();
            CreateMap<GameState, GameStateModel>();
        }
    }
}
