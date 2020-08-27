using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PalestranteController : ControllerBase
    {
        public readonly IProAgilRepository _repository;
        public PalestranteController(IProAgilRepository repository)
        {
            _repository = repository;
        }

        // GET api/evento/nome
        [HttpGet]
        public async Task<IActionResult> Get(string nome)
        
        {
            try
            {
                Palestrante[] results = await _repository.GetAllPalestrantesAsyncByName(nome, true);

                return Ok(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
        }

        // GET api/values/5
        [HttpGet("{EventoID}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Palestrante results = await _repository.GetPalestranteAsyncByID(id, true);

                return Ok(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
        }

        // POST api/evento
        [HttpPost]
        public async Task<IActionResult> Post(Palestrante model)
        {
            try
            {
                _repository.Add(model);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/evento/{model.ID}", model);
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
            return BadRequest("msg");
        }

        // Update api/evento
        [HttpPut]
        public async Task<IActionResult> Put(int id, Palestrante model)
        {
            try
            {
                Palestrante palestrante = await _repository.GetPalestranteAsyncByID(id, false);

                if (palestrante == null) return NotFound();

                _repository.Update(model);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/evento/{model.ID}", model);
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
            return BadRequest("msg");
        }

        // Delete api/values/5
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Palestrante palestrante = await _repository.GetPalestranteAsyncByID(id, false);

                if (palestrante == null) return NotFound();

                _repository.Delete(palestrante);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
            return BadRequest("msg");
        }
    }
}
