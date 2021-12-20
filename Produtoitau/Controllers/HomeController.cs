using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Produtoitau.Models;

namespace Produtoitau.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuarios u)
        {
            // esta action trata o post (login)
            if (ModelState.IsValid) //verifica se é válido
            {
                using (masterEntities dc = new masterEntities())
                {
                    var v = dc.Usuarios.Where(a => a.NomeLogin.Equals(u.NomeLogin) && a.Senha.Equals(u.Senha)).FirstOrDefault();
                    if (v != null)
                    {
                        Session["usuarioLogadoID"] = v.IDUsuario.ToString();
                        Session["nomeUsuarioLogado"] = v.NomeLogin.ToString();
                        return RedirectToAction("Produtos");
                    }
                }
            }
            return View(u);
        }

        public ActionResult Index()
        {
            if (Session["usuarioLogadoID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Produtos()
        {
   
            using (masterEntities dc = new masterEntities())
            {

                var p = dc.Produtos.Where(a => a.IDProduto > 0).OrderByDescending(a => a.Produto).ThenBy(a => a.IDProduto).FirstOrDefault();

                return View(p);
            }

        }

    }
}