using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Domain;
using ProjectManagementSystem.Domain.Repositories;
using ProjectManagementSystem.WebApi.Services;

namespace ProjectManagementSystem.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ProjectsService _projectsService;
        private readonly TasksService _tasksService;

        public ReportController(DatabaseContext databaseContext, IMapper mapper)
        {
            var taskRepository = new TasksRepository(databaseContext);
            _projectsService = new ProjectsService(new ProjectsRepository(databaseContext), taskRepository, mapper);
            _tasksService = new TasksService(taskRepository, mapper);
        }

        [HttpGet("{date}")]
        public ActionResult Get(string date)
        {
            var projects = _projectsService.GetProjectsInProgress(DateTime.Parse(date));
            var tasks = _tasksService.GetTasksInProgress(DateTime.Parse(date));
            var reportService = new ReportService();

            return File(
                reportService.Generate(projects, tasks),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "report.xlsx");
        }
    }
}