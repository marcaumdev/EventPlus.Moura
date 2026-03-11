using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repositories;

public class InstituicaoRepository : IInstituicaoRepository
{
    private readonly EventContext _context;

    // Injeção de Dependência: Recebe o contexto pronto para uso
    public InstituicaoRepository(EventContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Atualiza uma instituição. Aproveitamos o rastreamento do EF.
    /// </summary>
    public void Atualizar(Guid IdInstituicao, Instituicao Instituicao)
    {
        // Ao usar o Find, o EF começa a "observar" este objeto
        var instituicaoBuscada = _context.Instituicaos.Find(IdInstituicao);

        if (instituicaoBuscada != null)
        {
            instituicaoBuscada.Cnpj = Instituicao.Cnpj;
            instituicaoBuscada.Endereco = Instituicao.Endereco;
            instituicaoBuscada.NomeFantasia = Instituicao.NomeFantasia;

            // Não é necessário chamar _context.Update() se o objeto foi buscado no mesmo contexto
            _context.SaveChanges();
        }
    }

    public Instituicao BuscarPorId(Guid IdInstituicao)
    {
        return _context.Instituicaos.Find(IdInstituicao)!;
    }

    public void Cadastrar(Instituicao Instituicao)
    {
        _context.Instituicaos.Add(Instituicao);
        _context.SaveChanges();
    }

    public void Deletar(Guid IdInstituicao)
    {
        var instituicaoBuscada = _context.Instituicaos.Find(IdInstituicao);

        if (instituicaoBuscada != null)
        {
            _context.Instituicaos.Remove(instituicaoBuscada);
            _context.SaveChanges();
        }
    }

    public List<Instituicao> Listar()
    {
        return _context.Instituicaos.ToList();
    }
}

