using ControleDeContatos.Models;
using ControleDeContatos.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers;

public class ContatoController : Controller
{
    private readonly IContatoRepository _contatoRepository;
    public ContatoController(IContatoRepository contatoRepository)
    {
        _contatoRepository = contatoRepository;
    }

    #region Get

    public IActionResult Index()
    {
        var contato = _contatoRepository.BuscarTodos();
        return View(contato);
    }
    public IActionResult Editar(int id)
    {
        var contato = _contatoRepository.ListarPorId(id);
        return View(contato);
    }
    public IActionResult Apagar(int id)
    {
        _contatoRepository.Apagar(id);
        return RedirectToAction("Index");
    }
    public IActionResult ApagarConfirmacao(int id)
    {
        var contato = _contatoRepository.ListarPorId(id);
        return View(contato);
    }

    #endregion

    #region Set

    [HttpPost]
    public IActionResult Criar(ContatoModel contato)
    {
        _contatoRepository.Adicionar(contato);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Alterar(ContatoModel contato)
    {
        _contatoRepository.Atualizar(contato);
        return RedirectToAction("Index");
    }

    #endregion

    #region Redirect

    public IActionResult Criar()
    {
        return View();
    }

    #endregion



}