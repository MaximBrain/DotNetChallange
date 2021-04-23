using System.Collections.Generic;
using ProjectManagementSystem.WebApi.Models;

namespace ProjectManagementSystem.WebApi.Services
{
    public interface IItemService<TItemModel> where TItemModel: ItemModelBase
    {
        IEnumerable<TItemModel> GetAllItems();
        TItemModel GetItem(int id);
        TItemModel CreateItem(TItemModel item);
        TItemModel UpdateItem(TItemModel item);
        void DeleteItem(TItemModel id);
    }
}