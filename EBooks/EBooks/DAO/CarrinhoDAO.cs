using EBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBooks.DAO
{
    public class CarrinhoDAO
    {
        #region Metodos

        public void AddCarrinho(int id, int quantidade = 0)
        {
            var lstLivros = (List<Livro>)HttpContext.Current.Session["LstLivros"];
            var lstCarrinho = (List<CarrinhoDeCompra>)HttpContext.Current.Session["LstCarrinho"];
            var livro = lstLivros.Find(x => x.LivroId.Equals(id));
            if (livro == null)
                throw new ArgumentException("Livro não encontrado!");

            if (quantidade > livro.Quantidade || quantidade <= 0)
                throw new ArgumentException("Quantidade de livros indiponível!");
            var cart = lstCarrinho.Find(x => x.Livro.LivroId.Equals(id));
            if (cart != null)
            {
                lstCarrinho.RemoveAll(x => x.CarrinhoId.Equals(cart.CarrinhoId));
                cart.Quantidade = cart.Quantidade + quantidade;
                cart.Total = livro.Preco * cart.Quantidade;
            }
            else
            {
                var rand = new Random();
                cart = new CarrinhoDeCompra()
                {
                    CarrinhoId = rand.Next(0, Int32.MaxValue),
                    Livro = livro,
                    Quantidade = quantidade,
                    Total = livro.Preco * quantidade
                };
            }
            lstCarrinho.Add(cart);
            HttpContext.Current.Session["LstCarrinho"] = lstCarrinho;
            var totalQuantidadeRestante = livro.Quantidade - quantidade;
            livro.Quantidade = totalQuantidadeRestante < 0 ? 0 : totalQuantidadeRestante;
            lstLivros.RemoveAll(x => x.LivroId.Equals(livro.LivroId));
            lstLivros.Add(livro);
            HttpContext.Current.Session["LstLivros"] = lstLivros;
        }

        public void DeleteLivroCarrinho(int id, int quantidade = 0)
        {
            if (quantidade <= 0)
                throw new ArgumentException("Para remover um item do carrinho a quantidade deve ser maior que 0!");

            var lstLivros = (List<Livro>)HttpContext.Current.Session["LstLivros"];
            var lstCarrinho = (List<CarrinhoDeCompra>)HttpContext.Current.Session["LstCarrinho"];

            var cart = lstCarrinho.Find(x => x.CarrinhoId.Equals(id));
            if (cart == null)
                throw new ArgumentException("Item do carrinho não encontrado!");

            if (quantidade > cart.Quantidade)
                throw new ArgumentException("Não é possível remover quantidade maior do que a disponível no carrinho!");

            var totalQtde = cart.Quantidade - quantidade;
            lstCarrinho.RemoveAll(x => x.CarrinhoId.Equals(cart.CarrinhoId));
            var livro = lstLivros.Find(x => x.LivroId.Equals(cart.Livro.LivroId));
            if (livro == null) throw new ArgumentException("Livro não encontrado!");
            lstLivros.RemoveAll(x => x.LivroId.Equals(livro.LivroId));
            livro.Quantidade = livro.Quantidade + quantidade;
            lstLivros.Add(livro);
            HttpContext.Current.Session["LstLivros"] = lstLivros;
            if (totalQtde > 0)
            {
                cart.Quantidade = totalQtde;
                cart.Total = cart.Livro.Preco * cart.Quantidade;
                lstCarrinho.Add(cart);
                HttpContext.Current.Session["LstCarrinho"] = lstCarrinho;
            }
        }

        #endregion
    }
}