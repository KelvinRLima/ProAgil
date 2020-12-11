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
    public class EventoController : ControllerBase
    {
        public readonly IProAgilRepository _repository;
        public EventoController(IProAgilRepository repository)
        {
            _repository = repository;
        }

        // GET api/evento
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Evento> results = await _repository.GetAllEventosAsync(true);

                return Ok(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
        }

        // GET api/evento/5
        [HttpGet("{EventoID}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Evento results = await _repository.GetEventoAsyncByID(id, true);

                return Ok(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
        }

        // GET api/evento/string
        [HttpGet("{Tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                Evento[] results = await _repository.GetAllEventosAsyncByTema(tema, true);

                return Ok(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
        }

        ////POST api/evento
        //[HttpPost]
        //[Route("post2")]
        //public async Task<IActionResult> Post2(Evento model)
        //{
        //    try
        //    {
        //        //_repository.Add(model);

        //        if (await _repository.SaveChangesAsync())
        //        {
        //            //return Created($"/api/evento/{model.ID}", model);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
        //    }
        //    return BadRequest("msg");
        //}

        // POST api/evento
        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                _repository.Add(model);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/evento/{model.ID}", model);
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
            return BadRequest("msg");
        }

        // Update api/evento
        [HttpPut("{eventoID}")]
        public async Task<IActionResult> Put(int eventoID, Evento model)
        {
            try
            {
                Evento evento = await _repository.GetEventoAsyncByID(eventoID, false);

                if (evento == null) return NotFound();

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

        // Delete api/evento/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Evento evento = await _repository.GetEventoAsyncByID(id, false);

                if (evento == null) return NotFound();

                _repository.Delete(evento);

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
