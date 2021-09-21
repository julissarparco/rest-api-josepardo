using demoApi.Data;
using demoApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace demoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {

        public readonly PetRepository _repository;


        public PetsController(PetRepository petRepository)
        {
            this._repository = petRepository;
        }

        // GET: api/<PetsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pet>>> Get()
        {
            return await _repository.GetAll();
        }

        // GET api/<PetsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> Get(int id)
        {
            var response = await _repository.GetById(id);

            if (response == null)
            {
                return NotFound();
            }

            return response;
        }

        // POST api/<PetsController>
        [HttpPost]
        public async Task Post([FromBody] Pet pet)
        {
            await _repository.Insert(pet);
        }

        // PUT api/<PetsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Pet pet)
        {
            var response = await _repository.GetById(id);

            if (response.Id != id)
            {
                return BadRequest("El id de la mascota no coincide con el id de la URL");
            }

            if (response is null)
            {
                return NotFound();
            }

            await _repository.Update(pet);

            return Ok();
        }

        // DELETE api/<PetsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _repository.DeleteById(id);
        }
    }
}
