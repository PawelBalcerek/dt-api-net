using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class FlywaySchemaHistory
    {
        public int InstalledRank { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Script { get; set; }
        public int? Checksum { get; set; }
        public string InstalledBy { get; set; }
        public DateTime InstalledOn { get; set; }
        public int ExecutionTime { get; set; }
        public bool Success { get; set; }
    }
}
