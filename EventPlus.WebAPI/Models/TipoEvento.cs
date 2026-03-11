using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EventPlus.WebAPI.Models;

[Table("TipoEvento")]
public partial class TipoEvento
{
    [Key]
    public Guid IdTipoEvento { get; set; }

    [StringLength(100)]
    public string Titulo { get; set; } = null!;

    [JsonIgnore]
    [InverseProperty("IdTipoEventoNavigation")]
    public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();
}
