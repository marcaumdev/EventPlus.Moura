using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repositories;

public class TipoUsuarioRepository : ITipoUsuarioRepository
{
    private readonly EventContext _context;

    // Injeção de Dependência: Recebe o contexto pelo construtor
    public TipoUsuarioRepository(EventContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Atualiza um tipo de usuário usando o Change Tracking do EF
    /// </summary>
    /// <param name="id">Id do tipo usuario a ser atualizado</param>
    /// <param name="TipoUsuario">Novos dados do tipo usuario</param>
    public void Atualizar(Guid id, TipoUsuario TipoUsuario)
    {
        // Ao buscar com Find, o objeto já entra no radar do EF
        var tipoBuscado = _context.TipoUsuarios.Find(id);

        if (tipoBuscado != null)
        {
            tipoBuscado.Titulo = TipoUsuario.Titulo;

            // Não precisa de .Update(), o SaveChanges detecta a alteração no Titulo
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Busca um tipo de usuário por id
    /// </summary>
    /// <param name="id">id do tipo usuário a ser buscado</param>
    /// <returns>Objeto do tipoUsuario com as informações do tipo de usuario buscado</returns>
    public TipoUsuario BuscarPorId(Guid id)
    {
        return _context.TipoUsuarios.Find(id)!;
    }

    /// <summary>
    /// Cadastra um novo tipo de usuario
    /// </summary>
    /// <param name="TipoUsuario">tipo de usuario a ser cadastrado</param>
    public void Cadastrar(TipoUsuario TipoUsuario)
    {
        _context.TipoUsuarios.Add(TipoUsuario);
        _context.SaveChanges();
    }

    /// <summary>
    /// Deleta um tipo de usuario
    /// </summary>
    /// <param name="id">id do tipo usuario a ser deletado</param>
    public void Deletar(Guid id)
    {
        var tipoBuscado = _context.TipoUsuarios.Find(id);

        if (tipoBuscado != null)
        {
            _context.TipoUsuarios.Remove(tipoBuscado);
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Busca a lista de tipo de usuario cadastrado
    /// </summary>
    /// <returns>Uma lista de tipo usuario</returns>
    public List<TipoUsuario> Listar()
    {
        return _context.TipoUsuarios.ToList();
    }
}
