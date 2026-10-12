using Microsoft.EntityFrameworkCore;
using System.Data;
using UniProject1.Models;

namespace UniProject1.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<TodoItem> TodoItems { get; set; } = null!;
    public DbSet<Priority> Priorities { get; set; } = null!;
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProjectUser> ProjectUsers => Set<ProjectUser>();
    public DbSet<Status> Statuses => Set<Status>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<TodoItemProject> TodoItemProjects {  get; set; } = null!;
    public DbSet<CommentUser> CommentUsers {  get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CommentUser>()
            .HasKey(cu => new { cu.CommentId, cu.UserId });

        modelBuilder.Entity<CommentUser>()
            .HasOne(cu => cu.Comment)
            .WithMany(c => c.CommentUsers)
            .HasForeignKey(cu => cu.CommentId);

        modelBuilder.Entity<CommentUser>()
            .HasOne(cu => cu.User)
            .WithMany(u => u.CommentUsers)
            .HasForeignKey(cu => cu.UserId);

        modelBuilder.Entity<ProjectUser>()
            .HasKey(pu => new { pu.ProjectId, pu.UserId });

        modelBuilder.Entity<ProjectUser>()
            .HasOne(pu => pu.Project)
            .WithMany(p => p.ProjectUsers)
            .HasForeignKey(pu => pu.ProjectId);

        modelBuilder.Entity<ProjectUser>()
            .HasOne(pu => pu.User)
            .WithMany(u => u.ProjectUsers)
            .HasForeignKey(pu => pu.UserId);

        modelBuilder.Entity<TodoItemProject>()
            .HasKey(tp => new { tp.TodoItemId, tp.ProjectId });

        modelBuilder.Entity<TodoItemProject>()
            .HasOne(tp => tp.TodoItem)
            .WithMany(t => t.TodoItemProjects)
            .HasForeignKey(tp => tp.TodoItemId);

        modelBuilder.Entity<TodoItemProject>()
            .HasOne(tp => tp.Project)
            .WithMany(p => p.TodoItemProjects)
            .HasForeignKey(tp => tp.ProjectId);

        modelBuilder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);

        // Seed данни за приоритетите

        modelBuilder.Entity<Priority>().HasData(
            new Priority { Id = 1, Name = "Low" },
            new Priority { Id = 2, Name = "Medium" },
            new Priority { Id = 3, Name = "High" }
        );
    }
}
