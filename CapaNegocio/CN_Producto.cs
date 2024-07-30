using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocio
{
    public class CN_Producto
    {
        private CD_Producto AuxCapaDatoProducto = new CD_Producto();
        public List<Producto> ListarProductos()
        {
            return AuxCapaDatoProducto.ListarProductos();
        }

        public int Registrar(Producto ProductoARegistrar, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(ProductoARegistrar.Nombre) || string.IsNullOrWhiteSpace(ProductoARegistrar.Nombre))
            {
                Mensaje = "El campo Nombre es obligatorio.";
            }
            else if (string.IsNullOrEmpty(ProductoARegistrar.Descripcion) || string.IsNullOrWhiteSpace(ProductoARegistrar.Descripcion))
            {
                Mensaje = "El campo Descripcion es obligatorio.";
            }
            else if (ProductoARegistrar.Precio <= 0)
            {
                Mensaje = "El campo Precio tiene que ser mayor a 0.";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return AuxCapaDatoProducto.Registrar(ProductoARegistrar, out Mensaje);
            }
            else
            {
                return 0;
            }

        }

        public bool Editar(Producto ProductoAEditar, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (ProductoAEditar.IDProducto <= 0)
            {
                Mensaje = "El ID del Producto no es válido.";
            }
            else if (string.IsNullOrEmpty(ProductoAEditar.Nombre) || string.IsNullOrWhiteSpace(ProductoAEditar.Nombre))
            {
                Mensaje = "El campo Nombre es obligatorio.";
            }
            else if (string.IsNullOrEmpty(ProductoAEditar.Descripcion) || string.IsNullOrWhiteSpace(ProductoAEditar.Descripcion))
            {
                Mensaje = "El campo Descripcion es obligatorio.";
            }
            else if (ProductoAEditar.Precio <= 0)
            {
                Mensaje = "El campo Precio tiene que ser mayor a 0.";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return AuxCapaDatoProducto.Editar(ProductoAEditar, out Mensaje);
            }
            else
            {
                return false;
            }
        }

        public bool Eliminar(int IDProducto, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (IDProducto <= 0)
            {
                Mensaje = "El ID del Producto no es válido";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return AuxCapaDatoProducto.Eliminar(IDProducto, out Mensaje);
            }
            else
            {
                return false;
            }
        }
    }
}
