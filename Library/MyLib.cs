using System.Data;
using System.Diagnostics;
using Biblioteca.Models;

namespace Biblioteca.Library
{
    public class MyLib
    {
        //static Para poder llamarla desde cualquier parte sin instanciar Mylib.llenas()
        public static string llenar(string tabla, string id_campo, string campo, string val,string order) 
        {

            Modelo modelo = new Modelo();
            var dt = modelo.Llenar(tabla, campo);
            string r = "";

            while (dt.Read())
            {
                if (dt.GetValue(dt.GetOrdinal(id_campo)).ToString() != val)
                {
                    r = r + "<option value=\"" + dt.GetValue(dt.GetOrdinal(id_campo)) + "\">" + dt.GetValue(dt.GetOrdinal(campo)) + "</option>\n";
                }
                else
                {
                    r = r + "<option value=\"" + dt.GetValue(dt.GetOrdinal(id_campo)) + "\" selected>" + dt.GetValue(dt.GetOrdinal(campo)) + "</option>\n";
                }
            }

            if (string.IsNullOrEmpty(val))
            {
                r = r + "<option value=\"\" selected></option>";
            }
            return r;

        }

    }
}
