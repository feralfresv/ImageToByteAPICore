using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MovieData.MovieDataBase;

namespace MovieData.Models
{
    public partial class DatingAppContext : DbContext
    {
        public DatingAppContext()
        {
        }

        public DatingAppContext(DbContextOptions<DatingAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MovieDetails> MovieDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;  Database=DatingApp; Trusted_Connection=True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<MovieData.MovieDataBase.MovieDetail> MovieDetail { get; set; }
    }
}
