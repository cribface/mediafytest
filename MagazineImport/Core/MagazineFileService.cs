using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MagazineImport.Core
{
    public class MagazineFileService
    {
        private const string UploadPath = @"C:\ftpImport\MagazineImport\upload";
        private const string ArchivePath = @"C:\ftpImport\MagazineImport\archive";
        public const string FilePrefix = "prenax_";

        public IEnumerable<FileStream> GetAllFileStreams()
        {
            CreateDirectoriesIfNotExists();

            var filePaths = Directory.GetFiles(UploadPath)
                .Where(s => (s.EndsWith(".xls") || s.EndsWith(".xlsx")) &&
                            Path.GetFileName(s).StartsWith(FilePrefix))
                .ToList();

            foreach (var filePath in filePaths)
            {
                yield return new FileStream(filePath, FileMode.Open);
            }
        }

        private static void CreateDirectoriesIfNotExists()
        {
            if (!Directory.Exists(UploadPath))
            {
                Directory.CreateDirectory(UploadPath);
            }

            if (!Directory.Exists(ArchivePath))
            {
                Directory.CreateDirectory(ArchivePath);
            }
        }
    }
}