using System.Collections.Generic;

namespace Zopa.Business.Interfaces
{
    public interface IRepository
    {
        IEnumerable<Entity.FileData> GetAllInvestment(string filename);
    }
}