using System;
using System.Linq;
using System.Reflection;
using AIGame.Game;
using AIGame.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AIGame.Hosting
{
    public static class GameHostBuilderExtensions
    {
        public static IHostBuilder ConfigureGame(this IHostBuilder builder, Action<HostBuilderContext, GameHostingContext> configure = null)
        {
            builder.ConfigureServices((context, services) =>
            {
                var gameContext = new GameHostingContext();
                configure?.Invoke(context, gameContext);
                
                var assemblies = gameContext.AgentAssemblies ?? new[] {Assembly.GetEntryAssembly()};
                var agentTypes = assemblies.SelectMany(assembly => 
                    assembly.ExportedTypes.Where(type => 
                        type.IsAssignableTo(typeof(Agent)) && !type.IsAbstract))
                    .ToArray();

                if (agentTypes.Length == 0)
                    throw new InvalidOperationException("Found no agents in the entry assembly. Add a public class that inherits from Agent.");
                
                services.AddHostedService<GameRunnerService>();
                services.AddSingleton<ITurnGeneratorService, TurnGeneratorService>();
                services.Configure<GameConfiguration>(x =>
                {
                    x.DefaultAgentType = gameContext.DefaultAgentType;
                    x.AgentTypes = agentTypes.ToArray();
                });
                
                foreach (var userAgentType in agentTypes)
                    services.AddTransient(userAgentType);
            });
            
            return builder;
        }
    }
}
