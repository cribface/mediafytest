using Serilog;
using System;
using System.IO;

namespace MagazineImport.Code.Helpers
{
    public class FileHelper
    {
        public static bool MoveFile(string strFilePath, string strNewPath)
        {
            var bitResult = false;
            try
            {
                File.Move(strFilePath, strNewPath);
                bitResult = true;
            }
            catch (Exception ex)
            {
                Log.Logger?.Error(ex, "MoveFile Failed. \nFrom Path: {FilePath}\nTo Path:{NewFilePath}", strFilePath, strNewPath);
            }
            
            return bitResult;
        }

        public static string GetValidFileName(string strNewPath, string strFileName)
        {
            var strFileNameNoExt = Path.GetFileNameWithoutExtension(strFileName);
            var strExtension = Path.GetExtension(strFileName);

            var strCount = string.Empty;
            for (var i = 1; File.Exists(Path.Combine(strNewPath, strFileNameNoExt + strCount + strExtension)); i++)
                strCount = " (" + i + ")";
            return Path.Combine(strNewPath, strFileNameNoExt + strCount + strExtension);
        }
    }
}
