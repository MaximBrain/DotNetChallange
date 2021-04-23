using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Entities;

namespace ProjectManagementSystem.Domain.Repositories
{
    public class ProjectsRepository:IItemRepository<Project>
    {
        private readonly DatabaseContext _databaseContext;

        public ProjectsRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Project Get(int id)
        {
            return _databaseContext.Projects.Find(id);
        }

        public IEnumerable<Project> GetAll()
        {
            return _databaseContext.Projects;
        }

        public Project Update(Project item)
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

        public Project Create(Project item)
        {
            _databaseContext.Projects.Add(item);
            _databaseContext.SaveChanges();
            return item;
        }

        public void Delete(List<Project> items)
        {
            _databaseContext.Projects.RemoveRange(items);
            _databaseContext.SaveChanges();
        }
    }
}