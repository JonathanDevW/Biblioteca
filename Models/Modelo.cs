using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using System.Data.OleDb;

/// <summary>
/// Contiene los metodos para el matenimiento de las tablas
/// </summary>
public class Modelo
{
    private OleDbCommand Command;

    public Modelo()
    {

    }

    /// <summary>
    /// Se encarga de verificar las credenciales en la base de datos.
    /// </summary>
    /// <param name="nickname">Usuario</param>
    /// <param name="password">Contraseña</param>
    /// <returns>Devuelve el registro si existe.</returns>
    public OleDbDataReader Login(string nickname, string password)
    {
        BaseDatos com = new BaseDatos(); //Instanciando a la clase BaseDatos

        try
        {
            var query = "SELECT * FROM usuario WHERE id_estado = '1' AND nickname = '" + nickname + "' AND password = '" + password + "';";

            com.Conectar();
            com.CrearComando(query);
            return com.EjecutarConsulta();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al ejecutar consulta: " + ex.Message);
            return null;
        }
    }


    public void Agregar(string tabla, string[] datos)
    {
        BaseDatos com = new BaseDatos(); //Instanci a base de datos
        com.Conectar(); // Se conecta a la instancia
        string sql = "INSERT INTO " + tabla + " ("; //Query
        foreach (string data1 in datos) //Sirven para despedazar el arreglo query
        {
            sql += data1.Split(':')[0] + ", ";
        }
        sql = sql.Substring(0, sql.Length - 2) + ") VALUES (";
        foreach (string data1 in datos)
        {
            sql += "'" + data1.Split(':')[1] + "', ";
        }
        sql = sql.Substring(0, sql.Length - 2) + ");";
        com.CrearComando(sql);
        //Debug.WriteLine(sql);
        com.EjecutarComando();
        com.Desconectar();
    }
    public OleDbDataReader BuscarTodo(string tabla)
    {
        BaseDatos com = new BaseDatos();
        try 
        { 
        var query = "SELECT * FROM " + tabla + ";";

        com.Conectar();
        com.CrearComando(query);
        return com.EjecutarConsulta();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al ejecutar consulta: " + ex.Message);
            return null;
        }
    }

}
