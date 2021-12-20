using System.Drawing.Imaging;

namespace ReadImages.BLL.Contracts
{
    public interface IFileStoreService
    {
        public string StoreFile(string base64);
        public string SaveDocument(string cleandata, string fileName, ImageFormat imageFormat = null);
    }
}
