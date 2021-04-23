using AutoMapper;
using ProjectManagementSystem.Domain;
using ProjectManagementSystem.Domain.Repositories;
using ProjectManagementSystem.WebApi.Models;
using ProjectManagementSystem.WebApi.Services;

namespace ProjectManagementSystem.WebApi.Controllers
{
    public class ProjectsController: CrudControllerBase<ProjectModel>
    {
        public ProjectsController(DatabaseContext databaseContext, IMapper mapper)
            :base(new ProjectsService(new ProjectsRepository(databaseContext), new TasksRepository(databaseContext), mapper))
        {
        }
    }
}