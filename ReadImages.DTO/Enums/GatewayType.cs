using System.ComponentModel;

namespace ReadImages.DTO.Enums
{
    public enum GatewayType
    {
        [Description("0019")]
        BB = 3,
        [Description("0339")]
        SANTANDER = 5,
        [Description("7489")]
        SICREDI = 7,
        [Description("2379")]
        BRADESCO = 28
    }
}
