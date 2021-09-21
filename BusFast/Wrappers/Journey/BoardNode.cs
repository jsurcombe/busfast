using BusFast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Wrappers.Journey
{
    public class BoardNode : Node
    {
        private readonly IEnumerator<ServiceStopHelper.Occurrence> _occurrences;
        private readonly ServiceStopHelper.Occurrence _thisStop;

        // board one of the upcoming services
        public BoardNode(IEnumerator<ServiceStopHelper.Occurrence> occurrences, DataService ds) : base(ds)
        {
            _occurrences = occurrences;
            occurrences.MoveNext();
            _thisStop = occurrences.Current;
        }

        public override IEnumerable<Edge> Edges(DateTime at)
        {
            // board this service
            yield return new Edge(_thisStop.At, 0f, new ServiceStopNode(_thisStop, _ds));

            // decide to board the next service
            yield return new Edge(_thisStop.At, 0f, new BoardNode(_occurrences, _ds));
        }

        public override int GetHashCode()
        {
            return _thisStop.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is BoardNode bo && bo._thisStop.Equals(_thisStop);
        }
    }
}
