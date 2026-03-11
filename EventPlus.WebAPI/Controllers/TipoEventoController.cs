using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoEventoController : ControllerBase
{
    private ITipoEventoRepository _tipoEventoRepository;

    //Injeção de dependência
    public TipoEventoController(ITipoEventoRepository tipoEventoRepository)
    {
        _tipoEventoRepository = tipoEventoRepository;
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o método de listar os tipos de evento
    /// </summary>
    /// <returns>Status code 200 e a lista de tipos de evento</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_tipoEventoRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o método de buscar um tipo de envento específico
    /// </summary>
    /// <param name="id">Id do tipo de evento buscado</param>
    /// <returns>Status code 200 e o tipo de evento buscado</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_tipoEventoRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }

    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o método de cadastrar um tipo de evento
    /// </summary>
    /// <param name="tipoEvento">Tipo de evento a ser cadastrado</param>
    /// <returns>Status Code 201 e o tipo de evento a ser cadastrado</returns>
    [HttpPost]
    public IActionResult Cadastrar(TipoEvento tipoEvento)
    {
        try
        {
            _tipoEventoRepository.Cadastrar(tipoEvento);

            return StatusCode(201, tipoEvento);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    //[HttpPut("{id}")]

}
