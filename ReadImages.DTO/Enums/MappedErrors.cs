using System.ComponentModel;

namespace ReadImages.DTO.Enums
{
    public enum MappedErrors
    {
        [Description("Could not find the digitable line on the proof of payment sent.")]
        GATEWAYNOTFOUND = 1,
        [Description("file extension is not supported.")]
        DOCUMENTINVALID = 2
    }
}
