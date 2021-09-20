using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Wrappers
{
    public abstract class JourneyState
    {
        public readonly DateTime At;

        public JourneyState(DateTime at) { At = at; }

        public abstract IEnumerable<JourneyState> NextStates { get; }
    }
}
