using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Be;
namespace Dal
{
    public class DalTarjetas  
    {
        private IConectarDB conectorDB;

        public DalTarjetas()
        {
            conectorDB = FactoriaConectorDB.ObtenerConector("SqlServer");
        }
         
        public Tarjetas ConsultarUno(Tarjetas filtro)
        {
            List<Tarjetas> lista = this.Listar(filtro);
            if (lista.Count > 0)
                return lista[0];
            else
                return null;
        }


        public bool ActualizarIntentos(Int64? v_nro,int v_tipo)
        {// tipo es si ingreso correcto o no
            int registrosAfectados = 0;
            IDbCommand cmm =null;
            try
            {
                string cmdTxt = "ActualizarIntentos";
                 cmm = this.conectorDB.CrearNuevoComando(cmdTxt, null, "SP");
                this.conectorDB.AgregarParametroAComando(cmm, "nro", v_nro);
                this.conectorDB.AgregarParametroAComando(cmm, "tipo", v_tipo);

                IDataParameter outSal22 = this.conectorDB.AgregarParametroAComandoOut(cmm, "estado");
               
                this.conectorDB.ExecuteNonQuery(cmm);
                registrosAfectados  = Convert.ToInt32( outSal22.Value.ToString());
            }
            catch
            {
                throw;
            }
            finally
            {
                this.conectorDB.Close(cmm);
            }

            return (registrosAfectados > 0);
        }
        public List<Tarjetas> Listar(Tarjetas filtro)
        {
            List<Tarjetas> lista = new List<Tarjetas>();

            string cmdTxt = "SELECT Id_Tarjeta, Nro, convert(char,Fecha_Vto, 103) Fecha_Vto,Balance,Bloqueo,Intentos,Pin ";
            cmdTxt += "FROM Tarjetas ";
            cmdTxt += "WHERE (Id_Tarjeta = @Id_Tarjeta OR @Id_Tarjeta IS NULL) ";
            cmdTxt += "AND  (Nro= @Nro OR @Nro IS NULL) ";
            cmdTxt += "AND  (Pin= @Pin OR @Pin IS NULL) ";
            IDbCommand comando = this.conectorDB.CrearNuevoComando(cmdTxt);
            try
            {
                if (filtro != null && filtro.Id_Tarjeta > 0)
                    this.conectorDB.AgregarParametroAComando(comando, "@Id_Tarjeta", filtro.Id_Tarjeta);
                else
                    this.conectorDB.AgregarParametroAComando(comando, "@Id_Tarjeta", DBNull.Value);

                if (filtro != null && filtro.Nro > 0)
                    this.conectorDB.AgregarParametroAComando(comando, "@Nro", filtro.Nro);
                else
                    this.conectorDB.AgregarParametroAComando(comando, "@Nro", DBNull.Value);

             
                if (filtro != null && !string.IsNullOrEmpty(filtro.Pin))
                        this.conectorDB.AgregarParametroAComando(comando, "@Pin", filtro.Pin);
                else
                    this.conectorDB.AgregarParametroAComando(comando, "@Pin", DBNull.Value);

                IDataReader lector = this.conectorDB.ExecuteReader(comando);
                while (lector.Read())
                {//SELECT Id_Tarjeta, Nro,Fecha_Vto,Balance,Bloqueo,Intentos,Pin

                    Tarjetas entidad = new Tarjetas();
                    entidad.Id_Tarjeta = ConectarSqlDB.VeriIntSql(lector,"Id_Tarjeta");
                    entidad.Nro = ConectarSqlDB.VeriInt64Sql(lector,"Nro"); 
                    entidad.Fecha_Vto = ConectarSqlDB.VerifStringSql(lector,"Fecha_Vto");
                    entidad.Balance = ConectarSqlDB.VerifStringSql(lector,"Balance");
                    entidad.Bloqueo = ConectarSqlDB.VerifStringSql(lector,"Bloqueo");
                    entidad.Intentos = ConectarSqlDB.VeriIntSql(lector,"Intentos");
                    entidad.Pin = ConectarSqlDB.VerifStringSql(lector,"Pin");
                    lista.Add(entidad);
                }
                // cerrar lector
                lector.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                this.conectorDB.Close(comando);
            }

            return lista;
        }

        public  Tarjetas ReporteUltimoRetiro(Int32 v_tj)
        {
            Tarjetas entidad = new Tarjetas();
            IDbCommand comando = null;
            try
            {
                string cmdTxt = "SELECT Id_Tarjeta, Nro, convert(char,Fecha_Vto, 103) Fecha_Vto,Balance,Bloqueo,Intentos,Pin ";
                cmdTxt += "FROM Tarjetas ";
                cmdTxt += "WHERE (Id_Tarjeta = @Id_Tarjeta OR @Id_Tarjeta IS NULL) ";

               comando = this.conectorDB.CrearNuevoComando(cmdTxt);


                this.conectorDB.AgregarParametroAComando(comando, "@Id_Tarjeta", v_tj);
                
                IDataReader lector = this.conectorDB.ExecuteReader(comando);
               
                while (lector.Read())
                {                    
                    entidad.Id_Tarjeta = ConectarSqlDB.VeriIntSql(lector, "Id_Tarjeta");
                    entidad.Nro = ConectarSqlDB.VeriInt64Sql(lector, "Nro");
                    entidad.Fecha_Vto = ConectarSqlDB.VerifStringSql(lector, "Fecha_Vto");
                    entidad.Balance = ConectarSqlDB.VerifStringSql(lector, "Balance");
                    entidad.Bloqueo = ConectarSqlDB.VerifStringSql(lector, "Bloqueo");
                    entidad.Intentos = ConectarSqlDB.VeriIntSql(lector, "Intentos");
                    entidad.Pin = ConectarSqlDB.VerifStringSql(lector, "Pin");
                    
                }
                // cerrar lector
                lector.Close();


                 cmdTxt = "SELECT  Id_Tarjeta, convert(char,Fecha_Op, 103) Fecha_Op_h,CONVERT(varchar(5),Fecha_Op,14) Fecha_Op_s, Cod_Op, Monto  ";
                cmdTxt += "FROM  Operaciones ";
                cmdTxt += " WHERE Id_Operacion = (select max(op.Id_Operacion) from Operaciones op where op.Id_Tarjeta = @Id_Tarjeta )   ";
                comando = null;
                comando = this.conectorDB.CrearNuevoComando(cmdTxt); 

                this.conectorDB.AgregarParametroAComando(comando, "@Id_Tarjeta", v_tj);

                  lector = this.conectorDB.ExecuteReader(comando);
                List<Operaciones> Lista = new List<Operaciones>();

                while (lector.Read())
                {
                    Operaciones entidadOp = new Operaciones();
                    entidadOp.Id_Tarjeta = ConectarSqlDB.VeriIntSql(lector, "Id_Tarjeta"); 
                    entidadOp.Fecha_Op = ConectarSqlDB.VerifStringSql(lector, "Fecha_Op_h").Trim()+' ' + ConectarSqlDB.VerifStringSql(lector, "Fecha_Op_s").Trim();
                    entidadOp.Cod_Op = ConectarSqlDB.VeriIntSql(lector, "Cod_Op");
                    entidadOp.Monto = ConectarSqlDB.VerifStringSql(lector, "Monto");
                    Lista.Add(entidadOp);
                   
                }
                // cerrar lector
                lector.Close();

                entidad.ListOperaciones = Lista;
            }
            catch
            {
                throw;
            }
            finally
            {
                this.conectorDB.Close(comando);
            }

            return entidad;
        }
        public bool Retiro(Int32 v_tj, string v_monto)
        {// tipo es si ingreso correcto o no
            int registrosAfectados = 0;
            IDbCommand cmm = null;
            try
            {
                string cmdTxt = "Retiro";
                cmm = this.conectorDB.CrearNuevoComando(cmdTxt, null, "SP");
                this.conectorDB.AgregarParametroAComando(cmm, "Id_Tarjeta", v_tj);
                this.conectorDB.AgregarParametroAComando(cmm, "Monto", v_monto);

                IDataParameter outSal22 = this.conectorDB.AgregarParametroAComandoOut(cmm, "estado");

                this.conectorDB.ExecuteNonQuery(cmm);
                registrosAfectados = Convert.ToInt32(outSal22.Value.ToString());
            }
            catch
            {
                throw;
            }
            finally
            {
                this.conectorDB.Close(cmm);
            }

            return (registrosAfectados > 0);
        }

    }
}
