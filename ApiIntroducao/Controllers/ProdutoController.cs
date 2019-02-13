using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiIntroducao.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiIntroducao.Controllers
{
    [Route("api/Categoria/{CategoriaId}/Produto")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public ProdutoController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Produto> Get(int CategoriaId)
        {
            return dbContext.Produtos.Where(c => c.CategoriaId == CategoriaId).ToList();
        }

    }
}