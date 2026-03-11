using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface ITipoUsuarioRepository
{
    void Cadastrar(TipoUsuario tipoUsuario);
    void Deletar(Guid id);
    List<TipoUsuario> Listar();
    TipoUsuario BuscarPorId(Guid id);
    void Atualizar(Guid id, TipoUsuario tipoUsuario);
}
