using Symo.Library.Extensibility.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symo.Library.Extensibility.Common
{
    public class DefaultConfiguration : IConfiguration
    {
        public int ChallengeDelay { get { return 5000; } }

        public int FailThreshold { get { return 3; } }
    }
}
