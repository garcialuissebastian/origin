using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Dal
{

    public interface IConectarDB
    {
        IDbConnection CrearNuevaConexion();
        IDbCommand CrearNuevoComando(string textoComando, IDbConnection conexion);
        IDataParameter AgregarParametroAComandoOut(IDbCommand comando, string nombreParametro);
        
            IDbCommand CrearNuevoComando(string textoComando, IDbConnection conexion, string v_type);
        IDbCommand CrearNuevoComando(string textoComando);
        IDataParameter AgregarParametroAComando(IDbCommand comando, string nombreParametro, object valorParametro);


        int ExecuteNonQuery(IDbCommand comando);
        IDataReader ExecuteReader(IDbCommand comando);
        DataTable ExecuteDataTable(IDbCommand comando, string nombreTabla);

        void Close(IDbCommand comando);
    }

}
