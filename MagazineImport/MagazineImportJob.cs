using MagazineImport.Code.Importers;
using MagazineImport.Core;
using System.Collections.Generic;

namespace MagazineImport
{
    public class MagazineImportJob
    {
        public void Run()
        {
            var importers = new List<IImporter>
            {
                //new PrenaxImporter() // TODO: IoC
            };

            foreach (var import in importers)
            {
                import.Import();
            }
        }
    }
}