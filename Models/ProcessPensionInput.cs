using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionManagementMVC.Models
{
    public class ProcessPensionInput
    {

        public string AadhaarNo { get; set; }
        public double PensionAmount { get; set; }
        public int BankCharge { get; set; }
    }

}
