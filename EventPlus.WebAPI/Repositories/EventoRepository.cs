using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class EventoRepository : IEventoRepository
{
    private readonly EventContext _context;
    public EventoRepository(EventContext context)
    {
        _context = context; 
    }

    /// <summary>
    /// Método que atualiza um evento
    /// </summary>
    /// <param name="IdEvento">id do evento a ser atualizado</param>
    /// <param name="Evento">Novos dados do evento</param>
    public void Atualizar(Guid IdEvento, Evento Evento)
    {
        // Ao buscar com Find, o objeto já fica "rastreado" pelo EF
        var eventoBuscado = _context.Eventos.Find(IdEvento);

        if (eventoBuscado != null)
        {
            eventoBuscado.DataEvento = Evento.DataEvento;
            eventoBuscado.Nome = Evento.Nome;
            eventoBuscado.Descricao = Evento.Descricao;
            eventoBuscado.IdTipoEvento = Evento.IdTipoEvento;
            eventoBuscado.IdInstituicao = Evento.IdInstituicao;

            // Não precisa de _context.Update se o objeto foi alterado diretamente
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Método que busca um evento por id
    /// </summary>
    /// <param name="IdEvento">Id do evento a ser buscado</param>
    /// <returns>Evento buscado</returns>
    public Evento BuscarPorId(Guid IdEvento)
    {
        return _context.Eventos
            .Include(e => e.IdTipoEventoNavigation)
            .Include(e => e.IdInstituicaoNavigation)
            .FirstOrDefault(e => e.IdEvento == IdEvento)!;
    }

    /// <summary>
    /// Método que cadastra um novo evento
    /// </summary>
    /// <param name="Evento">Evento a ser cadastrado</param>
    /// <exception cref="ArgumentException">casa a data do evento seja menor que hoje</exception>
    public void Cadastrar(Evento Evento)
    {
        if (Evento.DataEvento < DateTime.Now)
        {
            throw new ArgumentException("A data do evento deve ser maior ou igual a data atual.");
        }

        _context.Eventos.Add(Evento);
        _context.SaveChanges();
    }

    /// <summary>
    /// Método que deleta um evento
    /// </summary>
    /// <param name="IdEvento">id do evento a ser deletado</param>
    public void Deletar(Guid IdEvento)
    {
        var eventoBuscado = _context.Eventos.Find(IdEvento);

        if (eventoBuscado != null)
        {
            _context.Eventos.Remove(eventoBuscado);
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Método que retorna uma lista de eventos
    /// </summary>
    /// <returns>retorna uma lista de eventos</returns>
    public List<Evento> Listar()
    {
        return _context.Eventos
            .Include(e => e.IdTipoEventoNavigation)
            .Include(e => e.IdInstituicaoNavigation)
            .ToList();
    }

    /// <summary>
    /// Método que lista eventos filtrando pelas presencas de um usuário
    /// </summary>
    /// <param name="IdUsuario">Id do usuário para filtragem</param>
    /// <returns>Lista de eventos filtrados por usuário</returns>
    public List<Evento> ListarPorId(Guid IdUsuario)
    {
        return _context.Eventos
            .Include(e => e.IdTipoEventoNavigation)
            .Include(e => e.IdInstituicaoNavigation)
            .Where(e => e.Presencas.Any(p => p.IdUsuario == IdUsuario && p.Situacao == true))
            .ToList();
    }

    /// <summary>
    /// Método que busca os próximos eventos que irão acontecer
    /// </summary>
    /// <returns>Lista de próximos eventos</returns>
    public List<Evento> ListarProximos()
    {
        return _context.Eventos
            .Include(e => e.IdTipoEventoNavigation)
            .Include(e => e.IdInstituicaoNavigation)
            .Where(e => e.DataEvento >= DateTime.Now)
            .OrderBy(e => e.DataEvento)
            .ToList();
    }
}
