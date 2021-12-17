using IronOcr;
using ReadImages.BLL.Contracts;
using System;
using System.IO;

namespace ReadImages.BLL.Services
{
    public class HandleFilesService : IHandleFilesService
    {
        private readonly IFileStoreService _fileStoreService;
        public HandleFilesService(IFileStoreService fileStoreService)
        {
            _fileStoreService = fileStoreService;
        }
        public string HandleFileBase64(string base64)
        {
            string imagePath = _fileStoreService.StoreFile(base64);

            var Ocr = new IronTesseract();

            Ocr.Configuration.BlackListCharacters = "~`$#^*_}{][|\\.";
            Ocr.Configuration.PageSegmentationMode = TesseractPageSegmentationMode.Auto;
            Ocr.Configuration.TesseractVersion = TesseractVersion.Tesseract5;
            Ocr.Configuration.EngineMode = TesseractEngineMode.LstmOnly;
            Ocr.Language = OcrLanguage.PortugueseBest;
            using (var Input = new OcrInput(imagePath))
            {
                var Result = Ocr.Read(Input).Text;

                Result = Result.Replace("\n", "").Replace("\r", "").Replace(".", "").Replace(",", "").Replace(" ", "");

                File.Delete(imagePath);

                string start = FindGateway(Result);

                Result = FindIndex(Result, start);

                return Result;
            }
        }
        public string FindIndex(string strSource, string strStart)
        {
            if (strSource.Contains(strStart))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length - 4;
                End = Start + 47;
                return strSource.Substring(Start, End - Start);
            }

            return "";
        }

        public string FindGateway(string strSource)
        {
            _ = strSource switch
            {
                string when strSource.Contains("0019") => strSource = "0019",
                string when strSource.Contains("0339") => strSource = "0339",
                string when strSource.Contains("7489") => strSource = "7489",
                _ => throw new NotImplementedException()
            };

            return strSource;
        }
    }
}
