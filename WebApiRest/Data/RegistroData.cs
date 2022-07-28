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
            //Generacion de token
            var randomnumber = new Random().Next(100000, 999999);
            oregistro.Token = randomnumber;
            bool Validacion = false;

            using (MySqlConnection connection = new MySqlConnection(Conexion.ConexionString))
            {
                //Validacion de que el token no existe en la base de datos
                while(Validacion == false) {
                    try
                    {
                        MySqlCommand cmd1 = new MySqlCommand("usp_token", connection);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("token", oregistro.Token);
                        connection.Open();
                        cmd1.ExecuteNonQuery();
                        using (MySqlDataReader dr = cmd1.ExecuteReader())

                        {
                            if (dr.Read())
                            {
                                randomnumber = new Random().Next(1000000, 99999999);
                                oregistro.Token = randomnumber;
                            }
                            else
                            {
                                Validacion = true;
                            }
                        }
                    }
                    catch (Exception ex){ Validacion = true; }
                    finally { connection.Close(); }
                }
                //Registro de datos
                MySqlCommand cmd = new MySqlCommand("usp_registrar", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("token", oregistro.Token);
                cmd.Parameters.AddWithValue("nombre", oregistro.Nombre);
                cmd.Parameters.AddWithValue("apellidos", oregistro.Apellidos);
                cmd.Parameters.AddWithValue("user", oregistro.User);
                cmd.Parameters.AddWithValue("password", oregistro.Password);
                cmd.Parameters.AddWithValue("correo", oregistro.Correo);
                cmd.Parameters.AddWithValue("fechadeNacimiento", oregistro.User);
                cmd.Parameters.AddWithValue("sexo", oregistro.Sexo);

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
                finally { connection.Close(); }

            }

        }

        public static bool Modificar(Registro oregistro)
        {
            using (MySqlConnection connection = new MySqlConnection(Conexion.ConexionString))
            {
                MySqlCommand cmd = new MySqlCommand("usp_modificar", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", oregistro.Token);
                cmd.Parameters.AddWithValue("nombre", oregistro.Nombre);
                cmd.Parameters.AddWithValue("apellidos", oregistro.Apellidos);
                cmd.Parameters.AddWithValue("user", oregistro.User);
                cmd.Parameters.AddWithValue("password", oregistro.Password);
                cmd.Parameters.AddWithValue("correo", oregistro.Correo);
                cmd.Parameters.AddWithValue("fechadeNacimiento", oregistro.User);
                cmd.Parameters.AddWithValue("sexo", oregistro.Sexo);

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
                finally { connection.Close(); }
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
                finally { connection.Close(); }

            }
        }
        public static Registro Obtener(string Usuario)
        {
            Registro oregistro = new Registro();
            using (MySqlConnection connection = new MySqlConnection(Conexion.ConexionString))
            {
                MySqlCommand cmd = new MySqlCommand("usp_obtener", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("usuario", Usuario);

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
                }
                finally { connection.Close(); }
            }
        }

        public static bool Eliminar(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(Conexion.ConexionString))
            {
                MySqlCommand cmd = new MySqlCommand("usp_eliminar", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
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
                finally { connection.Close(); }
            }
        }
    }
}
