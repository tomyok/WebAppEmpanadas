using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaNegocio;
using CapaEntidad;

namespace CapaPresentacionAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarClientes()
        {
            List<Cliente> ListaClientes = new CN_Cliente().ListarClientes();

            return Json(new { data = ListaClientes }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RegistrarCliente(Cliente clienteARegistrar)
        {
            string Mensaje = string.Empty;
            object Resultado;

            if(clienteARegistrar.IDCliente == 0)
            {
                Resultado = new CN_Cliente().Registrar(clienteARegistrar, out Mensaje);
            } else
            {
                Resultado = new CN_Cliente().Editar(clienteARegistrar, out Mensaje);
            }

            return Json(new { Resultado = Resultado, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}