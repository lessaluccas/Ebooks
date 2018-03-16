using EBooks.DAO;
using EBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBooks.Controllers
{
    public class LivroController : Controller
    {

        #region Propriedades

        private LivroDAO _dao = new LivroDAO();

        public List<Livro> LstLivros
        {
            get { return (List<Livro>)Session["LstLivros"]; }
            set { Session["LstLivros"] = value; }
        }

        #endregion

        #region PageLoad

        [HttpGet]
        public ActionResult TodosLivros()
        {
            if (LstLivros != null && LstLivros.Any()) return View(LstLivros);
            LstLivros = new List<Livro>();
            LstLivros = _dao.PreencherLivros();
            return View(LstLivros);
        }

        #endregion

        #region NovoLivro

        [HttpGet]
        public ActionResult NovoLivro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NovoLivro(Livro l, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
                return View(l);
            try
            {
                l = _dao.AdicionaImagemLivro(l, file);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Imagem", ex.Message);
                return View(l);
            }
            _dao.AddLivro(l);
            TempData["msg"] = "Livro cadastrado com sucesso!";
            return RedirectToAction("NovoLivro");
        }

        #endregion

        #region EditarLivro

        [HttpGet]
        public ActionResult EditarLivro(int id)
        {
            if (id == 0 || !LstLivros.Any(x => x.LivroId.Equals(id)))
            {
                TempData["erro"] = "Livro não encontrado!";
                return RedirectToAction("TodosLivros");
            }

            var livro = LstLivros.Find(x => x.LivroId.Equals(id));
            return View(livro);
        }

        [HttpPost]
        public ActionResult EditarLivro(Livro l, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
                return View(l);
            try
            {
                l = _dao.AdicionaImagemLivro(l, file);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Imagem", ex.Message);
                return View(l);
            }
            _dao.UpdateLivro(l);
            TempData["msg"] = "Livro alterado com sucesso!";            
            return RedirectToAction("TodosLivros");
        }

        #endregion

        #region ExcluirLivro

        public ActionResult ExcluirLivro(int id)
        {
            try
            {
                _dao.DeleteLivro(id);
            }
            catch (Exception ex)
            {
                TempData["erro"] = ex.Message;
                return RedirectToAction("TodosLivros");
            }
            TempData["msg"] = "Livro excluído com sucesso!";
            return RedirectToAction("TodosLivros");
        }

        #endregion        
    }
}
