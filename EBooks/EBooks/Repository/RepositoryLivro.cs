using EBooks.DAO;
using EBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBooks.Repository
{
    public class RepositoryLivro : LivroDAO
    {
        public List<Livro> BuscarLivroPeloNome(string nome)
        {
            var lstLivros = (List<Livro>)HttpContext.Current.Session["LstLivros"];
            if (string.IsNullOrWhiteSpace(nome)) return lstLivros;
            return lstLivros.Where(x => x.Nome.ToUpper().Contains(nome.ToUpper())).ToList();
        }
    }
}