﻿using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using System.Data.OleDb;
using System.Diagnostics;

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
            var query = "SELECT *, CASE WHEN id_estado = 3 THEN 1 ELSE 0 END AS Bloqueado FROM usuario WHERE nickname = " + nickname + " AND " + password + ";";


            com.Conectar();
            com.CrearComando(query);
            //return com.EjecutarConsulta();
            var datos = com.EjecutarConsulta();

            if (datos.Read())
            {
                // Devolver el OleDbDataReader con la columna "Bloqueado" agregada
                return datos;
            }
            else
            {
                // Credenciales inválidas o usuario no encontrado
                return null;
            }
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
    public OleDbDataReader Llenar(string tabla, string campo)
    {
        BaseDatos com = new BaseDatos(); //Instanciando a la clase BaseDatos

        try
        {
            var query = "SELECT * FROM " + tabla + " WHERE id_estado = '1' ORDER BY " + campo + " ASC";

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

    public OleDbDataReader Mostrar(string tabla, string id_usuario)
    {
        BaseDatos com = new BaseDatos(); //Instanciando a la clase BaseDatos

        try
        {
            var query = "SELECT * FROM " + tabla + " WHERE id_estado = " + id_usuario;

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


    public void Actualizar(string tabla, string[] datos, string campo, string valor)
    {
        BaseDatos com = new BaseDatos();
        com.Conectar();
        string sql = "UPDATE " + tabla + " SET ";
        foreach (string data1 in datos)
        {
            sql += data1.Split(":")[0] + " = '" + data1.Split(":")[1] + "', ";
        }
        sql = sql.Substring(0, sql.Length - 2) + " WHERE " + campo + " = '" + valor + "';";
        Debug.WriteLine(sql);
        com.CrearComando(sql);
        com.EjecutarComando();
        com.Desconectar();
    }

    public OleDbDataReader ObtenerUser(string tabla, string id)
    {
        BaseDatos com = new BaseDatos();

        try
        {
            com.Conectar();
            com.CrearComando("SELECT * FROM " + tabla + " WHERE id_usuario= " + id);
            return com.EjecutarConsulta();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al ejecutar consulta: " + ex.Message);
            return null;
        }
    }
    public OleDbDataReader ObtenerLibro(string tabla, string id)
    {
        BaseDatos com = new BaseDatos();

        try
        {
            com.Conectar();
            com.CrearComando("SELECT * FROM " + tabla + " WHERE id_libro= " + id);
            return com.EjecutarConsulta();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al ejecutar consulta: " + ex.Message);
            return null;
        }
    }


    public void BloquearUsuario(string tabla, string[] datos, string idUsuario)
    {
        BaseDatos com = new BaseDatos(); // Instancia a la base de datos
        com.Conectar(); // Se conecta a la instancia
        string sql = "UPDATE " + tabla + " SET "; // Query de actualización

        foreach (string data1 in datos)
        {
            string[] partes = data1.Split(':');
            sql += partes[0] + " = '" + partes[1] + "', ";
        }
        sql = sql.Substring(0, sql.Length - 2);

        // Agregar cláusula WHERE para actualizar solo el usuario específico
        sql += " WHERE id_usuario = '" + idUsuario + "'";

        com.CrearComando(sql);
        // Debug.WriteLine(sql);
        com.EjecutarComando();
        com.Desconectar();
    }

}
