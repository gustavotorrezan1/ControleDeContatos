using ControleDeContatos.Data;
using ControleDeContatos.Models;

namespace ControleDeContatos.Repository;

public class ContatoRepository : IContatoRepository
{
    private readonly BancoContext _context;

    public ContatoRepository(BancoContext bancoContext)
    {
        _context = bancoContext;
    }

    public ContatoModel ListarPorId(int id)
    {
        return _context.Contato.FirstOrDefault(x => x.Id == id);
    }

    public List<ContatoModel> BuscarTodos()
    {
        return _context.Contato.ToList();
    }

    public ContatoModel Adicionar(ContatoModel contato)
    {
        _context.Contato.Add(contato);
        _context.SaveChanges();
        return contato;
    }

    public ContatoModel Atualizar(ContatoModel contato)
    {
        ContatoModel contatoDB = ListarPorId(contato.Id);
        if (contatoDB == null)
        {
            throw new Exception("Ouve um erro na atualização do contato");
        }
        else
        {
            contatoDB.Nome = contato.Nome;
            contatoDB.Email = contato.Email;
            contatoDB.Celular = contato.Celular;

            _context.Contato.Update(contatoDB);
            _context.SaveChanges();

            return contatoDB;
        }

    }

    public bool Apagar(int id)
    {
        ContatoModel contatoDB = ListarPorId(id);
        if (contatoDB == null)
        {
            throw new Exception("Ouve um erro na exclusão do contato");
        }
        else
        {
            _context.Contato.Remove(contatoDB);
            _context.SaveChanges();
            return true;
        }
    }
}