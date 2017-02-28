using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Logbook.Core;
using Logbook.Core.DTO;
using LogbookUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace LogbookUI.Controllers
{
    [Authorize]
    public class AccountController : CustomController
    {
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            //ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await TryLogin(model.Email, model.Password);
            switch (result)
            {
                case "LOGINRESULT/USERNOTFOUND":
                    ModelState.AddModelError("", "No user found with that email address.");
                    return View(model);
                case "LOGINRESULT/INCORRECTPASSWORD":
                    ModelState.AddModelError("", "Incorrect password. If you have forgotten your password, you can <a href='account/resetpassword'>reset it here</a>.");
                    return View(model);
                case "LOGINRESULT/SUCCESS":
                    // sign in, set up authentication
                    IdentitySignin(model.Email, model.Email, string.Empty, model.RememberMe);
                    // redirect to home page
                    return RedirectToLocal(returnUrl);
                default:
                    ModelState.AddModelError("", "An error occurred while attempting to log in.  Please refresh the page and try again.");
                    return View(model);
            }

        }
        
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Home", "Home");
        }

        private Task<string> TryLogin(string email, string pass)
        {
            return Task<string>.Factory.StartNew(() =>
            {
                var user = DataAccess.GetUser(email);
                if (user == null)
                {
                    return "LOGINRESULT/USERNOTFOUND";
                }
               
                var pbkdf2 = new Rfc2898DeriveBytes(pass, user.PasswordSalt, 1000);
                var providedHash = pbkdf2.GetBytes(32);
                var passwordCorrect = true;
                for (var i = 0; i < 32; i++)
                {
                    if (providedHash[i] != user.PasswordHash[i])
                    {
                        passwordCorrect = false;
                    }
                }

                return passwordCorrect ? "LOGINRESULT/SUCCESS" : "LOGINRESULT/INCORRECTPASSWORD";
            });
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // check if user already exists

                var user = DataAccess.GetUser(model.Email);
                if (user != null)
                {
                    ModelState.AddModelError("", "That email address is already in use.  If you think it's your account, you can <a href='account/resetpassword'>recover it here</a>.");
                    return View(model);
                }

                byte[] salt;
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[32]);
                var pbkdf2 = new Rfc2898DeriveBytes(model.Password, salt, 1000);
                byte[] hash = pbkdf2.GetBytes(32);

                user = new UserDTO
                {
                    Email = model.Email,
                    PasswordHash = hash,
                    PasswordSalt = salt,
                    Name = model.Name,
                    Location = model.Location,
                    Status = "USERSTATUS/TRIAL"
                };

                var result = DataAccess.CreateUser(user);
                if (!result)
                {
                    ModelState.AddModelError("", "Sorry, something went wrong while creating your account. Please refresh the page and try again.");
                }
                else
                {
                    ModelState.AddModelError("", "Registration Successful.");

                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult LogOff()
        {
            IdentitySignout();
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            ModelState.AddModelError("", "An email has been sent to the specified address with instructions on how to reset your account password.");
            var user = DataAccess.GetUser(model.Email);
            if (user == null)
            {
            }
            else
            {
                var requestGuid = DataAccess.GenerateRequest(new RequestDTO()
                {
                    RequestType = "REQUEST/PASSWORDRESET",
                    UserId = user.UserId
                });
                Mailer.SendMessage("support@outdoorlogbook.com", user.Email, "OutdoorLogbook - Password reset instructions", "http://localhost:53279/Account/PasswordReset/" + requestGuid);
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult PasswordReset(Guid requestId)
        {
            ModelState.SetModelValue("RequestId", new ValueProviderResult(requestId, "123", CultureInfo.CurrentCulture));
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PasswordReset(PasswordResetViewModel model)
        {
            var validRequest = DataAccess.CheckRequest(model.RequestId, model.Email);
            if (!validRequest)
            {
                ModelState.AddModelError("", "Request has expired, been used, or is not valid for this email address.  You will need to submit another password reset request.");
                return View(model);
            }
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[32]);
            var pbkdf2 = new Rfc2898DeriveBytes(model.Password, salt, 1000);
            byte[] hash = pbkdf2.GetBytes(32);

            var user = DataAccess.GetUser(model.Email);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            DataAccess.ResetPassword(user);
            DataAccess.ConsumeRequest(model.RequestId);
            ModelState.AddModelError("", "Your password has been successfully reset.  Please login to access your account.");

            return View(model);
        }

        public void IdentitySignin(string userId, string name, string providerKey = null, bool isPersistent = false)
        {
            var claims = new List<Claim>();

            // create *required* claims
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));
            claims.Add(new Claim(ClaimTypes.Name, name));

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            // add to user here!
            AuthenticationManager.SignIn(new AuthenticationProperties()
            {
                //AllowRefresh = true,
                IsPersistent = isPersistent,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            }, identity);
        }

        public void IdentitySignout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                                          DefaultAuthenticationTypes.ExternalCookie);
        }
    }
}
