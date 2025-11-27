using FCG.API.Infra.Repository;
using FCG.API.Models.Entities;
using FCG.API.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FCG.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly ILogger<GamesController> _logger;
        private readonly GamesRepository _repository;


        public GamesController(ILogger<GamesController> logger, GamesRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: api/<JogoController>
        [HttpGet]
        public async Task<IEnumerable<Games>> Get()
        {
            return await _repository.GetAllGames();
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
