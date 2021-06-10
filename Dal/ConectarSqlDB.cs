using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Resources;
using Dal.Properties;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Dal
{

    public class ConectarSqlDB : IConectarDB
    {
        private string _nombreConexion = "SqlSeguridad";

        internal ConectarSqlDB()
        {
           
        }
        internal ConectarSqlDB(string nombreConexion)
        {
            this._nombreConexion = nombreConexion;
        }

        public IDbConnection CrearNuevaConexion()
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            
            IConfiguration configuration = builder.Build();

          
            string connString;
            // obtener la cadena de conexion del archivo de configuracion de la aplicacion (appsettings.json en core)
           connString = configuration[this._nombreConexion].ToString();// Properties. Resource

            // declarar e instanciar una nueva instancia de un objeto del tipo SqlConnection
            System.Data.SqlClient.SqlConnection conexion = new  SqlConnection(connString);
            return conexion;
        }

        public IDbCommand CrearNuevoComando(string textoComando, IDbConnection conexion)
        {
            

            if (conexion == null)
            {
                conexion = this.CrearNuevaConexion();
            }

            SqlCommand comando = new SqlCommand(textoComando, (SqlConnection)conexion);

            return comando;
        }

        public IDbCommand CrearNuevoComando(string textoComando, IDbConnection conexion, string v_type)
        {
            if (conexion == null)
            {
                conexion = this.CrearNuevaConexion(); 
            }

            SqlCommand comando = new SqlCommand(textoComando, (SqlConnection)conexion);
            if (v_type == "SP")
            {
                comando.CommandType = CommandType.Text;
                comando.CommandType = CommandType.StoredProcedure; 

            }



            return comando;
        }
        public IDbCommand CrearNuevoComando(string textoComando)
        {
            return this.CrearNuevoComando(textoComando, null);

        }

        public IDataParameter AgregarParametroAComando(IDbCommand comando, string nombreParametro, object valorParametro)
        {
            return ((SqlCommand)comando).Parameters.AddWithValue(nombreParametro, valorParametro);

        }

        public IDataParameter AgregarParametroAComandoOut(IDbCommand comando, string nombreParametro)
        {
            SqlParameter outPutParameter = new SqlParameter();
            outPutParameter.ParameterName = nombreParametro;
            outPutParameter.SqlDbType = SqlDbType.VarChar;
            outPutParameter.Size = 500;
            outPutParameter.Direction = System.Data.ParameterDirection.Output;
            ((SqlCommand)comando).Parameters.Add(outPutParameter);

            return outPutParameter;
        }

        public System.Data.DataTable ExecuteDataTable(System.Data.IDbCommand comando, string nombreTabla)
        {
            SqlDataAdapter adaptador = new SqlDataAdapter((SqlCommand)comando);
            DataTable tabla = new DataTable(nombreTabla);
            adaptador.Fill(tabla);
            return tabla;
        }

        public int ExecuteNonQuery(System.Data.IDbCommand comando)
        {
            this.VerificarConexion(ref comando);

            return comando.ExecuteNonQuery();
        }

        public System.Data.IDataReader ExecuteReader(System.Data.IDbCommand comando)
        {
            this.VerificarConexion(ref comando, true);

            return comando.ExecuteReader();
        }

        private void VerificarConexion(ref System.Data.IDbCommand comando)
        {
            this.VerificarConexion(ref comando, false);
        }

        private void VerificarConexion(ref System.Data.IDbCommand comando, bool forzarNueva)
        {
            if (comando.Connection == null || forzarNueva)
                comando.Connection = this.CrearNuevaConexion();

            if (comando.Connection.State != ConnectionState.Open)
                comando.Connection.Open();
        }

        public void Close(System.Data.IDbCommand comando)
        {
            if (comando.Connection != null)
            {
                if (comando.Connection.State != ConnectionState.Closed)
                {
                    comando.Connection.Close();
                }
            }
        }


        //mi helper
        public static Int64 VeriInt64Sql(IDataReader lector, string s)
        {

            if (lector[s] == DBNull.Value)
            {

                return 0;
            }
            else
            {

                return Convert.ToInt64(lector[s]);

            }


        }
        public static Int32 VeriIntSql(IDataReader lector, string s)
        {

            if (lector[s] == DBNull.Value)
            {

                return 0;
            }
            else
            {

                return Convert.ToInt32(lector[s]);

            }


        }
        public static Double VeriDoubleSql(IDataReader lector, string s)
        {

            if (lector[s] == DBNull.Value)
            {

                return 0;
            }
            else
            {

                return Convert.ToDouble(lector[s]);

            }


        }

        public static string VerifStringSql(IDataReader lector, string s)
        {

            if (lector[s] == DBNull.Value)
            {

                return "";
            }
            else
            {

                return Convert.ToString(lector[s]);

            }


        }
    }
}
