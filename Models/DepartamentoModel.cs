using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webMVC.Models
{
    public class DepartamentoModel
    {
        public int Id { get; set; }

        [Required]
        public string DepName { get; set; }

        public DepartamentoModel(int Id, string Name)
        {
            this.Id = Id;
            this.DepName = Name;
        }

        public DepartamentoModel() { }

    }
}