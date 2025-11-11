using FCG.API.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FCG.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        // GET: api/<UsuarioController>
        [HttpGet]
        public IEnumerable<UsuarioViewModel> Get()
        {
            return new UsuarioViewModel[] {
                new UsuarioViewModel { Nome = "João Silva", Email = "joao.silva@example.com" },
                new UsuarioViewModel { Nome = "Maria Souza", Email = "maria.souza@example.com" }
            };
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public UsuarioViewModel Get(int id)
        {
            // Exemplo simples — em um cenário real você buscaria o usuário por id
            return new UsuarioViewModel { Nome = $"Usuário {id}", Email = $"usuario{id}@example.com" };
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public void Post([FromBody] UsuarioViewModel value)
        {
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UsuarioViewModel value)
        {
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
