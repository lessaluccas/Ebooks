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

        public ActionResult Carrinho()
        {
            var lstCarrinho = (List<CarrinhoDeCompra>)Session["LstCarrinho"];
            return View(lstCarrinho);
        }

        #endregion

        #region RemoverLivroCarrinho

        public ActionResult RemoverLivroCarrinho(int id, int quantidade)
        {
            if(quantidade <= 0)
            {
                TempData["erro"] = "Para remover um item do carrinho a quantidade deve ser maior que 0!";
                return RedirectToAction("Carrinho");
            }
            var lstCarrinho = (List<CarrinhoDeCompra>)Session["LstCarrinho"];
            var lstLivros = (List<Livro>)Session["LstLivros"];
            if (lstCarrinho == null) return RedirectToAction("LojaLivros", "Loja");
            if (lstLivros == null) return RedirectToAction("TodosLivros", "Livro");
            var cart = lstCarrinho.Find(x => x.CarrinhoId.Equals(id));
            if (cart == null)
            {
                TempData["erro"] = "Item do carrinho não encontrado!";
                return RedirectToAction("Carrinho");
            }
            if(quantidade > cart.Quantidade)
            {
                TempData["erro"] = "Não é possível remover quantidade maior do que a disponível no carrinho!";
                return RedirectToAction("Carrinho");
            }
            var totalQtde = cart.Quantidade - quantidade;
            lstCarrinho.RemoveAll(x => x.CarrinhoId.Equals(cart.CarrinhoId));
            var livro = lstLivros.Find(x => x.LivroId.Equals(cart.Livro.LivroId));
            if (livro == null) return RedirectToAction("TodosLivros", "Livro");
            lstLivros.RemoveAll(x => x.LivroId.Equals(livro.LivroId));
            livro.Quantidade = livro.Quantidade + quantidade;
            lstLivros.Add(livro);
            Session["LstLivros"] = lstLivros;
            if (totalQtde > 0)
            {
                cart.Quantidade = totalQtde;
                cart.Total = cart.Livro.Preco * cart.Quantidade;
                lstCarrinho.Add(cart);
                Session["LstCarrinho"] = lstCarrinho;
            }
            return RedirectToAction("Carrinho");
        }

        #endregion

    }
}