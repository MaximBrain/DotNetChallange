using System.Collections.Generic;
using ProjectManagementSystem.Domain.Entities;

namespace ProjectManagementSystem.Domain.Repositories
{
    public interface IItemRepository<TItem> where TItem: ItemBase
    {
        TItem Get(int id);

        IEnumerable<TItem> GetAll();
        TItem Update(TItem item);
        TItem Create(TItem item);
        void Delete(List<TItem> items);
    }
}