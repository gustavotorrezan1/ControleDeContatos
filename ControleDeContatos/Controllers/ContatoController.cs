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
        // Lista todos
        var contato = _contatoRepository.BuscarTodos();
        return View(contato);
    }

    public IActionResult Apagar(int id)
    {
        // Apaga o contato pelo id
        try
        {
            var apagado = _contatoRepository.Apagar(id);
            if (apagado)
            {
                TempData["MensagemSucesso"] = "Contato apagado com sucesso!";
            }
            else
            {
                TempData["MensagemSucesso"] = "Ops, não conseguimos apagar o contato!";
            }
            return RedirectToAction("Index");
        }
        catch (Exception erro)
        {
            TempData["MensagemSucesso"] = $"Ops, não conseguimos apagar seu contato, mais detalhes do erro: {erro}";
            return RedirectToAction("Index");
        }
    }

    #endregion

    #region Set

    [HttpPost]
    public IActionResult Criar(ContatoModel contato)
    {
        // Cria um novo contato
        try
        {
            if (ModelState.IsValid)
            {
                _contatoRepository.Adicionar(contato);
                TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
                return RedirectToAction("Index");
            }

            return View(contato);
        }
        catch (Exception erro)
        {
            TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu contato, tente novamente," +
                                       $"detalhe do erro:{erro.Message}";
            throw;
        }
    }

    [HttpPost]
    public IActionResult Alterar(ContatoModel contato)
    {
        // Atualiza o contato
        try
        {
            if (ModelState.IsValid)
            {
                _contatoRepository.Atualizar(contato);
                TempData["MensagemSucesso"] = "Contato atualizado com sucesso";
                return RedirectToAction("Index");
            }

            return View("Editar", contato);
        }
        catch (Exception erro)
        {
            TempData["MensagemErro"] = $"Ops, não conseguimos atualizar seu contato, tente novamente," +
                                       $"detalhe do erro:{erro.Message}";
            throw;
        }
    }

    #endregion

    #region Redirect

    public IActionResult Criar()
    {
        //Direciona para a pagina de criar o contato
        return View();
    }

    public IActionResult Editar(int id)
    {
        // Direciona para a pagina de editar o contato
        var contato = _contatoRepository.ListarPorId(id);
        return View(contato);
    }

    public IActionResult ApagarConfirmacao(int id)
    {
        // Direciona para a pagina de confirmar a exclusao do contato
        var contato = _contatoRepository.ListarPorId(id);
        return View(contato);
    }

    #endregion
}