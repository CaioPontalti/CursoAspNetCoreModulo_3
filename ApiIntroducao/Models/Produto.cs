using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiIntroducao.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        [ForeignKey("Categoria")]
        public int CategoriaId { get; set; }

        [JsonIgnore] //Não mostra null no retorno quando não tem informação.
        public Categoria Categoria { get; set; }
    }
}
