using EBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBooks.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Post()
        {
            return View();
        }

        public int TotalCarrinho()
        {
            var lstCarrinho = (List<CarrinhoDeCompra>)Session["LstCarrinho"];
            if (lstCarrinho == null || !lstCarrinho.Any()) return 0;
            return lstCarrinho.Count;
        }

    }
}