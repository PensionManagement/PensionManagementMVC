using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PensionManagementMVC.Models
{
    public class ProcessPension
    {
        [Required(ErrorMessage = "Name is required")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Only Alphabets acceptable")]

        public string Name { get; set; }
        [Required(ErrorMessage = "DOB is required")]
        [DisplayName("Date Of Birth")]
        [DataType(DataType.Date)]
        //[Remote("DateCheck", "ProcessPension",
        //      ErrorMessage = "Give Valid Date of birth")]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "PAN Number is required")]
        [DisplayName("Pan Number")]
        [RegularExpression("^([A-Z]){5}([0-9]){4}([A-Z]){1}$", ErrorMessage = "Invalid PAN Number")]

        public string PAN { get; set; }
        [Required(ErrorMessage = "Aadhar Number is required")]
        [DisplayName("Aadhar Number")]
        [RegularExpression("^[1-9]{12}$", ErrorMessage = "Invalid Aadhaar number")]
        [Remote("AadharCheck", "ProcessPension",
               ErrorMessage = "Give Valid Aadhar")]
        public string AadhaarNo { get; set; }
        [Required(ErrorMessage = "Pension Type is required")]
        [DisplayName("Pension Type")]

        public int PensionType { get; set; }

    }
}
