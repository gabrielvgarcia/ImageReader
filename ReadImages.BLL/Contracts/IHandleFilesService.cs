namespace ReadImages.BLL.Contracts
{
    public interface IHandleFilesService
    {
        public string HandleFileBase64(string base64);
        public string FindIndex(string strSource, string strStart);
        public string FindGateway(string strSource);
    }
}
