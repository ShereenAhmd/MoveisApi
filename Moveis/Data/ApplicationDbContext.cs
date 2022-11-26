using Microsoft.EntityFrameworkCore;
using Moveis.Models;

namespace Moveis;

public class ApplicationDbContext:DbContext 
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
    { }

    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movei> Movies { get; set; }





}
