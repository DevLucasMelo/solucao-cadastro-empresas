using CadastroEmpresasApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CadastroEmpresasApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
    }
}
