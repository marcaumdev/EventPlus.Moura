using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EventPlus.WebAPI.Models;

[Table("Usuario")]
[Index("Email", Name = "UQ__Usuario__A9D10534E1B1EC98", IsUnique = true)]
public partial class Usuario
{
    [Key]
    public Guid IdUsuario { get; set; }

    [StringLength(100)]
    public string Nome { get; set; } = null!;

    [StringLength(256)]
    public string Email { get; set; } = null!;

    [StringLength(60)]
    public string Senha { get; set; } = null!;

    public Guid? IdTipoUsuario { get; set; }

    [JsonIgnore]
    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<ComentarioEvento> ComentarioEventos { get; set; } = new List<ComentarioEvento>();

    [ForeignKey("IdTipoUsuario")]
    [InverseProperty("Usuarios")]
    public virtual TipoUsuario? IdTipoUsuarioNavigation { get; set; }

    [JsonIgnore]
    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<Presenca> Presencas { get; set; } = new List<Presenca>();
}
