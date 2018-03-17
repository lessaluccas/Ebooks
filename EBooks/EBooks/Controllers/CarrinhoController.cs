using EBooks.DAO;
using EBooks.Models;
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

        private CarrinhoDAO _carrinhoDao = new CarrinhoDAO();

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
                _carrinhoDao.DeleteLivroCarrinho(id, quantidade);
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