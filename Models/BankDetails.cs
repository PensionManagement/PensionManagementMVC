using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PensionManagementMVC.Models
{
    public class BankDetails
    {
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        [Required(ErrorMessage = "Bank Type is required")]
        [DisplayName("Bank Type")]
        public int BankType { get; set; }
    }
}
