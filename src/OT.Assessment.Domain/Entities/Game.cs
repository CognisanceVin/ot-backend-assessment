using OT.Assessment.Domain.Entities.Common;

namespace OT.Assessment.Domain.Entities
{
    public class Game : AuditTrackingBase
    {

        public string Name { get; set; } = string.Empty;
        public string GameCode { get; set; } = string.Empty;

    }
}
