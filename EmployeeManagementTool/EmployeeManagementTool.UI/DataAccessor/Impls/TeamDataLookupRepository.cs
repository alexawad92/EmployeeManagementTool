using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeeManagementTool.DataAccess;
using EmployeeManagementTool.DataAccessor.Contracts;
using EmployeeManagementTool.DataModel;


namespace EmployeeManagementTool.DataAccessor.Impls
{
    public class TeamDataLookupRepository : ITeamDataLookupRepository
    {
        private readonly Func<EmployeeManagementToolDbContext> _contextCreator;

        public TeamDataLookupRepository(Func<EmployeeManagementToolDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }
        public async Task<IEnumerable<LookupItem>> GetLookupItemsAsync()
        {
            List<LookupItem> lookupItems = new List<LookupItem>();
            using (var ctx = _contextCreator())
            {
                var teams = await Task.Run(() => ctx.Teams.AsNoTracking().ToListAsync());
                foreach (var team in teams)
                {
                    lookupItems.Add(new LookupItem()
                    {
                        DisplayMember = team.Name,
                        Id = team.Id
                    });
                }
            }
            return lookupItems;
        }
    }
}