using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HeavenCars.ViewModels.Account
{
    public class RegisterViewModel
    
        {
            [Required]
            public string FirstName { get; set; }

            [Required]
            public string LastName { get; set; }

            [Required]
            [EmailAddress]
            [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
                ErrorMessage = "Email must be a valid Address Email.")]
            [Remote(action: "IsEmailInUse",
                controller: "Account")]
            //[ValidEmailDomain(allowedDomain: "test.com",
            //    ErrorMessage = "Email domain must be test.com")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Comfirm password")]
            [Compare("Password",
                ErrorMessage = "Password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string City { get; set; }

        //public DateTime Birthday { get; set; }
    }
    }
