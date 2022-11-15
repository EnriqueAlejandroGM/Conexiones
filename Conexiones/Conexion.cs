using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conexiones
{
    internal class Conexion
    {

        SqlConnection conectada = new SqlConnection(@"Data Source = localhost; Initial Catalog = Escuela2A; Integrated Security = true");

        public void Conectar()
        {
            try
            {
                conectada.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void Desconectar()
        {
            conectada.Close();
        }
        public bool Altas(string nom, string ape, int edad)
        {
            bool correcto = false;
            try
            {
                Conectar();
                SqlCommand cmd = new SqlCommand("Insert Into Alumnos Values(@Nombre, @Apellidos, @Edad)", conectada);
                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = nom;
                cmd.Parameters.Add("@Apellidos", SqlDbType.VarChar, 50).Value = ape;
                cmd.Parameters.Add("@Edad", SqlDbType.Int, 3).Value = edad;
                cmd.ExecuteNonQuery();
                correcto = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Desconectar();
            }
            return correcto;
        }
        
        public DataTable ConsultarAlumno(int mat)
        {
            DataTable dt = new DataTable();
            try
            {
                Conectar();
                SqlCommand cmd = new SqlCommand("Select * From Alumnos Where Matrícula = @Matricula", conectada);
                cmd.Parameters.Add("@Matricula", SqlDbType.Int, 3).Value = mat;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Desconectar();
            }
            return dt;
        }

        public bool Cambios(int mat, string nom, string ape, int edad)
        {
            bool cambios = false;
            try
            {
                Conectar();
                SqlCommand cmd = new SqlCommand("Update Alumnos set Nombres = @Nombre, Apellidos = @Apellido, Edad = @Edad where Matrícula = @Matricula", conectada);
                cmd.Parameters.Add("@Matricula", SqlDbType.Int, 3).Value = mat;
                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = nom;
                cmd.Parameters.Add("@Apellido", SqlDbType.VarChar, 50).Value = ape;
                cmd.Parameters.Add("@Edad", SqlDbType.Int, 3).Value = edad;
                cmd.ExecuteNonQuery();
                cambios = true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Desconectar();
            }
            return cambios;
        }


    public bool Baja(int mat)
        {
            bool baja;
            try
            {
                Conectar();
                SqlCommand cmd = new SqlCommand("DELETE FROM Alumnos WHERE Matrícula = @Matricula;", conectada);
                cmd.Parameters.Add("@Matricula", SqlDbType.Int, 3).Value = mat;
                cmd.ExecuteNonQuery();
                baja = true;

            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                baja = false;
            }
            finally
            {
                Desconectar();
            }
            return baja;
        }
        public DataTable ConsultaGeneral()
        {
            DataTable dt = new DataTable();
            try
            {
                Conectar();
                SqlCommand cmd = new SqlCommand("Select * From Alumnos ", conectada);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Desconectar();
            }
            return dt;
        }
    }
}

