using System;
using System.Collections.Generic;
using System.Linq;
using ProjectManager.DataLayer;
using ProjectManager.BusinessLayer;
using NUnit.Framework;
using System.Globalization;

namespace ProjectManager.Tests
{
    [TestFixture]
    public class ProjectManagerBusinessTest
    {

        #region Variables
        IUserRepository userRepository;
        List<Users> randomUsers;

        IProjectRepository projectRepository;
        List<Projects> randomProjects;


        IParentTaskRepository parentRepository;
        List<ParentTasks> randomParents;


        ITaskRepository taskRepository;
        List<TaskData> randomTasks;
        #endregion

        #region Setup
        [SetUp]
        public void Setup()
        {
            userRepository = new UserRepository();
            randomUsers = new List<Users>();
            randomUsers = SetupUsers();

            projectRepository = new ProjectRepository();
            randomProjects = new List<Projects>();
            randomProjects = SetupProjects();

            parentRepository = new ParentTaskRepository();
            randomParents = new List<ParentTasks>();
            randomParents = SetupParent();

            taskRepository = new TaskRepository();
            randomTasks = new List<TaskData>();
            randomTasks = SetupTasks();


        }


        public List<TaskData> SetupTasks()
        {
            List<TaskData> tasks = taskRepository.GetAllTasks().ToList();
            return tasks;
        }

        public List<Users> SetupUsers()
        {
            List<Users> user = userRepository.GetAllUsers().ToList();
            return user;
        }

        public List<Projects> SetupProjects()
        {
            List<Projects> project = projectRepository.GetAllProjects().ToList();
            return project;
        }

        public List<ParentTasks> SetupParent()
        {
            List<ParentTasks> ptask = parentRepository.GetAllParentTasks().ToList();
            return ptask;
        }
        #endregion


        #region Test Methods for Users

        [Test]
        public void TestAddUser()
        {

            Users newUser = new Users()
            {
                FirstName = "Sugumar",
                LastName = "K",
                EmployeeId = "CT30000"
            };

            userRepository.AddUser(newUser);
            randomUsers = SetupUsers();
            Users lastUser = randomUsers.Last();
            
            Assert.AreEqual(newUser.FirstName, lastUser.FirstName);
            Assert.AreEqual(newUser.LastName, lastUser.LastName);
            Assert.AreEqual(newUser.EmployeeId, lastUser.EmployeeId);
        }


        [Test]
        public void TestUpdateUser()
        {
            Users firstUser = randomUsers.Find(a => a.UserID == 17);

            Users UpdateUser = new Users()
            {
                UserID = firstUser.UserID,
                FirstName = "Jahir",
                LastName = "KM",
                EmployeeId = "CT40000"
            };
            userRepository.EditUser(UpdateUser);

            
            Assert.AreEqual(firstUser.FirstName, "Jahir");
            Assert.AreEqual(firstUser.LastName, "KM");
            Assert.AreEqual(firstUser.EmployeeId, "CT40000");
        }


        [Test]
        public void TestDeleteUser()
        {
            int maxUserID = randomUsers.Max(a => a.UserID);
            Users lastUser = randomUsers.Last();
            userRepository.RemoveUser(lastUser.UserID);
            
            Assert.That(maxUserID + 1, Is.GreaterThan(randomUsers.Max(a => a.UserID)));
        }

        [Test]
        public void TestGetAllUsers()
        {
            int countUser = randomUsers.Count;
            List<Users> allUsers = userRepository.GetAllUsers().ToList();  taskRepository.GetAllTasks().ToList();
            Assert.IsNotNull(allUsers);
            Assert.AreEqual(countUser, allUsers.Count);
        }

        [Test]
        public void TestGetUserById()
        {
            Users firstUser = randomUsers.First();
            Users getUser = userRepository.GetUserById(firstUser.UserID);
                       
            Assert.IsNotNull(getUser);
            Assert.AreEqual(firstUser.UserID, getUser.UserID);
        }


        #endregion



        #region Test Methods for Project

        [Test]
        public void TestAddProject()
        {

            Projects newProject = new Projects()
            {
                ProjectName = "Lycos",
                Priority = 5,
                StartDate = DateTime.ParseExact("8/25/2018", "M/d/yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("9/26/2018", "M/d/yyyy", CultureInfo.InvariantCulture),
                ManagerID = 4
            };

            projectRepository.AddProject(newProject);
            randomProjects = SetupProjects();
            Projects lastProject = randomProjects.Last();
            Assert.That(lastProject.StartDate, Is.EqualTo(DateTime.ParseExact("8/25/2018", "M/d/yyyy", CultureInfo.InvariantCulture)));
            Assert.That(lastProject.EndDate, Is.EqualTo(DateTime.ParseExact("9/26/2018", "M/d/yyyy", CultureInfo.InvariantCulture)));
            Assert.AreEqual(newProject.ManagerID, lastProject.ManagerID);
            Assert.AreEqual(newProject.Priority, lastProject.Priority);
        }


        [Test]
        public void TestUpdateProject()
        {
            Projects firstProject = randomProjects.Find(a => a.ProjectID == 1002);
            Projects UpdateProject = new Projects()
            {
                ProjectID = firstProject.ProjectID,
                ProjectName = firstProject.ProjectName,
                StartDate = DateTime.ParseExact("9/21/2018", "M/d/yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("11/21/2018", "M/d/yyyy", CultureInfo.InvariantCulture),
                Priority = 10,
                ManagerID = 4
            };

            projectRepository.EditProject(UpdateProject);


            Assert.AreEqual(firstProject.ProjectID, UpdateProject.ProjectID);
            Assert.That(firstProject.StartDate, Is.EqualTo(DateTime.ParseExact("9/21/2018", "M/d/yyyy", CultureInfo.InvariantCulture)));
            Assert.That(firstProject.EndDate, Is.EqualTo(DateTime.ParseExact("11/21/2018", "M/d/yyyy", CultureInfo.InvariantCulture)));
            Assert.That(firstProject.Priority, Is.EqualTo(10));
        }


        [Test]
        public void TestGetAllProjects()
        {
            int countProject = randomProjects.Count;
            List<Projects> allProjects = projectRepository.GetAllProjects().ToList();
            Assert.IsNotNull(allProjects);
            Assert.AreEqual(countProject, allProjects.Count);
        }


        [Test]
        public void TestGetProjectById()
        {
            Projects firstProject = randomProjects.First();
            Projects getProject = projectRepository.GetProjectById(firstProject.ProjectID);
          
            Assert.IsNotNull(getProject);
            Assert.AreEqual(firstProject.ProjectID, getProject.ProjectID);
        }


        #endregion



        #region Test Methods for Parent Tasks

        [Test]
        public void TestAddParentTask()
        {

            ParentTasks newParent = new ParentTasks()
            {
                ParentTaskName = "Development",
                ProjectID = 3
            };

            parentRepository.AddParentTask(newParent);
            randomParents = SetupParent();
            ParentTasks lastParent = randomParents.Last();
            
            Assert.AreEqual(lastParent.ParentTaskName, "Development");
            Assert.AreEqual(newParent.ProjectID, lastParent.ProjectID);
        }


        [Test]
        public void TestUpdateParentTask()
        {
            ParentTasks firstParent = randomParents.Find(a => a.ParentTaskId == 1001);
            
            ParentTasks UpdateParent = new  ParentTasks()
            {
                ParentTaskId = firstParent.ParentTaskId,
                ParentTaskName = firstParent.ParentTaskName,
                ProjectID = 5
            };

            parentRepository.EditParentTask(UpdateParent);
         
            Assert.AreEqual(firstParent.ParentTaskId, UpdateParent.ParentTaskId);
            Assert.That(firstParent.ProjectID, Is.EqualTo(5));
        }


        [Test]
        public void TestGetAllParenTasks()
        {
            int countParent = randomParents.Count;
            List<ParentTasks> allParents = parentRepository.GetAllParentTasks().ToList();
            Assert.IsNotNull(allParents);
            Assert.AreEqual(countParent, allParents.Count);
        }

        [Test]
        public void TestGetParentTaskById()
        {
            ParentTasks firstParent = randomParents.First();
            ParentTasks getParent = parentRepository.GetParentTaskById(firstParent.ParentTaskId);
            Assert.IsNotNull(getParent);
            Assert.AreEqual(firstParent.ParentTaskId, getParent.ParentTaskId);
            Assert.AreEqual(firstParent.ProjectID, getParent.ProjectID);
        }


        #endregion



        #region Test Methods for Tasks
        [Test]
        public void TestAddTask()
        {
            
            TaskData newTask = new TaskData()
            {
                TaskName = "Testing1", 
                ParentTaskId = 5,
                ProjectID = 3,
                Priority = 10,
                StartDate = DateTime.ParseExact("8/25/2018", "M/d/yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("9/26/2018", "M/d/yyyy", CultureInfo.InvariantCulture),
                UserID = 3
            };
           
            taskRepository.AddTask(newTask);
            randomTasks = SetupTasks();
            TaskData lastTask = randomTasks.Last();
            Assert.That(lastTask.StartDate, Is.EqualTo(DateTime.ParseExact("8/25/2018", "M/d/yyyy", CultureInfo.InvariantCulture)));
            Assert.That(lastTask.EndDate, Is.EqualTo(DateTime.ParseExact("9/26/2018", "M/d/yyyy", CultureInfo.InvariantCulture)));
            Assert.AreEqual(newTask.ParentTaskId, lastTask.ParentTaskId);
            Assert.AreEqual(newTask.Priority, lastTask.Priority);

        }


        [Test]
        public void TestUpdateTask()
        {
            TaskData firstTask = randomTasks.Find(a => a.TaskId == 1001);
            TaskData UpdateTask = new TaskData()
            {
                TaskId = firstTask.TaskId,
                TaskName = firstTask.TaskName,
                StartDate = DateTime.ParseExact("9/21/2018", "M/d/yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("11/21/2018", "M/d/yyyy", CultureInfo.InvariantCulture),
                Priority = 10
            };
            taskRepository.EditTask(UpdateTask);
         
          
            Assert.AreEqual(firstTask.TaskId, UpdateTask.TaskId);
            Assert.That(firstTask.StartDate,Is.EqualTo(DateTime.ParseExact("9/21/2018", "M/d/yyyy", CultureInfo.InvariantCulture)));
            Assert.That(firstTask.EndDate, Is.EqualTo(DateTime.ParseExact("11/21/2018", "M/d/yyyy", CultureInfo.InvariantCulture)));
            Assert.That(firstTask.Priority, Is.EqualTo(10));
        }

        [Test]
        public void TestDeleteTask()
        {
            int maxTaskID = randomTasks.Max(a => a.TaskId);
            TaskData lastTask = randomTasks.Last();
            taskRepository.RemoveTask(lastTask.TaskId);
            Assert.That(maxTaskID + 1, Is.GreaterThan(randomTasks.Max(a => a.TaskId)));
        }

        [Test]
        public void TestGetAllTasks()
        {
            int countTask = randomTasks.Count;
            List<TaskData> allTasks = taskRepository.GetAllTasks().ToList();
            Assert.IsNotNull(allTasks);
            Assert.AreEqual(countTask, allTasks.Count);
        }

        [Test]
        public void TestGetTaskById()
        {
            TaskData firstTask = randomTasks.First();
            TaskData getTask = taskRepository.GetTaskById(firstTask.TaskId);
            Assert.IsNotNull(getTask);
            Assert.AreEqual(firstTask.TaskId, getTask.TaskId);

        }
        #endregion
    }
}
