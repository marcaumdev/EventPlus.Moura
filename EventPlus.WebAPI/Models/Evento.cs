using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Models;

[Table("Evento")]
public partial class Evento
{
    [Key]
    public Guid IdEvento { get; set; }

    [StringLength(100)]
    public string Nome { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime DataEvento { get; set; }

    [Column(TypeName = "text")]
    public string Descricao { get; set; } = null!;

    public Guid? IdTipoEvento { get; set; }

    public Guid? IdInstituicao { get; set; }

    [JsonIgnore]
    [InverseProperty("IdEventoNavigation")]
    public virtual ICollection<ComentarioEvento> ComentarioEventos { get; set; } = new List<ComentarioEvento>();

    [ForeignKey("IdInstituicao")]
    [InverseProperty("Eventos")]
    public virtual Instituicao? IdInstituicaoNavigation { get; set; }

    [ForeignKey("IdTipoEvento")]
    [InverseProperty("Eventos")]
    public virtual TipoEvento? IdTipoEventoNavigation { get; set; }

    [JsonIgnore]
    [InverseProperty("IdEventoNavigation")]
    public virtual ICollection<Presenca> Presencas { get; set; } = new List<Presenca>();
}
