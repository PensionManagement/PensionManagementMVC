using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PensionManagementMVC.Models
{
    public class LoginVerification
    {
        [Required(ErrorMessage = "Aadhar Number is required")]
        [DisplayName("Aadhar Number")]
        public string AadhaarNo { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Only Alphabets acceptable")]

        public string Name { get; set; }
    }
}
