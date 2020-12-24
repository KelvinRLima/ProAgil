using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProAgil.Domain;
using AutoMapper;
using ProAgil.WebAPI.Dtos;

namespace ProAgil.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        public readonly IProAgilRepository _repository;
        private readonly IMapper _mapper;

        public EventoController(IProAgilRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET api/evento
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Evento> eventos = await _repository.GetAllEventosAsync(true);

                IEnumerable<EventoDto> results = _mapper.Map<EventoDto[]>(eventos);

                return Ok(results);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de Dados Falhou: {ex.Message}");
            }
        }

        // GET api/evento/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                Evento eventos = await _repository.GetEventoAsyncByID(id, true);

                EventoDto results = _mapper.Map<EventoDto>(eventos);

                return Ok(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
        }

        // GET api/evento/string
        [HttpGet("{Tema}")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                Evento[] eventos = await _repository.GetAllEventosAsyncByTema(tema, true);

                EventoDto[] results = _mapper.Map<EventoDto[]>(eventos);

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
        public async Task<IActionResult> Post(EventoDto model)
        {
            try
            {
                Evento evento = _mapper.Map<Evento>(model);

                _repository.Add(evento);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/evento/{evento.ID}", _mapper.Map<EventoDto>(evento));
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de Dados Falhou: {ex.Message}");
            }
            return BadRequest("msg");
        }

        // Update api/evento
        [HttpPut("{eventoID}")]
        public async Task<IActionResult> Put(int eventoID, EventoDto model)
        {
            try
            {
                Evento evento = await _repository.GetEventoAsyncByID(eventoID, false);

                if (evento == null) return NotFound();

                var teste = _mapper.Map(model, evento);

                _repository.Update(evento);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/evento/{evento.ID}", _mapper.Map<EventoDto>(evento));
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
