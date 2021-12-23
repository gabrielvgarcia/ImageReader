using IronOcr;
using ReadImages.BLL.Contracts;
using ReadImages.DTO.Enums;
using ReadImages.DTO.Extensions;
using System;
using System.IO;
using System.Linq;

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
            string documentPath = _fileStoreService.StoreFile(base64);

            var Ocr = new IronTesseract();

            Ocr.Configuration.BlackListCharacters = "~`$#^*_}{][|\\.";
            Ocr.Configuration.PageSegmentationMode = TesseractPageSegmentationMode.Auto;
            Ocr.Configuration.TesseractVersion = TesseractVersion.Tesseract5;
            Ocr.Configuration.EngineMode = TesseractEngineMode.LstmOnly;
            Ocr.Language = OcrLanguage.PortugueseBest;
            using (var Input = new OcrInput(documentPath))
            {
                string Result = Ocr.Read(Input).Text;

                Result = new string(Result.Where(c => char.IsDigit(c)).ToArray());

                File.Delete(documentPath);

                string start = FindGateway(Result);

                Result = FindIndex(Result, start);

                return Result;
            }
        }
        public string FindIndex(string strSource, string strStart)
        {
            if (strSource.Contains(strStart))
            {
                int start, end;
                start = strSource.IndexOf(strStart, 0) + strStart.Length - 4;
                end = start + 47;
                return strSource.Substring(start, end - start);
            }

            return "";
        }

        public string FindGateway(string strSource)
        {
            _ = strSource switch
            {
                string when strSource.Contains(GatewayType.BB.GetDescription()) => strSource = GatewayType.BB.GetDescription(),
                string when strSource.Contains(GatewayType.SANTANDER.GetDescription()) => strSource = GatewayType.SANTANDER.GetDescription(),
                string when strSource.Contains(GatewayType.SICREDI.GetDescription()) => strSource = GatewayType.SICREDI.GetDescription(),
                _ => throw new Exception(MappedErrors.GATEWAYNOTFOUND.GetDescription())
            };

            return strSource;
        }
    }
}
