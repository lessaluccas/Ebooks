using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EBooks.Models
{
    public class Livro
    {        
        public int LivroId { get; set; }
        [Required(ErrorMessage = "Favor informar o nome do livro!")]
        [MaxLength(80, ErrorMessage = "Máximo de 80 caracteres!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Favor informar a descrição do livro!")]
        [MaxLength(800, ErrorMessage = "Máximo de 800 caracteres!")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Favor informar o preço do livro!")]
        [DataType(DataType.Currency)]
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }
        [Required(ErrorMessage = "Favor informar a quantidade de livros disponíveis!")]
        public int Quantidade { get; set; }
        public byte[] Imagem { get; set; }
    }
}