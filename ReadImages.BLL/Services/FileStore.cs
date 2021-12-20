using ReadImages.BLL.Contracts;
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
            string cleandata, fileName;
            ImageFormat imageFormat = null;

            if (base64.Contains("data:image/png;base64"))
            {
                cleandata = base64.Replace("data:image/png;base64,", "");
                fileName = "Document.png";
                imageFormat = ImageFormat.Png;
            }
            else if (base64.Contains("data:image/jpg;base64"))
            {
                cleandata = base64.Replace("data:image/jpg;base64,", "");
                fileName = "Document.jpg";
                imageFormat = ImageFormat.Jpeg;
            }
            else if (base64.Contains("data:image/jpeg;base64"))
            {
                cleandata = base64.Replace("data:image/jpeg;base64,", "");
                fileName = "Document.jpeg";
                imageFormat = ImageFormat.Jpeg;
            }
            else if (base64.Contains("data:application/pdf;base64,"))
            {
                cleandata = base64.Replace("data:application/pdf;base64,", "");
                fileName = "Document.pdf";
            }
            else
            {
                return null;
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
    }
}
