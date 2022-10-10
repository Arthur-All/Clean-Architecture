using ApiPerson.Interface;
using ApiPerson.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiPerson.Controllers
{
    [ApiController]
    [Route(template:"v1")]
    public class PersonController : Controller
    {
        private readonly IPersonServices _personService;
        private readonly ILogger _logger;
        public PersonController(IPersonServices personService)
        {
            _personService = personService;
        }

        [HttpGet(template:"get")]
        public async Task <ActionResult<List<Person>>> FindAllPerson()
        {
            try {

               var pess = await _personService.FindAllPerson();

               return Ok(pess);
            } 
            catch(Exception ex) {

                return BadRequest(ex);
            }
            
        }
        /*[HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            var person = _personService.FindById(Id);
            if (person == null)
            {
                return NotFound("Person not foud");
            }
            return Ok(person);
        }*/
        [HttpPost(template: "TES")]
        public async Task <ActionResult> Post([FromBody] Person person)
        {

            if (person.Email == null)
            {
                return BadRequest("Need a email");
            }
            try
            {
                return Ok(await _personService.CreatePerson(person));
            }catch(Exception ex)
            {
                return BadRequest(ex);
            }
            
        }
        [HttpPut(template: "put")]
        public async Task< IActionResult> UpdatePerson([FromBody] Person personModel)
        {
            try
            {
                var edit = await _personService.UpdatePerson( personModel);
                return Ok(edit);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message); //Perguntar se statusCode não é muito generico
            }
        }
        [HttpDelete(template:"delete")]
        public async Task< ActionResult> Delete([FromBody] int Id)
        {
            await _personService.DeletePerson(Id);
            return NoContent();
        }
    }
}
