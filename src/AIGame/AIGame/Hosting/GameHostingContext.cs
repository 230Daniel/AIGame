using System;
using System.Collections.Generic;
using System.Reflection;
using AIGame.Game;

namespace AIGame.Hosting
{
    public sealed class GameHostingContext
    {
        private Type _defaultAgentType;
        
        public Type DefaultAgentType
        {
            get => _defaultAgentType;
            set
            {
                if (!value.IsAssignableTo(typeof(Agent)))
                    throw new ArgumentException("The specified type is not an agent.", nameof(value));
                if (value.IsAbstract)
                    throw new ArgumentException("The specified type is abstract so can not be used as an agent.", nameof(value));

                _defaultAgentType = value;
            }
        }
        
        public IEnumerable<Assembly> AgentAssemblies { get; set; }
    }
}
