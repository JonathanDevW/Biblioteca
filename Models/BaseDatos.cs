using System;
using System.Data;
using System.Data.OleDb;

public class BaseDatos
{
    public string? connect;
    private OleDbConnection? conexion;
    private OleDbCommand? comando;
    private string? cadenaConexion;

    public BaseDatos()
    {
        this.Configurar();
    }

    private void Configurar()
    {

        /*Datos de servidores*/
        string JonaServer = "DESKTOP-LH3NL7T";
        string JonaServerHP = "DESKTOP-KVAU16F";
        string RaquelServer = "LAPTOP-HQGAUREL";
        /*Datos de la cadena conexion*/
        string dataBase = "SisBibliotecario";
        string userId = "sa";
        string passConnection = "uma";

        /*Creando cadena conexion*/
        cadenaConexion = "Provider = sqloledb; Data Source = " + JonaServer + "; Initial Catalog = " + dataBase + "; User Id = " + userId + "; Password = " + passConnection + ";";
        conexion = new OleDbConnection(cadenaConexion);
    }

    public void Conectar()
    {
        try
        {
            if (conexion != null)
            {
                conexion.Open();
                this.connect = "true";
            }
        }
        catch
        {
            this.connect = "false";
        }
    }

    public void Desconectar()
    {
        if (this.conexion != null && this.conexion.State == ConnectionState.Open)
            this.conexion.Close();
    }

    public void CrearComando(string query)
    {
        if (conexion != null)
            comando = new OleDbCommand(query, conexion);
    }

    public OleDbDataReader? EjecutarConsulta()
    {
        if (comando != null)
            return this.comando.ExecuteReader();
        return null;
    }

    public object? EjecutarEscalar()
    {
        if (comando != null)
            return this.comando.ExecuteScalar();
        return null;
    }

    public void EjecutarComando()
    {
        if (comando != null)
            this.comando.ExecuteNonQuery();
    }
}
