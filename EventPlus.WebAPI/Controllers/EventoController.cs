using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventoController : ControllerBase
{
    private IEventoRepository _eventoRepository;

    public EventoController(IEventoRepository eventoRepository)
    {
        _eventoRepository = eventoRepository; 
    }

    /// <summary>
    /// Enpoint da API que faz a chamada para o método de listar os eventos
    /// </summary>
    /// <returns>Status code e a lista de eventos</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_eventoRepository.Listar());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o método de listar eventos filtrado por usuário
    /// </summary>
    /// <param name="idUsuario">Id do usuário para filtragem</param>
    /// <returns>Lista de eventos filtrados por usuário</returns>
    [HttpGet("Usuario/{idUsuario}")]
    public IActionResult ListarPorId(Guid idUsuario)
    {
        try
        {
            return Ok(_eventoRepository.ListarPorId(idUsuario));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o método de listar os próximos eventos
    /// </summary>
    /// <returns>Status code 200 e uma lista de próximos eventos</returns>
    [HttpGet("ListarProximos")]
    public IActionResult BuscarProximos()
    {
        try
        {
            return Ok(_eventoRepository.ListarProximos());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Enpoint da API que faz a chamada para o método de buscar um evento específico
    /// </summary>
    /// <param name="id">Id do evento a ser buscado</param>
    /// <returns>Status code e o evento buscado</returns>
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        try
        {
            return Ok(_eventoRepository.BuscarPorId(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Enpoint da API que faz a chamada para o método de cadastrar um evento
    /// </summary>
    /// <param name="evento">Evento a ser cadastrado</param>
    /// <returns>Status code e o evento cadastrado</returns>
    [HttpPost]
    public IActionResult Post(EventoDTO eventoDto)
    {
        try
        {
            // Converte DTO para Entidade
            var novoEvento = new Evento
            {
                Nome = eventoDto.NomeEvento!,
                Descricao = eventoDto.Descricao!,
                DataEvento = eventoDto.DataEvento,
                IdTipoEvento = eventoDto.IdTipoEvento,
                IdInstituicao = eventoDto.IdInstituicao
            };
            _eventoRepository.Cadastrar(novoEvento);


            return StatusCode(201, novoEvento);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Enpoint da API que faz a chamada para o método de atualizar um evento
    /// </summary>
    /// <param name="id">Id do evento a ser atualizado</param>
    /// <param name="evento">Evento com os dados atualizados</param>
    /// <returns>Status code e o evento atualizado</returns>
    [HttpPut("{id}")]
    public IActionResult Put(Guid id, EventoDTO eventoDto)
    {
        try
        {
            // Converte DTO para Entidade
            var eventoAtualizado = new Evento
            {
                Nome = eventoDto.NomeEvento!,
                Descricao = eventoDto.Descricao!,
                DataEvento = eventoDto.DataEvento,
                IdTipoEvento = eventoDto.IdTipoEvento,
                IdInstituicao = eventoDto.IdInstituicao
            };
            _eventoRepository.Atualizar(id, eventoAtualizado);

            return StatusCode(204, eventoAtualizado);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Enpoint da API que faz a chamada para o método de deletar um evento
    /// </summary>
    /// <param name="id">Id do evento a ser excluído</param>
    /// <returns>Status code</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        try
        {
            _eventoRepository.Deletar(id);

            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
