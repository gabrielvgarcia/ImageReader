using System.ComponentModel;

namespace ReadImages.DTO.Enums
{
    public enum EnumDocumentType
    {
        [Description("data:image/png;base64,")]
        PNG = 1,
        [Description("data:image/jpg;base64,")]
        JPG = 2,
        [Description("data:image/jpeg;base64,")]
        JPEG = 3,
        [Description("data:application/pdf;base64,")]
        PDF = 4
    }
}
