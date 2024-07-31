using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaNegocio;
using CapaEntidad;


namespace CapaPresentacionAdmin.Controllers
{
    public class MantenedorController : Controller
    {
        // GET: Mantenedor
        public ActionResult Clientes()
        {
            return View();
        }

        //Pedidos
        #region Pedidos
        public ActionResult Pedido()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarPedidos()
        {
            List<Pedido> ListaPedidos = new CN_Pedido().ListarPedidos();

            return Json(new { data = ListaPedidos }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public JsonResult RegistrarPedido(Pedido pedidoARegistrar)
        {
            string Mensaje = string.Empty;
            object Resultado;

            // Calcula el total del pedido
            pedidoARegistrar.Total = pedidoARegistrar.Detalles.Sum(d => d.Producto.Precio * d.Cantidad);

            if (pedidoARegistrar.IDPedido == 0)
            {
                Resultado = new CN_Pedido().Registrar(pedidoARegistrar, out Mensaje);
            }
            else
            {
                Resultado = new CN_Pedido().Editar(pedidoARegistrar, out Mensaje);
            }

            return Json(new { Resultado = Resultado, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        //Productos
        #region Producto
        public ActionResult Producto()
        {
            return View();
        }
        [HttpGet]
        public JsonResult ListarProductos()
        {
            List<Producto> ListaProductos = new CN_Producto().ListarProductos();

            return Json(new { data = ListaProductos }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RegistrarProducto(Producto productoARegistrar)
        {
            string Mensaje = string.Empty;
            object Resultado;

            decimal precio;

            if (decimal.TryParse(productoARegistrar.Precio.ToString(), out precio))
            {
                productoARegistrar.Precio = precio;
            } else
            {
                return Json(new { Resultado = false, Mensaje = "El campo Precio debe ser un número" }, JsonRequestBehavior.AllowGet);
            }

            if (productoARegistrar.IDProducto == 0)
            {
                Resultado = new CN_Producto().Registrar(productoARegistrar, out Mensaje);
            }
            else
            {
                Resultado = new CN_Producto().Editar(productoARegistrar, out Mensaje);
            }

            return Json(new { Resultado = Resultado, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public JsonResult EliminarProducto(int IDProducto)
        {
            bool Respuesta = false;
            string Mensaje = string.Empty;

            Respuesta = new CN_Producto().Eliminar(IDProducto, out Mensaje);

            return Json(new { Respuesta = Respuesta, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}