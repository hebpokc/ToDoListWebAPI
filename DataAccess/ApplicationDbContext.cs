using DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    /// <summary>
    /// Контекст базы данных приложения ToDoList.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }

        /// <summary>
        /// DbSet для работы с пользователями (<see cref="ApplicationUser"/>).
        /// </summary>
        public DbSet<ApplicationUser> Users { get; set; }

        /// <summary>
        /// DbSet для работы с категориями задач (<see cref="Category"/>).
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// DbSet для работы со статусами задач (<see cref="Status"/>).
        /// </summary>
        public DbSet<Status> Statuses { get; set; }

        /// <summary>
        /// DbSet для работы с задачами (<see cref="TaskEntity"/>).
        /// </summary>
        public DbSet<TaskEntity> Tasks { get; set; }

        /// <summary>
        /// DbSet для работы с расходами по задачам (<see cref="Expense"/>).
        /// </summary>
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка отношения один ко одному: Task <-> Expense
            modelBuilder.Entity<TaskEntity>()
                .HasOne(t => t.Expense)
                .WithOne(e => e.Task)
                .HasForeignKey<Expense>(e => e.TaskId);
        }
    }
}
