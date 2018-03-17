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
    public class CarrinhoController : Controller
    {
        #region PageLoad

        private RepositoryCarrinho _repCarrinho = new RepositoryCarrinho();

        public ActionResult Carrinho()
        {
            var lstCarrinho = (List<CarrinhoDeCompra>)Session["LstCarrinho"];
            return View(lstCarrinho);
        }

        #endregion

        #region RemoverLivroCarrinho

        public ActionResult RemoverLivroCarrinho(int id, int quantidade)
        {
            try
            {
                _repCarrinho.DeleteLivroCarrinho(id, quantidade);
            }
            catch (Exception ex)
            {
                TempData["erro"] = ex.Message;
                return RedirectToAction("Carrinho");
            }
            return RedirectToAction("Carrinho");
        }

        #endregion

    }
}