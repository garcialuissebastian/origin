using Dal.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal
{
  public class FactoriaConectorDB
{
    public static IConectarDB ObtenerConector(string nombreConexion)
    {
     

            if (nombreConexion != null)
        {
            if (nombreConexion.Equals("SqlServer"))

                return new ConectarSqlDB(nombreConexion);
            else
                throw new ArgumentException("El Proveedor especificado para la conexión no es válido.");
        }
        else
            throw new ArgumentOutOfRangeException("nombreConexion", "No se encontró la cadena de conexión con el nombre especificado.");
    }
}

}
