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
        private readonly ServiceStopHelper.Occurrence _thisOccurrence;

        // board one of the upcoming services
        public BoardNode(IEnumerator<ServiceStopHelper.Occurrence> occurrences, DataService ds) : base(ds)
        {
            _occurrences = occurrences;
            occurrences.MoveNext();
            _thisOccurrence = occurrences.Current;
        }

        public override IEnumerable<Edge> Edges(DateTime at)
        {
            // board this service
            yield return new BoardEdge(_thisOccurrence.At, 0f, new ServiceStopNode(_thisOccurrence, _ds));

            // decide to board the next service
            yield return new WaitEdge(_thisOccurrence.At, 0f, new BoardNode(_occurrences, _ds));
        }

        public override int GetHashCode()
        {
            return _thisOccurrence.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is BoardNode bo && bo._thisOccurrence.Equals(_thisOccurrence);
        }
    }
}
