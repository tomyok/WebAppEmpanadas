using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Pedido
    {
        private CD_Pedido AuxCapaDatoPedido = new CD_Pedido();

        public List<Pedido> ListarPedidos()
        {
            return AuxCapaDatoPedido.ListarPedidos();
        }

        public int Registrar(Pedido pedidoARegistrar, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (pedidoARegistrar.Fecha == null)
            {
                Mensaje = "El campo Fecha es obligatorio";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return AuxCapaDatoPedido.Registrar(pedidoARegistrar, out Mensaje);
            }
            else
            {
                return 0;
            }
        }
        public bool Editar(Pedido pedidoAEditar, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (pedidoAEditar.Fecha == null)
            {
                Mensaje = "El campo Fecha es obligatorio";
            }
            else if (pedidoAEditar.Total == 0)
            {
                Mensaje = "El campo Total es obligatorio";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return AuxCapaDatoPedido.Editar(pedidoAEditar, out Mensaje);
            }
            else
            {
                return false;
            }
        }

        public bool Eliminar(int IDPedido, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (IDPedido == 0)
            {
                Mensaje = "El ID del Pedido es invalido";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return AuxCapaDatoPedido.Eliminar(IDPedido, out Mensaje);
            }
            else
            {
                return false;
            }
        }
    }
}
