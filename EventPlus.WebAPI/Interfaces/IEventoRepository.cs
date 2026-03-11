using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface IEventoRepository
{
    void Cadastrar(Evento evento);
    List<Evento> Listar();
    void Deletar(Guid id);
    void Atualizar(Guid id, Evento evento);
    Evento BuscarPorId(Guid id);
    List<Evento> ListarPorId(Guid IdUsuario);
    List<Evento> ListarProximos();

}
