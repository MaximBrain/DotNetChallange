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
    public class TasksServiceTest
    {
        private const int TaskId = 1;
        private Mock<IItemRepository<Task>> _tasksRepositoryMock;
        private Mock<IMapper> _mapperMock;

        private TasksService _tasksService;

        [SetUp]
        public void Init()
        {
            _tasksRepositoryMock = new Mock<IItemRepository<Task>>();
            _mapperMock = new Mock<IMapper>();

            _tasksService = new TasksService(_tasksRepositoryMock.Object, _mapperMock.Object);
        }

        [Test(Description = "Root task with sub task")]
        public void GetAllItemsTest()
        {
            var rootTask = new Task { Id = TaskId };
            var subTask = new Task { Id = 2, ParentId = TaskId };
            var tasks = new List<Task> { rootTask, subTask };

            _tasksRepositoryMock.Setup(tasksRepository => tasksRepository.GetAll()).Returns(tasks);
            // TODO: Setup mapper Mock

            var actualTasks = _tasksService.GetAllItems().ToList();

            // TODO: Verify _tasksRepositoryMock GetAll

            Assert.AreEqual(1, actualTasks.Count);

            var actualTask = actualTasks.Single();
            Assert.AreEqual(TaskId, actualTask.Id);
            Assert.AreEqual(2, actualTask.SubTasks.Single().Id);
        }

        [Test]
        [TestCase(true, Description = "Task exists")]
        [TestCase(false, Description = "Task doesn't exist")]
        public void GetItemTest(bool isExists)
        {
            var rootTask = new Task { Id = TaskId };
            if (isExists)
            {
                _tasksRepositoryMock.Setup(tasksRepository => tasksRepository.Get(TaskId)).Returns(rootTask);
                _tasksRepositoryMock.Setup(tasksRepository => tasksRepository.GetAll()).Returns(Enumerable.Empty<Task>());
                // TODO: Setup mapper Mock
            }
            else
            {
                _tasksRepositoryMock.Setup(tasksRepository => tasksRepository.Get(TaskId)).Returns((Task)null);
            }

            var actualTask = _tasksService.GetItem(TaskId);

            _tasksRepositoryMock.Verify(tasksRepository => tasksRepository.Get(TaskId), Times.Once);
            if (isExists)
            {
                _tasksRepositoryMock.Verify(tasksRepository => tasksRepository.GetAll(), Times.Once);
                Assert.AreEqual(TaskId, actualTask.Id);
            }
            else
            {
                Assert.AreEqual(null, actualTask);
            }
        }

        [Test]
        // TODO: Add Test cases
        public void GetTasksInProgressTest(bool isInDates, State state, bool isSubTask)
        {
            var tasks = new List<Task>()
            {
                new Task()
                {
                    Id = TaskId,
                    ParentId = isSubTask ? 2: (int?)null,
                    StartDate = new DateTime(2020, 1,1),
                    FinishDate = new DateTime(2020, 1,3),
                    State = (int)state
                }
            };
            _tasksRepositoryMock.Setup(tasksRepository => tasksRepository.GetAll()).Returns(tasks);
            // TODO: Setup mapper Mock

            var taskModels = _tasksService.GetTasksInProgress(new DateTime(2020, isInDates ? 1: 2, 1));

            if (isInDates && state == State.InProgress && !isSubTask)
            {
                Assert.AreEqual(1, taskModels.Count);
            }
            else
            {
                Assert.AreEqual(0, taskModels.Count);
            }
        }
    }
}