using ReadImages.BLL.Contracts;
using ReadImages.DTO.Enums;
using ReadImages.DTO.Extensions;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ReadImages.BLL.Services
{
    public class FileStore : IFileStoreService
    {
        public string StoreFile(string base64)
        {
            string cleandata, fileName, type;
            ImageFormat imageFormat = null;

            type = FindType(base64);

            switch (type.ToEnum<EnumDocumentType>())
            {
                case EnumDocumentType.PNG:
                    cleandata = base64.Replace(EnumDocumentType.PNG.GetDescription(), "");
                    fileName = "Document.png";
                    imageFormat = ImageFormat.Png;
                    break;
                case EnumDocumentType.JPG:
                    cleandata = base64.Replace(EnumDocumentType.JPG.GetDescription(), "");
                    fileName = "Document.jpg";
                    imageFormat = ImageFormat.Jpeg;
                    break;
                case EnumDocumentType.JPEG:
                    cleandata = base64.Replace(EnumDocumentType.JPEG.GetDescription(), "");
                    fileName = "Document.jpeg";
                    imageFormat = ImageFormat.Jpeg;
                    break;
                case EnumDocumentType.PDF:
                    cleandata = base64.Replace(EnumDocumentType.PDF.GetDescription(), "");
                    fileName = "Document.pdf";
                    break;

                default:
                    throw new Exception(MappedErrors.DOCUMENTINVALID.GetDescription());
            }

            return SaveDocument(cleandata, fileName, imageFormat);

        }
        public string SaveDocument(string cleandata, string fileName, ImageFormat imageFormat = null)
        {
            byte[] data = Convert.FromBase64String(cleandata);

            string documentPath = Path.Combine(Environment.CurrentDirectory, @"..\ReadImages.BLL\TempDocument\", fileName); ;

            if (fileName != "Document.pdf")
            {
                MemoryStream ms = new(data);

                Image img = Image.FromStream(ms);

                img.Save(documentPath, imageFormat);
            }
            else
                File.WriteAllBytes(documentPath, data);

            return documentPath;
        }
        public string FindType(string base64)
        {
            int start = base64.IndexOf(";base64,", 0) + 8;
            string response = base64.Remove(start);

            if (response == null)
                throw new Exception(MappedErrors.DOCUMENTINVALID.GetDescription());

            return response;
        }
    }
}
