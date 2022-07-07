using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using WebApiRest.Models;
using System.Data;

namespace WebApiRest.Data
{
    public class RegistroData
    {
        public static bool Registrar(Registro oregistro)
        {
            using (MySqlConnection connection = new MySqlConnection(Conexion.ConexionString))
            {
                MySqlCommand cmd = new MySqlCommand("usp_registar", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", oregistro.Nombre);
                cmd.Parameters.AddWithValue("@Apellidos", oregistro.Apellidos);
                cmd.Parameters.AddWithValue("@User", oregistro.User);
                cmd.Parameters.AddWithValue("@Password", oregistro.Password);
                cmd.Parameters.AddWithValue("@Correo", oregistro.Correo);
                cmd.Parameters.AddWithValue("@FechadeNacimiento", oregistro.User);
                cmd.Parameters.AddWithValue("@Sexo", oregistro.Sexo);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }

            }

        }

        public static bool Modificar(Registro oregistro)
        {
            using (MySqlConnection connection = new MySqlConnection(Conexion.ConexionString))
            {
                MySqlCommand cmd = new MySqlCommand("usp_registar", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Token", oregistro.Nombre);
                cmd.Parameters.AddWithValue("@Nombre", oregistro.Nombre);
                cmd.Parameters.AddWithValue("@Apellidos", oregistro.Apellidos);
                cmd.Parameters.AddWithValue("@User", oregistro.User);
                cmd.Parameters.AddWithValue("@Password", oregistro.Password);
                cmd.Parameters.AddWithValue("@Correo", oregistro.Correo);
                cmd.Parameters.AddWithValue("@FechadeNacimiento", oregistro.User);
                cmd.Parameters.AddWithValue("@Sexo", oregistro.Sexo);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static List<Registro> Listar()
        {
            List<Registro> oListaRegistro = new List<Registro>();
            using (MySqlConnection connection = new MySqlConnection(Conexion.ConexionString))
            {
                MySqlCommand cmd = new MySqlCommand("usp_listar", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oListaRegistro.Add(new Registro()
                            {
                                Token = Convert.ToInt32(dr["Token"]),
                                Nombre = dr["Nombre"].ToString(),
                                Apellidos = dr["Apellidos"].ToString(),
                                User = dr["User"].ToString(),
                                Password = dr["Password"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                FechadeNacimiento = Convert.ToDateTime(dr["FechadeNacimiento"].ToString()),
                                Sexo = dr["Sexo"].ToString()
                              });
                        }
                    }
                    return oListaRegistro;
                }
                catch (Exception ex)
                {
                    return oListaRegistro;
                }

            }
        }


        public static Registro Obtener(int Token)
        {
            Registro oregistro = new Registro();
            using (MySqlConnection connection = new MySqlConnection(Conexion.ConexionString))
            {
                MySqlCommand cmd = new MySqlCommand("usp_obtener", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Token", Token);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oregistro = new Registro()
                            {
                                Token = Convert.ToInt32(dr["Token"]),
                                Nombre = dr["Nombre"].ToString(),
                                Apellidos = dr["Apellidos"].ToString(),
                                User = dr["User"].ToString(),
                                Password = dr["Password"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                FechadeNacimiento = Convert.ToDateTime(dr["FechadeNacimiento"].ToString()),
                                Sexo = dr["Sexo"].ToString()
                            };
                        }
                    }

                    return oregistro;
                }
                catch (Exception ex)
                {
                    return oregistro;
                };
            }
        }

        public static bool Eliminar(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Conexion.ConexionString))
            {

                MySqlCommand cmd = new MySqlCommand("usp_eliminar", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Token", id);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }



    }
}
