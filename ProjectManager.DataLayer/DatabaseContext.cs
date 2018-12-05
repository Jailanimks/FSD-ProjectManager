namespace ProjectManager.DataLayer
{
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext(): base("name=SqlConnection")
        {
        }
        public DbSet<TaskData> Tasks { get; set; }
        public DbSet<Users> User { get; set; }
        public DbSet<Projects> Project { get; set; }
        public DbSet<ParentTasks> ParentTask { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Users>()
                .HasKey(p => p.UserID)
                .Property(c => c.UserID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //modelBuilder.Entity<Users>()
            //    .HasOptional(s =>s.UsersProjects)
            //    .WithOptionalPrincipal(l => l.Manager);

            
            modelBuilder.Entity<Projects>()
                .HasKey(p => p.ProjectID)
                .Property(c => c.ProjectID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //modelBuilder.Entity<Projects>()
            //        .HasMany(p => p.ParentTasks)
            //        .WithOptional(s => s.Projects)
            //        .HasForeignKey(s => s.Project_Id);
     

            modelBuilder.Entity<ParentTasks>().HasKey(p => p.ParentTaskId);
            modelBuilder.Entity<ParentTasks>().Property(c => c.ParentTaskId)
                   .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //modelBuilder.Entity<ParentTasks>()
            //        .HasRequired(s => s.Projects)
            //        .WithMany(g => g.ParentTasks)
            //        .HasForeignKey(s => s.Project_Id);



            modelBuilder.Entity<TaskData>().HasKey(p => p.TaskId);
            modelBuilder.Entity<TaskData>().Property(c => c.TaskId)
                   .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            base.OnModelCreating(modelBuilder);

        }
    }
}
