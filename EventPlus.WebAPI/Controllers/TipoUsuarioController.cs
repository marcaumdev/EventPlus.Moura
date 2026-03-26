using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoUsuarioController : ControllerBase
{
    private ITipoUsuarioRepository _tipoUsuarioRepository { get; set; }

    public TipoUsuarioController(ITipoUsuarioRepository tipoUsuarioRepository)
    {
        _tipoUsuarioRepository = tipoUsuarioRepository;
    }

    /// <summary>
    /// Enpoint da API que faz a chamada para o método de listar os tipos de usuário
    /// </summary>
    /// <returns>Status code e a lista de tipos de usuário</returns>
    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            return Ok(_tipoUsuarioRepository.Listar());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Enpoint da API que faz a chamada para o método de buscar um tipo de usuário específico
    /// </summary>
    /// <param name="id">Id do usuário buscado</param>
    /// <returns>Status code e o tipo de usuário buscado</returns>
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        try
        {
            return Ok(_tipoUsuarioRepository.BuscarPorId(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Enpoint da API que faz a chamada para o método de cadastrar um tipo de usuário
    /// </summary>
    /// <param name="tiposUsuario">Tipo de usuário a ser cadastrado</param>
    /// <returns>Status code e o tipo de usuário cadastrado</returns>
    [HttpPost]
    public IActionResult Post(TipoUsuarioDTO tipoUsuarioDto)
    {
        try
        {
            var novoTipoUsuario = new TipoUsuario
            {
                Titulo = tipoUsuarioDto.Titulo!
            };

            _tipoUsuarioRepository.Cadastrar(novoTipoUsuario);

            return StatusCode(201, novoTipoUsuario);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Enpoint da API que faz a chamada para o método de atualizar um tipo de usuário
    /// </summary>
    /// <param name="id">Id do tipo de usuário a ser atualizado</param>
    /// <param name="tipoUsuario">Tipo de usuário com os dados atualizados</param>
    /// <returns>Status code e o tipo de usuário atualizado</returns>
    [HttpPut("{id}")]
    public IActionResult Put(Guid id, TipoUsuarioDTO tipoUsuarioDto)
    {
        try
        {
            var tipoUsuarioAtualizado = new TipoUsuario
            {
                Titulo = tipoUsuarioDto.Titulo!
            };

            _tipoUsuarioRepository.Atualizar(id, tipoUsuarioAtualizado);

            return StatusCode(204, tipoUsuarioAtualizado);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Enpoint da API que faz a chamada para o método de deletar um tipo de usuário
    /// </summary>
    /// <param name="id">Id do tipo de usuário a ser excluído</param>
    /// <returns>Status code</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        try
        {
            _tipoUsuarioRepository.Deletar(id);

            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
