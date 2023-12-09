using System;
using System.Collections.Generic;

namespace GP1.UI
{
    [Serializable]
    public class LeaderboardData
    {
        public List<LeaderboardEntry> Entries = new();
    }
}