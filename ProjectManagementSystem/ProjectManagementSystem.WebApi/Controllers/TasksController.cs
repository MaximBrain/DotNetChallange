using AutoMapper;
using ProjectManagementSystem.Domain;
using ProjectManagementSystem.Domain.Repositories;
using ProjectManagementSystem.WebApi.Models;
using ProjectManagementSystem.WebApi.Services;

namespace ProjectManagementSystem.WebApi.Controllers
{
    public class TasksController : CrudControllerBase<TaskModel>
    {
        public TasksController(DatabaseContext databaseContext, IMapper mapper) :base(new TasksService(new TasksRepository(databaseContext), mapper))
        {
        }
    }
}