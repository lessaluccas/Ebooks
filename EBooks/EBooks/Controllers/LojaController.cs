using EBooks.DAO;
using EBooks.Models;
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

        private LivroDAO _livroDao = new LivroDAO();
        private CarrinhoDAO _carrinhoDao = new CarrinhoDAO();

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
                Session["LstLivros"] = _livroDao.PreencherLivros();
            return View(lstLivros);
        }

        #endregion

        #region AdicionarCarrinho

        public ActionResult AddCarrinho(int id, int quantidade = 0)
        {
            try
            {
                _carrinhoDao.AddCarrinho(id, quantidade);
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