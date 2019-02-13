using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiIntroducao.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ApiIntroducao
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Cria um Banco de Dados me Memória apenas.
            //services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("ProdutosDB"));

            //Busca a String de Conexão com o banco do SQl SERVER
            services.AddDbContext<ApplicationDbContext>(optios => optios.UseSqlServer(Configuration.GetConnectionString("strConn")));

            //Configurações do usuário. Essas informações ficam dentro da classe ApplicationUser
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            //Configuração para autenticação utilizando o [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

            //Precisa ser criado quando as entidades do projeto possuem relacionamento. Abaixo implementa o método ConfigureJson
            services.AddMvc().AddJsonOptions(ConfigureJson);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        //Método Criado
        private void ConfigureJson(MvcJsonOptions obj)
        {
            obj.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Utiliza a autenticação configurada no Authorize. Caso o usuário não esteja autenticado, a api não retorno nada.
            app.UseAuthentication();

            app.UseMvc();

            //Insere as informações na tabela em memória
            /*if (!dbContext.Categorias.Any())
            {
                dbContext.Categorias.AddRange(new List<Categoria>()
                {
                    new Categoria() {Nome = "Alimentos", Produtos = new List<Produto>() { new Produto { Nome= "Arroz"} }},


                    new Categoria() {Nome = "Lazer", Produtos = new List<Produto>() { new Produto { Nome="Bola"}, new Produto {Nome="CD"} }},


                    new Categoria() {Nome = "Limpeza"},
                    
                });

                dbContext.SaveChanges();
            }*/
        }
    }
}
