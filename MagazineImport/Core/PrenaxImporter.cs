using Serilog;
using SmartXLS;

namespace MagazineImport.Core
{
    public class PrenaxImporter : IImporter
    {
        private readonly MagazineFileService _magazineFileService;
        private readonly MagazineMapper _magazineMapper;
        private readonly MagazineRepository _magazineRepository;

        public PrenaxImporter(MagazineFileService magazineFileService, MagazineMapper magazineMapper, MagazineRepository magazineRepository)
        {
            _magazineFileService = magazineFileService;
            _magazineMapper = magazineMapper;
            _magazineRepository = magazineRepository;
        }

        public void Import()
        {
            foreach (var fileStream in _magazineFileService.GetAllFileStreams())
            {
                Log.Logger?.Information("File: {PrenaxImportFileName}", fileStream.Name);

                using (var workBook = new WorkBook())
                {
                    if (fileStream.Name.EndsWith(".xlsx"))
                    {
                        workBook.readXLSX(fileStream);
                    }
                    else
                    {
                        workBook.read(fileStream);
                    }

                    var magazines = _magazineMapper.FromWorkBook(workBook);

                    foreach (var magazine in magazines)
                    {
                        _magazineRepository.ImportMagazine(magazine);
                    }
                }

                //ArchiveAndLog(fileStream, strPathArchive);
            }
        }
    }
}
