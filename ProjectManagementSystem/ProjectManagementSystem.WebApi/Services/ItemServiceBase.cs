using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ProjectManagementSystem.Domain.Entities;
using ProjectManagementSystem.Domain.Repositories;
using ProjectManagementSystem.WebApi.Models;

namespace ProjectManagementSystem.WebApi.Services
{
    public abstract class ItemServiceBase<TItemModel, TItemEntity>: IItemService<TItemModel>
        where TItemModel: ItemModelBase
        where TItemEntity : ItemBase
    {
        protected IMapper AutoMapper { get; set; }
        protected IItemRepository<TItemEntity> Repository { get; set; }

        protected ItemServiceBase(IItemRepository<TItemEntity> repository, IMapper autoMapper)
        {
            AutoMapper = autoMapper;
            Repository = repository;
        }

        public abstract IEnumerable<TItemModel> GetAllItems();

        public abstract TItemModel GetItem(int id);

        public TItemModel CreateItem(TItemModel item)
        {
            return MapToModel(Repository.Create(MapToEntity(item)));
        }

        public TItemModel UpdateItem(TItemModel item)
        {
            return MapToModel(Repository.Update(MapToEntity(item)));
        }

        public void DeleteItem(TItemModel item)
        {
            var entitiesToRemove = Repository.GetAll().Where(t => t.ParentId == item.Id).Concat(new []{ MapToEntity(item) }).ToList();
            Repository.Delete(entitiesToRemove);
        }
        protected TItemEntity MapToEntity(TItemModel itemModel)
        {
            return AutoMapper.Map<TItemEntity>(itemModel);
        }
        protected TItemModel MapToModel(TItemEntity itemEntity)
        {
            return AutoMapper.Map<TItemModel>(itemEntity);
        }

        protected IEnumerable<TItemModel> MapToModel(IEnumerable<TItemEntity> itemEntity)
        {
            return AutoMapper.Map<IEnumerable<TItemModel>>(itemEntity);
        }

        protected bool IsInDatesRage(DateTime startDate, DateTime finishDate, DateTime date)
        {
            return startDate <= date && finishDate >= date;
        }
    }
}