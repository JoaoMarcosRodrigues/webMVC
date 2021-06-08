using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webMVC.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }

        [Required]
        public int IdDep { get; set; }

        public string NameDep { get; set; }


        [Required]
        public string EmpName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        [Required]
        public string MobileNo { get; set; }

    }
}