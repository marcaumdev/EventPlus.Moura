using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface IPresencaRepository
{
    void Inscrever(Presenca presenca);
    void Deletar(Guid id);
    List<Presenca> Listar();
    Presenca BuscarPorId(Guid id);
    void Atualizar(Guid id);
    List<Presenca> ListarMinhas(Guid IdUsuario);
}
