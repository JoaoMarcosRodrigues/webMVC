using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webMVC.Models
{
    public class EnderecoModel
    {
        
        public int IdEnd { get; set; }
        [Required]
        public int IdEmp { get; set; }
        [Required]
        public string Rua { get; set; }
        [Required]
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        [Required]
        public int Ativo { get; set; }

        public EnderecoModel(int idEnd, int idEmp, string rua, string bairro, string complemento, int ativo)
        {
            this.IdEnd = idEnd;
            this.IdEmp = idEmp;
            this.Rua = rua;
            this.Bairro = bairro;
            this.Complemento = complemento;
            this.Ativo = ativo;
        }

        public EnderecoModel() { }
    }
}