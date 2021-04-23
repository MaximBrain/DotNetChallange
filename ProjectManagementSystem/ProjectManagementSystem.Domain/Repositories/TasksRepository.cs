using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Entities;

namespace ProjectManagementSystem.Domain.Repositories
{
    public class TasksRepository : IItemRepository<Task>
    {
        private readonly DatabaseContext _databaseContext;

        public TasksRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public Task Get(int id)
        {
            return _databaseContext.Tasks.Find(id);
        }

        public IEnumerable<Task> GetAll()
        {
            return _databaseContext.Tasks;
        }

        public Task Update(Task item)
        {
            _databaseContext.Entry(item).State = EntityState.Modified;
            try
            {
                _databaseContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }

            return item;
        }

        public Task Create(Task item)
        {
            _databaseContext.Tasks.Add(item);
            _databaseContext.SaveChanges();
            return item;
        }

        public void Delete(List<Task> items)
        {
            _databaseContext.Tasks.RemoveRange(items);
            _databaseContext.SaveChanges();
        }
    }
}