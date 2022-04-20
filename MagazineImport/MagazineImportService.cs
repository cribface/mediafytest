using MagazineImport.Code.Importers;
using System.Collections.Generic;

namespace MagazineImport
{
    public class MagazineImportService
    {
        public void Run()
        {
            var importers = new List<BaseMultiImporter>
            {
                new PrenaxImporter()
            };

            foreach (var import in importers)
            {
                import.Import();
            }
        }
    }
}