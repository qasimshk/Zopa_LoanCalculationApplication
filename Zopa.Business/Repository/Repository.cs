using CsvHelper;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Zopa.Business.Repository
{
    public class Repository : Interfaces.IRepository
    {
        public IEnumerable<Entity.FileData> GetAllInvestment(string filename)
        {
            if (File.Exists(filename))
            {
                using (var read = new CsvReader(File.OpenText(filename)))
                {
                    return read.GetRecords<Entity.FileData>()
                        .Select(x => new Entity.FileData
                        {
                            Lender = x.Lender,
                            Rate = x.Rate,
                            Available = x.Available
                        })
                        .OrderBy(x => x.Rate)
                        .ToList();
                }
            }
            return default(IEnumerable<Entity.FileData>);
        }
    }
}