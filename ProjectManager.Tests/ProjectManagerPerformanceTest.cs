using System;
using System.Collections.Generic;
using System.Linq;
using ProjectManager.DataLayer;
using ProjectManager.BusinessLayer;
using System.Globalization;
using NBench;

namespace ProjectManager.Tests
{

    public  class ProjectManagerPerformanceTest : PerformanceTestStuite<ProjectManagerPerformanceTest>
    {
        #region Variables
        private Counter _counter;

        private IUserRepository userRepository;
        private List<Users> randomUsers;

        private IProjectRepository projectRepository;
        private List<Projects> randomProjects;


        private IParentTaskRepository parentRepository;
        private List<ParentTasks> randomParents;

        private ITaskRepository taskRepository = new TaskRepository();
        private List<TaskData> randomTasks = new List<TaskData>();
        #endregion

        #region Setup
        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            _counter = context.GetCounter("ProjectCounter");
            randomUsers = SetupUsers();
            randomProjects = SetupProjects();
            randomParents = SetupParent();
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


        [PerfBenchmark(Description = "Test to ensure get all Users.", NumberOfIterations = 5, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 100, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterTotalAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterMeasurement("ProjectCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, 20000000000d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 20.0d)]

        public void GetAllUsersBenchmark(BenchmarkContext context)
        {
            int countUser = randomUsers.Count;
            List<Users> allUsers = userRepository.GetAllUsers().ToList();
            _counter.Increment();

        }

        [PerfBenchmark(Description = "Test to ensure Add a User.", NumberOfIterations = 5, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 100, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterTotalAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterMeasurement("ProjectCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, 20000000000d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 20.0d)]
        public void AddUserBenchmark()
        {

            Users newUser = new Users()
            {
                FirstName = "Sugumar",
                LastName = "K",
                EmployeeId = "CT30000"
            };

            userRepository.AddUser(newUser);
            _counter.Increment();
        }


        [PerfBenchmark(Description = "Test to ensure Update a User.", NumberOfIterations = 5, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 100, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterTotalAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterMeasurement("ProjectCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, 20000000000d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 20.0d)]
        public void UpdateUserBenchmark()
        {
            Users firstUser = randomUsers.Find(a => a.UserID == 17);

            Users UpdateUser = new Users()
            {
                FirstName = "Jahir",
                LastName = "K",
                EmployeeId = "CT40000"
            };
            userRepository.EditUser(UpdateUser);
            _counter.Increment();
        }



        [PerfBenchmark(Description = "Test to ensure to delete a User.", NumberOfIterations = 5, RunMode = RunMode.Throughput,
         RunTimeMilliseconds = 100, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterTotalAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterMeasurement("ProjectCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, 20000000000d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 20.0d)]
        public void DeleteUserBenchmark()
        {
            int maxUserID = randomUsers.Max(a => a.UserID);
            Users lastUser = randomUsers.Last();
            userRepository.RemoveUser(lastUser.UserID);
            _counter.Increment();
        }


        [PerfBenchmark(Description = "Test to ensure to Retrive a User by UserID.", NumberOfIterations = 5, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 100, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterTotalAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterMeasurement("ProjectCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, 20000000000d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 20.0d)]
        public void GetUserByIdBenchmark()
        {
            Users firstUser = randomUsers.First();
            Users getUser = userRepository.GetUserById(firstUser.UserID);
            _counter.Increment();
        }







        [PerfBenchmark(Description = "Test to ensure get all Projects.", NumberOfIterations = 5, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 100, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterTotalAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterMeasurement("ProjectCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, 20000000000d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 20.0d)]

        public void GetAllProjectsBenchmark(BenchmarkContext context)
        {
            int countProject = randomProjects.Count;
            List<Projects> allProjects = projectRepository.GetAllProjects().ToList();
            _counter.Increment();

        }

        [PerfBenchmark(Description = "Test to ensure Add a Project.", NumberOfIterations = 5, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 100, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterTotalAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterMeasurement("ProjectCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, 20000000000d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 20.0d)]
        public void AddProjectBenchmark()
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
            _counter.Increment();
        }


        [PerfBenchmark(Description = "Test to ensure Update a Project.", NumberOfIterations = 5, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 100, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterTotalAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterMeasurement("ProjectCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, 20000000000d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 20.0d)]
        public void UpdateProjectBenchmark()
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
            _counter.Increment();
        }


     
        [PerfBenchmark(Description = "Test to ensure to Retrive a Project by ProjectID.", NumberOfIterations = 5, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 100, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterTotalAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterMeasurement("ProjectCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, 20000000000d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 20.0d)]
        public void GetProjectByIdBenchmark()
        {
            Projects firstProject = randomProjects.First();
            Projects getProject = projectRepository.GetProjectById(firstProject.ProjectID);
            _counter.Increment();
        }








        [PerfBenchmark(Description = "Test to ensure get all ParentTasks.", NumberOfIterations = 5, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 100, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterTotalAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterMeasurement("ProjectCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, 20000000000d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 20.0d)]

        public void GetAllParentTasksBenchmark(BenchmarkContext context)
        {
            int countParent = randomParents.Count;
            List<ParentTasks> allParents = parentRepository.GetAllParentTasks().ToList();
            _counter.Increment();

        }


        [PerfBenchmark(Description = "Test to ensure Add a ParentTask.", NumberOfIterations = 5, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 100, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterTotalAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterMeasurement("ProjectCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, 20000000000d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 20.0d)]
        public void AddParentTaskBenchmark()
        {

            ParentTasks newParent = new ParentTasks()
            {
                ParentTaskName = "Development",
                ProjectID = 3
            };

            parentRepository.AddParentTask(newParent);
            _counter.Increment();
        }


        [PerfBenchmark(Description = "Test to ensure Update a ParentTask.", NumberOfIterations = 5, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 100, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterTotalAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterMeasurement("ProjectCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, 20000000000d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 20.0d)]
        public void UpdateParentTaskBenchmark()
        {
            ParentTasks firstParent = randomParents.Find(a => a.ParentTaskId == 1001);

            ParentTasks UpdateParent = new ParentTasks()
            {
                ParentTaskId = firstParent.ParentTaskId,
                ParentTaskName = firstParent.ParentTaskName,
                ProjectID = 5
            };

            parentRepository.EditParentTask(UpdateParent);
            _counter.Increment();
        }



        [PerfBenchmark(Description = "Test to ensure to Retrive a ParentTask by TaskID.", NumberOfIterations = 5, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 100, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterTotalAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterMeasurement("ProjectCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, 20000000000d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 20.0d)]
        public void GetParentTaskByIdBenchmark()
        {
            ParentTasks firstParent = randomParents.First();
            ParentTasks getParent = parentRepository.GetParentTaskById(firstParent.ParentTaskId);
            _counter.Increment();
        }









        [PerfBenchmark(Description = "Test to ensure get all Tasks.",NumberOfIterations = 5, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 100, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterTotalAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterMeasurement("ProjectCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, 20000000000d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 20.0d)]

        public void GetAllTasksBenchmark(BenchmarkContext context)
        {
            int countTask = randomTasks.Count;
            List<TaskData> allTasks = taskRepository.GetAllTasks().ToList();
            _counter.Increment();

        }

        
        [PerfBenchmark(Description = "Test to ensure Add a Task.", NumberOfIterations = 5, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 100, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterTotalAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterMeasurement("ProjectCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, 20000000000d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 20.0d)]
        public void AddTaskBenchmark()
        {
            TaskData newTask = new TaskData()
            {
                TaskName = "Testing1",
                ParentTaskId = 5,
                Priority = 10,
                StartDate = DateTime.ParseExact("8/25/2018", "M/d/yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("9/26/2018", "M/d/yyyy", CultureInfo.InvariantCulture)
            };

            taskRepository.AddTask(newTask);
            _counter.Increment();
        }



        [PerfBenchmark(Description = "Test to ensure Update a Task.", NumberOfIterations = 5, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 100, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterTotalAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterMeasurement("ProjectCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, 20000000000d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 20.0d)]
        public void UpdateTaskBenchmark()
        {
            TaskData firstTask = randomTasks.First();
            TaskData UpdateTask = new TaskData()
            {
                TaskId = firstTask.TaskId,
                TaskName = firstTask.TaskName,
                ParentTaskId = firstTask.ParentTaskId,
                StartDate = DateTime.ParseExact("9/21/2018", "M/d/yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("11/21/2018", "M/d/yyyy", CultureInfo.InvariantCulture),
                Priority = firstTask.Priority
            };
            taskRepository.EditTask(UpdateTask);
            _counter.Increment();
        }

        [PerfBenchmark(Description = "Test to ensure to delete a Task.", NumberOfIterations = 5, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 100, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterTotalAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterMeasurement("ProjectCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, 20000000000d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 20.0d)]
        public void DeleteTaskBenchmark()
        {
            int maxTaskID = randomTasks.Max(a => a.TaskId);
            TaskData lastTask = randomTasks.Last();
            taskRepository.RemoveTask(lastTask.TaskId);
            _counter.Increment();
        }

        [PerfBenchmark(Description = "Test to ensure to get a Task by TaskID.", NumberOfIterations = 5, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 100, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterTotalAssertion("ProjectCounter", MustBe.LessThanOrEqualTo, 20000000.0d)]
        [CounterMeasurement("ProjectCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, 20000000000d)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.LessThanOrEqualTo, 20.0d)]
        public void GetTaskByIdBenchmark()
        {
            TaskData firstTask = randomTasks.First();
            TaskData getTask = taskRepository.GetTaskById(firstTask.TaskId);

      
            _counter.Increment();
        }


        [PerfCleanup]
        public void Cleanup(BenchmarkContext context)
        {
         
            taskRepository = null;
        }

    }
}
