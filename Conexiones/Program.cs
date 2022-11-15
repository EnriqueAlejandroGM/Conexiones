using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conexiones
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string nom, ape;
            int opc, edad, mat, baja;
            opc = 0;
            Conexion datos1 = new Conexion();
            while (opc != 6)
            {
                Console.Clear();
                Console.WriteLine("Enrique Alejandro Galván Montes\n");
                Console.WriteLine("\t\tMódulo ");
                Console.WriteLine("1.- Alta de alumnos");
                Console.WriteLine("2.- Baja de Alumnos");
                Console.WriteLine("3.- Cambios a los datos de los alumnos");
                Console.WriteLine("4.- Reporte general de alumnos");
                Console.WriteLine("5.- Consulta de Alumno");
                Console.WriteLine("6.- Salir del programa");
                Console.WriteLine("\n\nSeleccione una opcion");
                opc = Convert.ToInt32(Console.ReadLine());

                switch (opc)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("\t\tAlta de Alumnos\n\n");
                        Console.Write("Nombre de Alumno: ");
                        nom = Console.ReadLine();
                        Console.Write("Apellidos del alumno: ");
                        ape = Console.ReadLine();
                        Console.Write("Edad del alumno: ");
                        edad = Convert.ToInt32(Console.ReadLine());
                        
                        if(datos1.Altas(nom, ape, edad))
                        {
                            Console.WriteLine("Alta exitosa");
                        }
                        else
                        {
                            Console.WriteLine("No se pudo realizar el alta");
                        }
                        Console.WriteLine("Presione enter para regresar al menú");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("\t\t Baja de Alumnos\n\n");
                        Console.Write("Capture la matrícula del alumno: ");
                        mat = Convert.ToInt32(Console.ReadLine());

                        DataTable dt = new DataTable();
                        dt = datos1.ConsultarAlumno(mat);
                        if(dt.Rows.Count > 0)
                        {
                            nom = dt.Rows[0][1].ToString();
                            ape = dt.Rows[0][2].ToString();
                            edad = Convert.ToInt32(dt.Rows[0][3]);
                            Console.WriteLine("Nombre: {0}\nApellidos: {1}\nMatrícula: {2}", nom, ape, edad);
                            Console.WriteLine("Desea dar de baja al alumno?\n1.- Si\t2.- No");
                            baja = Convert.ToInt32(Console.ReadLine());
                            switch (baja)
                            {
                                case 1:
                                    if (datos1.Baja(mat))
                                    {
                                        Console.WriteLine("Baja exitosa");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Hubo un error en su baja");
                                    }
                                    break;
                                case 2:
                                    Console.WriteLine("Entendible, tenga un buen día");
                                    break;
                                default:
                                    Console.WriteLine("Opción incorrecta");
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ese alumno no está registrado o no existe");
                        }
                        


                        Console.WriteLine("Presione enter para volver al menú");
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("\t\t\tCambio a los Datos\t\t\t");
                        Console.Write("Capture la matrícula del alumno: ");
                        mat = Convert.ToInt32(Console.ReadLine());
                        dt = datos1.ConsultarAlumno(mat);
                        if (dt.Rows.Count > 0)
                        {
                            nom = dt.Rows[0][1].ToString();
                            ape = dt.Rows[0][2].ToString();

                            edad = Convert.ToInt32(dt.Rows[0][3].ToString());

                            Console.WriteLine("Nombre: {0}\nApellidos: {1}\nEdad: {2}", nom, ape, edad);
                            Console.WriteLine("\n¿Que Dato desea cammbiar?");
                            Console.WriteLine("\n1.- Nombre\n2.- Apellidos\n3.- Edad");
                            Console.WriteLine("\nSeleccione una opción");
                            baja = Convert.ToInt32(Console.ReadLine());
                            switch (baja)
                            {
                                case 1:
                                    Console.Clear();
                                    Console.WriteLine("Capture los Nombres completos del alumno");
                                    nom = Console.ReadLine();
                                    break;
                                case 2:
                                    Console.Clear();
                                    Console.WriteLine("Capture los Apellidos completos del alumno");
                                    ape = Console.ReadLine();
                                    break;
                                case 3:
                                    Console.WriteLine("Capture la edad del alumno");
                                    edad = Convert.ToInt32(Console.ReadLine());
                                    break;
                                default:
                                    Console.WriteLine("Opción incorrecta");
                                    break;
                            };
                            if (datos1.Cambios(mat, nom, ape, edad))
                            {
                                Console.WriteLine("Cambios Realizados");
                            }
                            else
                            {
                                Console.WriteLine("No se pudieron aplicar los cambios");
                            }
                        }
                        else
                        {
                            Console.WriteLine("El alumno no se encuentra en el sistema");
                        }
                        Console.WriteLine("Presione ENTER para volver al menú");
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("\t\tReporte general de alumnos\n\n");
                        Console.WriteLine("--------------------------------------------------------------------------------------");
                        Console.WriteLine("Matricula\t     |Nombres\t\t   |Apellidos\t       |Edad");
                        Console.WriteLine("--------------------------------------------------------------------------------------");
                        dt = datos1.ConsultaGeneral();
                        int i = 0;

                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                Console.WriteLine(String.Format("{0,-10} | {1,-19} | {2, -17} | {3,-17}", dt.Rows[i][0].ToString() + "\t\t", dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString() , dt.Rows[i][3].ToString()));
                                i++;
                            }

                        }
                        else
                        {
                            Console.WriteLine("La lista se encuentra vacia");

                        }
                        Console.WriteLine("--------------------------------------------------------------------------------------");
                        Console.WriteLine("Presione Enter para regresar al menu");
                        Console.ReadKey();
                        break;
                    case 5:
                        Console.Clear();
                        Console.Write("Consulta de Alumno.");
                        Console.Write("Capture la matrícula del alumno: ");
                        mat = Convert.ToInt32(Console.ReadLine());

                        dt = new DataTable();
                        dt = datos1.ConsultarAlumno(mat);
                        if (dt.Rows.Count > 0)
                        {
                            nom = dt.Rows[0][1].ToString();
                            ape = dt.Rows[0][2].ToString();
                            edad = Convert.ToInt32(dt.Rows[0][3]);
                            Console.WriteLine("Nombre: {0}\nApellidos: {1}\nMatrícula: {2}", nom, ape, edad);
                        }
                        else
                        {
                            Console.WriteLine("Ese alumno no está registrado o no existe...");
                        }
                        Console.WriteLine("Presione ENTER para volver al menú");
                        Console.ReadKey();
                        break;
                    case 6:
                        Console.Clear();
                        Console.WriteLine("Saliendo del programa...");
                        Console.WriteLine("Presione ENTER para continuar");
                        Console.ReadKey();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Esa opción no existe...");
                        Console.WriteLine("Presione ENTER para continuar");
                        Console.ReadKey();
                        break;
                }
            }

        }
    }
}