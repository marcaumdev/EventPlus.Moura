using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public LoginController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    /// <summary>
    /// Enpoint da API que faz a chamada para o método de buscar por email e senha e gera token de acesso
    /// </summary>
    /// <param name="usuario">Usuário que deseja se logar na aplicação</param>
    /// <returns>Status code e Token de acesso</returns>
    [HttpPost]
    public IActionResult Login(LoginDTO usuario)
    {
        try
        {
            //busca usuário por email e senha 
            Usuario usuarioBuscado = _usuarioRepository.BuscarPorEmailESenha(usuario.Email!, usuario.Senha!);

            //caso não encontre
            if (usuarioBuscado == null)
            {
                //retorna 401 - sem autorização
                return StatusCode(401, "Email ou senha inválidos!");
            }


            //caso encontre, prossegue para a criação do token

            //informações que serão fornecidas no token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email!),
                new Claim(JwtRegisteredClaimNames.Name,usuarioBuscado.Nome!),
                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                new Claim(ClaimTypes.Role, usuarioBuscado.IdTipoUsuarioNavigation!.Titulo!),


            };

            //chave de segurança
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("event-webapi-chave-autenticacao-ef"));

            //credenciais
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //token
            var meuToken = new JwtSecurityToken(
                    issuer: "webapi.event+",
                    audience: "webapi.event+",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: creds
                );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(meuToken)
            });
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
}
