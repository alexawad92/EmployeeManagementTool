using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeeManagementTool.DataAccess;
using EmployeeManagementTool.DataAccessor.Contracts;


namespace EmployeeManagementTool.DataAccessor.Impls
{
    public class DatabaseRepository<T>: IDatabaseRepository<T>
        where T : class
    {
        protected readonly EmployeeManagementToolDbContext Context;

        public DatabaseRepository(EmployeeManagementToolDbContext context)
        {
            Context = context;
        }
        public virtual async Task<IEnumerable<T>> GetAllEntitiesAsync()
        {
            return await Task.Run(() => Context.Set<T>().ToListAsync());
        }

        public async Task<T> GetEntityByIdAsync(int entityId)
        {
            return await Task.Run(() => Context.Set<T>().FindAsync(entityId));
        }

        public async Task SaveChangesAsync()
        {
           await Context.SaveChangesAsync();
        }
    
        public bool HasChanges()
        {
            return Context.ChangeTracker.HasChanges();
        }

        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }
        public void Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

    }
}
