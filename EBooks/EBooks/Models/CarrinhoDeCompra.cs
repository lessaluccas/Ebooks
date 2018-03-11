using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBooks.Models
{
    public class CarrinhoDeCompra
    {
        public int CarrinhoId { get; set; }
        public Livro Livro { get; set; }
        public int Quantidade { get; set; }
        public decimal Total { get; set; }
    }
}