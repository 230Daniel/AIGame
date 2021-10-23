﻿using System.Collections.Generic;

namespace AIGame.Game
{
    internal class Turn
    {
        public List<AgentAction> AgentActions { get; }

        public Turn()
        {
            AgentActions = new();
        }
    }
}
