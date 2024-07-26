using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacionAdmin.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Clientes()
        {
            return View();
        }
    }
}