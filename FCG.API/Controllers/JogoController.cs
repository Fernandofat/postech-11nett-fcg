using FCG.API.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FCG.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogoController : ControllerBase
    {
        // GET: api/<JogoController>
        [HttpGet]
        public IEnumerable<JogoViewModel> Get()
        {
            return new JogoViewModel[] {
                new JogoViewModel { Nome = "Super Jogo" }
            };
        }

        // GET api/<JogoController>/5
        [HttpGet("{id}")]
        public JogoViewModel Get(int id)
        {
            // Exemplo simples — em um cenário real você buscaria o jogo por id
            return new JogoViewModel { Nome = $"Jogo {id}" };
        }

        // POST api/<JogoController>
        [HttpPost]
        public void Post([FromBody] JogoViewModel value)
        {
        }

        // PUT api/<JogoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] JogoViewModel value)
        {
        }

        // DELETE api/<JogoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
