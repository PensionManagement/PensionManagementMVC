using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionManagementMVC.Models
{
    public class PensionDetail
    {
        public string Name { get; set; }
        public DateTime? Dob { get; set; }
        public string Pan { get; set; }
        public string AadhaarNo { get; set; }
        public double? Salary { get; set; }
        public double? Allowance { get; set; }
        public int? BankType { get; set; }
        public string BankName { get; set; }
        public string BankAccountNo { get; set; }
        public int? PensionType { get; set; }
        public string Password { get; set; }
    }
}
