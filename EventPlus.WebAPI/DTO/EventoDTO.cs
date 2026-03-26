using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

public class EventoDTO
{
    [Required(ErrorMessage = "O nome do evento é obrigatório!")]
    public string? NomeEvento { get; set; }
    [Required(ErrorMessage = "A descrição do evento é obrigatória!")]
    public string? Descricao { get; set; }
    public DateTime DataEvento { get; set; }
    public Guid IdTipoEvento { get; set; }
    public Guid IdInstituicao { get; set; }
}
