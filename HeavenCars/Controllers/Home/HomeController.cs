using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using HeavenCars.DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MimeKit;
using HeavenCars.ViewModels.Home;
using MimeKit.Text;

namespace HeavenCars.Controllers.Home
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
                    var message = new MimeMessage(); //instancie un nouveau mimeMessage
                    
                    message.To.Add(new MailboxAddress("E-mail Recipient Name", "ict.slap@gmail.com")); //va regler le TO email addresse dans le barre
                  
                    message.From.Add(new MailboxAddress("E-mail From Name", "from@domain.com"));   //va regler le FROM email addresse dans la barre
                    
                    message.Subject = contactViewModel.Subject; //C'est le subject de ton email
                   
                    message.Body = new TextPart(TextFormat.Html)  //C'est le body message de ton email
                    {
                        Text = contactViewModel.Message + " Message was sent by: " + contactViewModel.Name + " E-mail: " + contactViewModel.Email
                    };

                    //Va configurer le email
                    using (var emailClient = new MailKit.Net.Smtp.SmtpClient())
                    {
                        emailClient.Connect("smtp.gmail.com", 587, false);
                        emailClient.Authenticate("ict.slap@gmail.com", "Zntdu1790");
                        emailClient.Send(message);
                        emailClient.Disconnect(true);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $" Oops! We have a problem here {ex.Message}";
                }

            }
            return View();
        }
    }
}
