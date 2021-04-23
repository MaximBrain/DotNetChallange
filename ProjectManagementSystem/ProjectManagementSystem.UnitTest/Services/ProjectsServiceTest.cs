using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Moq;
using NUnit.Framework;
using ProjectManagementSystem.Domain.Entities;
using ProjectManagementSystem.Domain.Repositories;
using ProjectManagementSystem.WebApi.Models;
using ProjectManagementSystem.WebApi.Services;

namespace ProjectManagementSystem.UnitTest.Services
{
    [TestFixture]
    public class ProjectsServiceTest
    {
        private const int ProjectId = 1;
        private Mock<IItemRepository<Project>> _projectsRepositoryMock;
        private Mock<IItemRepository<Task>> _tasksRepositoryMock;
        private Mock<IMapper> _mapperMock;

        private ProjectsService _projectsService;

        [SetUp]
        public void Init()
        {
            _projectsRepositoryMock = new Mock<IItemRepository<Project>>();
            _tasksRepositoryMock = new Mock<IItemRepository<Task>>();
            _mapperMock = new Mock<IMapper>();

            _projectsService = new ProjectsService(_projectsRepositoryMock.Object, _tasksRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        // TODO: Add test cases
        public void GetItemTest(bool isProjectExists, bool isTasksExist, int? expectedId)
        {
            _projectsRepositoryMock.Setup(repository => repository.Get(ProjectId)).Returns(isProjectExists ? new Project() { Id = ProjectId } : null);
            GetProjectMock(isProjectExists, isTasksExist, ProjectId);

            var actualProject = _projectsService.GetItem(ProjectId);

            Assert.AreEqual(expectedId, actualProject?.Id);

            _projectsRepositoryMock.Verify(projectsRepository => projectsRepository.Get(ProjectId), Times.Once);
            VerifyMocks(isProjectExists, isTasksExist);
        }

        [Test(Description = "Root project with sub project")]
        public void GetAllItemsTest()
        {
            var rootProject = new Project()
            {
                Id = ProjectId
            };
            var subProject = new Project()
            {
                Id = 2,
                ParentId = ProjectId
            };
            var projects = new List<Project> { rootProject, subProject };

            _projectsRepositoryMock.SetupSequence(repository => repository.GetAll()).Returns(projects).Returns(projects);
            GetProjectMock(true, false, ProjectId);

            var actualProjects = _projectsService.GetAllItems().ToList();

            _projectsRepositoryMock.Verify(projectsRepository => projectsRepository.GetAll(), Times.Exactly(2));
            _tasksRepositoryMock.Verify(tasksRepository => tasksRepository.GetAll(), Times.Exactly(2));
            Assert.AreEqual(1, actualProjects.Count);

            var actualRootProject = actualProjects.Single();
            Assert.AreEqual(ProjectId, actualRootProject.Id);
            Assert.AreEqual(2, actualRootProject.SubProjects.Single().Id);
        }

        [Test]
        [TestCase(true, Description = "Date in of dates range")]
        [TestCase(false, Description = "Date out of dates range")]
        public void GetProjectsInProgressTest(bool isInDatesRange)
        {
            var projects = new List<Project>()
            {
                new Project()
                {
                    Id = ProjectId,
                    StartDate = new DateTime(2020, 1,1),
                    FinishDate = new DateTime(2020, 1,3),
                },
                new Project()
                {
                    Id = 2,
                    StartDate = new DateTime(2020, 1,4),
                    FinishDate = new DateTime(2020, 1,5),
                }
            };

            var tasks = new List<Task>()
            {
                new Task()
                {
                    ProjectId = ProjectId,
                    State = (int)State.InProgress
                },
                new Task()
                {
                    ProjectId = ProjectId,
                    State = (int)State.Planned
                }
            };

            _projectsRepositoryMock.Setup(repository => repository.GetAll()).Returns(projects);
            _tasksRepositoryMock.Setup(tasksRepository => tasksRepository.GetAll()).Returns(tasks);

            var actualProjects = _projectsService.GetProjectsInProgress(isInDatesRange ? new DateTime(2020, 1, 2) : new DateTime(2020, 2, 2));

            Assert.AreEqual(isInDatesRange ? 1: 0, actualProjects.Count);
            if (isInDatesRange)
            {
                Assert.AreEqual(ProjectId, actualProjects.Single().Id);
            }
        }

        private void GetProjectMock(bool isProjectExists, bool isTasksExist, int projectId)
        {
            if (isProjectExists)
            {
                // TODO: Add all necessary mocks
            }
        }

        private void VerifyMocks(bool isProjectExists, bool isTasksExist)
        {
            if (isProjectExists)
            {
                _tasksRepositoryMock.Verify(tasksRepository => tasksRepository.GetAll(), Times.Exactly(isTasksExist ? 2 : 1));
                if (isTasksExist)
                {
                    _mapperMock.Verify(mapper => mapper.Map<List<TaskModel>>(It.IsAny<List<Task>>()), Times.Once());
                }
            }
        }
    }
}