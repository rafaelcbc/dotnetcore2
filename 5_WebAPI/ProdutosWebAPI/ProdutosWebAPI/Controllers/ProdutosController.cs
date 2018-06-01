using Microsoft.AspNetCore.Mvc;
using ProdutosWebAPI.Models;
using System.Collections.Generic;

namespace ProdutosWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProdutosController : Controller
    {
        static readonly IProdutoRepositorio repositorio = new ProdutoRepositorio();

        [HttpGet]
        public IEnumerable<Produto> GetTodos()
        {
            return repositorio.GetAll();
        }

        [HttpGet("{id}" , Name = "GetProduto")]
        public IActionResult GetProdutoPorId(int id)
        {
            Produto produto = repositorio.Get(id);
            if(produto == null)
            {
                return NotFound();
            }
            return new ObjectResult(produto);
        }

        [HttpPost]
        public IActionResult CriarProduto([FromBody] Produto item)
        {
            if(item == null)
            {
                return BadRequest();
            }

            item = repositorio.Add(item);
            return CreatedAtRoute("GetProduto", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaProduto(int id, [FromBody] Produto item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            item.Id = id;
            if(!repositorio.Update(item))
            {
                return NotFound();
            }
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaProduto(int id)
        {
            Produto item = repositorio.Get(id);
            if (item == null)
            {
                return BadRequest();
            }
            repositorio.Remove(id);
            return new NoContentResult();
        }
    }
}
