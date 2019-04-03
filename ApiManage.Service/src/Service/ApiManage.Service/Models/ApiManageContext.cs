using System;
using ApiManage.Service.Models.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiManage.Service.Models
{
    public partial class ApiManageContext : DbContext
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string ConnectionString { get; set; }

        public ApiManageContext()
        {
        }

        public ApiManageContext(DbContextOptions<ApiManageContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Directory> Directory { get; set; }
        public virtual DbSet<InterfaceHistory> IinterfaceHistory { get; set; }
        public virtual DbSet<Interface> Interface { get; set; }
        public virtual DbSet<LoginRecord> LoginRecord { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<PermissionsAccount> PermissionsAccount { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<ProjectAccount> ProjectAccount { get; set; }
        public virtual DbSet<ProjectTeam> ProjectTeam { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RoleAccount> RoleAccount { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<TeamAccount> TeamAccount { get; set; }
        public virtual DbSet<Template> Template { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.AccountId)
                    .HasColumnName("AccountID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AccountName).HasMaxLength(10);

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DataState).HasDefaultValueSql("((0))");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HeadPic)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LoginFailCount).HasDefaultValueSql("((0))");

                entity.Property(e => e.NickName).HasMaxLength(10);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Directory>(entity =>
            {
                entity.Property(e => e.DirectoryId)
                    .HasColumnName("DirectoryID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DataState).HasDefaultValueSql("((0))");

                entity.Property(e => e.DirectoryName).HasMaxLength(20);

                entity.Property(e => e.ParentDirectoryId).HasColumnName("ParentDirectoryID");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<InterfaceHistory>(entity =>
            {
                entity.HasKey(e => e.InterfaceHistoryId);

                entity.Property(e => e.InterfaceHistoryId)
                    .HasColumnName("InterfaceHistoryID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ContentType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DirectoryId).HasColumnName("DirectoryID");

                entity.Property(e => e.HttpVersion)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.InterfaceId).HasColumnName("InterfaceID");

                entity.Property(e => e.InterfaceName).HasMaxLength(50);

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.RequestBody).IsUnicode(false);

                entity.Property(e => e.RequestHeader).IsUnicode(false);

                entity.Property(e => e.RequestMethod)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RequestUrl)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ResponseBody).IsUnicode(false);

                entity.Property(e => e.ResponseHeader).IsUnicode(false);

                entity.Property(e => e.ResponseState)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.TemplateId).HasColumnName("TemplateID");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.DataState).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Interface>(entity =>
            {
                entity.Property(e => e.InterfaceId)
                    .HasColumnName("InterfaceID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ContentType)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DataState).HasDefaultValueSql("((0))");

                entity.Property(e => e.DirectoryId).HasColumnName("DirectoryID");

                entity.Property(e => e.HttpVersion)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.InterfaceName).HasMaxLength(50);

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.RequestBody).IsUnicode(false);

                entity.Property(e => e.RequestHeader).IsUnicode(false);

                entity.Property(e => e.RequestMethod)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RequestUrl)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ResponseBody).IsUnicode(false);

                entity.Property(e => e.ResponseCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.ResponseHeader).IsUnicode(false);

                entity.Property(e => e.TemplateId).HasColumnName("TemplateID");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<LoginRecord>(entity =>
            {
                entity.Property(e => e.LoginRecordId)
                    .HasColumnName("LoginRecordId")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LoginTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserAgent)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Permissions>(entity =>
            {
                entity.Property(e => e.PermissionsId)
                    .HasColumnName("PermissionsID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DataState).HasDefaultValueSql("((0))");

                entity.Property(e => e.ProjectAccountId).HasColumnName("ProjectAccountID");

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<PermissionsAccount>(entity =>
            {
                entity.HasKey(e => e.PermissionsAccount1);

                entity.Property(e => e.PermissionsAccount1)
                    .HasColumnName("PermissionsAccount")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AccountId)
                    .HasColumnName("AccountID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DataState).HasDefaultValueSql("((0))");

                entity.Property(e => e.PermissionsId).HasColumnName("PermissionsID");

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.ProjectId)
                    .HasColumnName("ProjectID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DataState).HasDefaultValueSql("((0))");

                entity.Property(e => e.ProjectIntro).HasMaxLength(200);

                entity.Property(e => e.ProjectName).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<ProjectAccount>(entity =>
            {
                entity.ToTable("Project_Account");

                entity.Property(e => e.ProjectAccountId)
                    .HasColumnName("ProjectAccountID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DataState).HasDefaultValueSql("((0))");

                entity.Property(e => e.JoinTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<ProjectTeam>(entity =>
            {
                entity.ToTable("Project_Team");

                entity.Property(e => e.ProjectTeamId)
                    .HasColumnName("ProjectTeamID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DataState).HasDefaultValueSql("((0))");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.TeamId).HasColumnName("TeamID");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DataState).HasDefaultValueSql("((0))");

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.TeamId).HasColumnName("TeamID");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.角色名称).HasMaxLength(50);
            });

            modelBuilder.Entity<RoleAccount>(entity =>
            {
                entity.ToTable("Role_Account");

                entity.Property(e => e.RoleAccountId)
                    .HasColumnName("RoleAccountID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DataState).HasDefaultValueSql("((0))");

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.Property(e => e.TeamId)
                    .HasColumnName("TeamID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.TeamIntro).HasMaxLength(200);

                entity.Property(e => e.TeamName).HasMaxLength(50);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TeamAccount>(entity =>
            {
                entity.ToTable("Team_Account");

                entity.Property(e => e.TeamAccountId)
                    .HasColumnName("TeamAccountID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DataState).HasDefaultValueSql("((0))");

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.TeamId).HasColumnName("TeamID");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.TeamAccount)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_TEAM_ACC_REFERENCE_ACCOUNT");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamAccount)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK_TEAM_ACC_REFERENCE_TEAM");
            });

            modelBuilder.Entity<Template>(entity =>
            {
                entity.Property(e => e.TemplateId)
                    .HasColumnName("TemplateID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DataState).HasDefaultValueSql("((0))");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.ProjectTeamId).HasColumnName("ProjectTeamID");

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.TemplateContent).IsUnicode(false);

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });
        }
    }
}
