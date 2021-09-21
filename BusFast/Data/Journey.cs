using BusFast.Wrappers.Journey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Data
{
    public class Journey
    {
        public Journey(Cursor c)
        {
            Steps = c.Cursors.Select(ci => MakeStep(ci)).Where(s => s.Description != null).ToArray();
        }

        public Step[] Steps { get; }


        private Step MakeStep(Cursor c)
        {
            return new Step(c.Edge);
        }

        public class Step
        {
            public Step(Edge edge)
            {
                At = edge.At;
                Description = edge.Describe();
            }

            public DateTime At { get; }
            public string Description { get; }
        }
    }
}
