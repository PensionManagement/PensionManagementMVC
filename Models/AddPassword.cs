using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PensionManagementMVC.Models
{
    public class AddPassword
    {
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [CompareAttribute("Password", ErrorMessage = "Password Do not Match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
