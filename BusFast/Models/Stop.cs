using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusFast.Models
{
    public class Stop
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Cluster Cluster { get; set; }

        public string Bound
        {
            get
            {
                var i = Name.IndexOf('-');
                if (i == -1)
                    return null;
                else
                    return Name.Substring(i + 1).Trim();
            }
        }

        public override string ToString() => Name;
    }
}
