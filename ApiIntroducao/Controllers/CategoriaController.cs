using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiIntroducao.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiIntroducao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
            
        public CategoriaController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public IEnumerable<Categoria> Get()
        {
            return dbContext.Categorias.ToList();
        }

        [HttpGet("{id}", Name = "NovaCategoria")]
        public IActionResult GetById(int id)
        {
            var cat = dbContext.Categorias.Include(p => p.Produtos).FirstOrDefault(c => c.Id == id);

            if (cat == null)
            {
                return NotFound();
            }

            return Ok(cat);
        }

        [HttpPost]
        public IActionResult Post(Categoria cat)
        {
            if (ModelState.IsValid)
            {
                dbContext.Categorias.Add(cat);
                dbContext.SaveChanges();
                //return new CreatedAtRouteResult("NovaCategoria", new { id = cat.Id }, cat);
                return Ok(cat);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Categoria categoria)
        {
            try
            {
                dbContext.Entry(categoria).State = EntityState.Modified;
                dbContext.SaveChanges();

                return Ok(categoria);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cat = dbContext.Categorias.FirstOrDefault(c => c.Id == id);

            if (cat == null)
            {
                return NotFound();
            }

            dbContext.Categorias.Remove(cat);
            dbContext.SaveChanges();
            return Ok(HttpContext.Response.StatusCode);
        }
    }
}