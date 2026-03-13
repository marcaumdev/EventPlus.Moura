using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

public class TipoUsuarioDTO
{
    [Required(ErrorMessage = "O titulo do tipo de usuário é obrigatório!")]
    public string? Titulo { get; set; }
}
