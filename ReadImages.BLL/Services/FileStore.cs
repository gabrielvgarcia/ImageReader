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
            ImageFormat imageFormat;

            if (base64.Contains("data:image/png;base64"))
            {
                cleandata = base64.Replace("data:image/png;base64,", "");
                fileName = "Image.png";
                imageFormat = ImageFormat.Png;
            }
            else if (base64.Contains("data:image/jpg;base64"))
            {
                cleandata = base64.Replace("data:image/jpg;base64,", "");
                fileName = "Image.jpg";
                imageFormat = ImageFormat.Jpeg;
            }
            else if (base64.Contains("data:image/jpeg;base64"))
            {
                cleandata = base64.Replace("data:image/jpeg;base64,", "");
                fileName = "Image.jpeg";
                imageFormat = ImageFormat.Jpeg;
            }
            else
            {
                return null;
            }

            byte[] data = Convert.FromBase64String(cleandata);

            MemoryStream ms = new MemoryStream(data);

            Image img = Image.FromStream(ms);

            string imagePath = Path.Combine(Environment.CurrentDirectory, @"..\ReadImages.BLL\TempImage\", fileName);

            img.Save(imagePath, imageFormat);

            return imagePath;
        }
    }
}
