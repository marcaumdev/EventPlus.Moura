using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InstituicaoController : ControllerBase
{
    private IInstituicaoRepository _instituicaoRepository { get; set; }

    public InstituicaoController(IInstituicaoRepository instituicaoRepository)
    {
        _instituicaoRepository = instituicaoRepository;
    }

    /// <summary>
    /// Enpoint da API que faz a chamada para o método de listar as instituições
    /// </summary>
    /// <returns>Status code e a lista de instituições</returns>
    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            return Ok(_instituicaoRepository.Listar());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Enpoint da API que faz a chamada para o método de buscar uma instituição específica
    /// </summary>
    /// <param name="id">Id da instituição buscada</param>
    /// <returns>Status code e a instituição buscada</returns>
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        try
        {
            return Ok(_instituicaoRepository.BuscarPorId(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Enpoint da API que faz a chamada para o método de cadastrar uma instituição
    /// </summary>
    /// <param name="instituicao">Instituição a ser cadastrada</param>
    /// <returns>status code e a instituição cadastrada</returns>
    [HttpPost]
    public IActionResult Post(InstituicaoDTO instituicaoDto)
    {
        try
        {
            var novaInstituicao = new Instituicao
            {
                Cnpj = instituicaoDto.CNPJ!,
                Endereco = instituicaoDto.Endereco!,
                NomeFantasia = instituicaoDto.NomeFantasia!
            };

            _instituicaoRepository.Cadastrar(novaInstituicao);

            return StatusCode(201, novaInstituicao);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Enpoint da API que faz a chamada para o método de atualizar um evento
    /// </summary>
    /// <param name="id">Id da instituição a ser atualizada</param>
    /// <param name="instituicao">Instituição com os dados atualizados</param>
    /// <returns>Status code e a instituição atualizada</returns>
    [HttpPut("{id}")]
    public IActionResult Put(Guid id, InstituicaoDTO instituicaoDto)
    {
        try
        {
            var novaInstituicao = new Instituicao
            {
                Cnpj = instituicaoDto.CNPJ!,
                Endereco = instituicaoDto.Endereco!,
                NomeFantasia = instituicaoDto.NomeFantasia!
            };

            _instituicaoRepository.Atualizar(id, novaInstituicao);

            return StatusCode(204, novaInstituicao);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Enpoint da API que faz a chamada para o método de deletar uma instituição
    /// </summary>
    /// <param name="id">Id da instituição a ser excluída</param>
    /// <returns>Status code</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        try
        {
            _instituicaoRepository.Deletar(id);

            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
