using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ProjectManagementSystem.Domain.Entities;
using ProjectManagementSystem.Domain.Repositories;
using ProjectManagementSystem.WebApi.Models;

namespace ProjectManagementSystem.WebApi.Services
{
    public class TasksService: ItemServiceBase<TaskModel, Task>
    {
        public TasksService(IItemRepository<Task> tasksRepository, IMapper autoMapper) : base(tasksRepository, autoMapper)
        {
        }

        public override IEnumerable<TaskModel> GetAllItems()
        {
            var tasks = Repository.GetAll().Where(task=>task.ParentId == null).ToList();

            foreach (var task in tasks)
            {
                yield return GetModelItem(task);
            }
        }

        public override TaskModel GetItem(int id)
        {
            var task = Repository.Get(id);
            return task == null? null: GetModelItem(task);
        }

        public List<TaskModel> GetTasksInProgress(DateTime date)
        {
            var tasks = Repository.GetAll().Where(task => true /* TODO: Add condition according to requirements */);

            return tasks.Select(GetModelItem).ToList();
        }

        private TaskModel GetModelItem(Task task)
        {
            var subTasks = AutoMapper.Map<List<TaskModel>>(Repository.GetAll().Where(t => t.ParentId == task.Id).ToList());
            return new TaskModel(
                task.Id,
                task.Name,
                task.Description,
                task.StartDate,
                task.FinishDate,
                (State)task.State,
                subTasks);
        }
    }
}