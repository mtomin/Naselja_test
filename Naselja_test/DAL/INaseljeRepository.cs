using Naselja_test.Models;
using System.Collections.Generic;

namespace Naselja_test.DAL
{
    public interface INaseljeRepository
    {
        List<Naselje> GetNaseljaPaged(int page, int pageSize);

        List<Naselje> GetNaselja();

        void UpdateNaselje(Naselje naselje);

        void DeleteNaselje(int naseljeID);

        void AddNaselje(Naselje naselje);

        int BrojNaselja();
    }
}
