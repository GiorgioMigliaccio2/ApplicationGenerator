using System;
namespace ApplicationGenerator.General.Models
{
    public class ObjectModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string version { get; set; }
        public DateTime lastmodified { get; set; }
        public string lastmodifiedby { get; set; }
        public bool tenantspecific { get; set; }
        public ObjectType type { get; set; }
        public string icon { get; set; }
        public bool cache { get; set; }
        public string notes { get; set; }
        public object datamodel { get; set; }
    }

}
