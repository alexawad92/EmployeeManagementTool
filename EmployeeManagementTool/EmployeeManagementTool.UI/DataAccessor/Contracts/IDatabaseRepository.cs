using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementTool.DataAccessor.Contracts
{
    public interface IDatabaseRepository<T>
    {
        Task<IEnumerable<T>> GetAllEntitiesAsync();
        Task<T> GetEntityByIdAsync(int entityId);
        Task SaveChangesAsync();
        bool HasChanges();
        void Add(T entity);
        void Remove(T entity);
    }
}
