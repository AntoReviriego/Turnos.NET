using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Turnos.Models;

namespace Turnos.Controllers
{
    public class LoginController : Controller
    {
        private readonly TurnosContext db;
        public LoginController(TurnosContext db)
        {
            this.db = db;
        }

        // GET: LoginController
        public IActionResult Index()
        {
            return View();
        }

        // POST: LoginController/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                // ENCRIPTACION DE PASS
                string encryptPass = EncryptPassword(login.Password);
                var loginUsuario = db.Login.Where(l => l.Usuario == login.Usuario && l.Password == encryptPass).FirstOrDefault();

                if (loginUsuario != null) // es valido 
                {
                    HttpContext.Session.SetString("User", loginUsuario.Usuario);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["mensaje"] = "Los datos ingresados son incorrectos.";
                    return View("Index");
                }
            }

            return View("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // cerramos la sesion del usuario
            return View("Index");
        }

        private string EncryptPassword(string loginPass)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(loginPass)); // convierte string en byte (pasa a binario)

                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    stringBuilder.Append(bytes[i].ToString("x2")); //convertimos de binario a hexadecimal, y concatenamos
                }

                return stringBuilder.ToString();
            }
        }
    }
}
