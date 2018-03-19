using EBooks.DAO;
using EBooks.Models;
using EBooks.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBooks.Controllers
{
    public class LojaController : Controller
    {

        #region Propriedades

        private RepositoryLivro _repLivro = new RepositoryLivro();
        private RepositoryCarrinho _repCarrinho = new RepositoryCarrinho();

        public List<CarrinhoDeCompra> LstCarrinho
        {
            get { return (List<CarrinhoDeCompra>)Session["LstCarrinho"]; }
            set { Session["LstCarrinho"] = value; }
        }

        #endregion 

        #region PageLoad

        public ActionResult LojaLivros()
        {
            if (LstCarrinho == null)
                LstCarrinho = new List<CarrinhoDeCompra>();
            var lstLivros = (List<Livro>)Session["LstLivros"];
            if (lstLivros == null || !lstLivros.Any())
            {
                lstLivros = _repLivro.PreencherLivros();
                Session["LstLivros"] = lstLivros;

            }
            return View(lstLivros);
        }

        #endregion

        #region AdicionarCarrinho

        public ActionResult AddCarrinho(int id, int quantidade = 0)
        {
            try
            {
                _repCarrinho.AddCarrinho(id, quantidade);
            }
            catch (Exception ex)
            {
                TempData["erro"] = ex.Message;
                return RedirectToAction("LojaLivros");
            }
            TempData["msg"] = "Livro adicionado ao carrinho com sucesso!";
            return RedirectToAction("LojaLivros");
        }

        #endregion

    }
}