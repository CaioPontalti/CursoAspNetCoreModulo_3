using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiIntroducao.Models;

namespace ApiIntroducao.Models
{                                       //DbContext
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //Esta configurado no arquivo Startup no Metodo ConfigureServices
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ApiIntroducao.Models.Pessoa> Pessoa { get; set; }
    }

}
