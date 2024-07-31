using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Pedido
    {
        public List<Pedido> ListarPedidos()
        {
            List<Pedido> lista = new List<Pedido>();

            try
            {
                using (SqlConnection Oconnection = new SqlConnection(Conexion.Cn))
                {
                    StringBuilder query = new StringBuilder();
                    query.Append("Select p.ID_Pedido, p.FechaPedido, p.Total, p.Estado, c.ID_Cliente, c.Nombre[NombreCliente], c.Apellido[ApellidoCliente], c.Telefono[TelefonoCliente] From Pedidos p Inner Join Clientes c On p.ID_Cliente = c.ID_Cliente");

                    SqlCommand cmd = new SqlCommand(query.ToString(), Oconnection);
                    cmd.CommandType = CommandType.Text;

                    Oconnection.Open();

                    using (SqlDataReader Reader = cmd.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            lista.Add(
                                new Pedido()
                                {
                                    IDPedido = (int)Reader["ID_Pedido"],
                                    Fecha = (DateTime)Reader["FechaPedido"],
                                    Total = (decimal)Reader["Total"],
                                    Estado = (bool)Reader["Estado"],
                                    IDCliente = (int)Reader["ID_Cliente"]
                                }
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public int Registrar(Pedido pedidoARegistrar, out string Mensaje)
        {
            int IDPedido;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection Oconnection = new SqlConnection(Conexion.Cn))
                {
                    SqlCommand cmd = new SqlCommand("SP_RegistrarPedido", Oconnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IDCliente", pedidoARegistrar.IDCliente);
                    cmd.Parameters.AddWithValue("@FechaPedido", pedidoARegistrar.Fecha);
                    cmd.Parameters.AddWithValue("@Total", pedidoARegistrar.Total);
                    cmd.Parameters.AddWithValue("@Estado", pedidoARegistrar.Estado);
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@IDPedido", SqlDbType.Int).Direction = ParameterDirection.Output;

                    Oconnection.Open();

                    cmd.ExecuteNonQuery();

                    IDPedido = Convert.ToInt32(cmd.Parameters["@IDPedido"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IDPedido;
        }

        public bool Editar(Pedido pedidoAEditar, out string Mensaje)
        {
            bool Resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection Oconnection = new SqlConnection(Conexion.Cn))
                {
                    SqlCommand cmd = new SqlCommand("SP_EditarPedido", Oconnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IDPedido", pedidoAEditar.IDPedido);
                    cmd.Parameters.AddWithValue("@Total", pedidoAEditar.Total);
                    cmd.Parameters.AddWithValue("@Estado", pedidoAEditar.Estado);
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    Oconnection.Open();

                    cmd.ExecuteNonQuery();

                    Resultado = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Resultado;
        }

        public bool Eliminar(int IDPedido, out string Mensaje)
        {
            bool Resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection Oconnection = new SqlConnection(Conexion.Cn))
                {
                    SqlCommand cmd = new SqlCommand("SP_EliminarPedido", Oconnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IDPedido", IDPedido);
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    Oconnection.Open();

                    cmd.ExecuteNonQuery();

                    Resultado = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Resultado;
        }
    }
}
