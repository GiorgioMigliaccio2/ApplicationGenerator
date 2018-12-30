using System;

namespace ApplicationGenerator.General.CodeGen.BaseClasses
{
    internal class GeneratedBase
    {
        public string Description { get; set; }
        public string Version { get; set; }
        public DateTime LastModified { get; set; }
        public string LastModifiedBy { get; set; }
        public bool TenantSpecific { get; set; }
        public string Icon { get; set; }
        public bool Cache { get; set; }
        public string Notes { get; set; }
        public DateTime GenerationTime { get; set; }
    }
}
