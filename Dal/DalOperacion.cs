using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Be;
namespace Dal
{
  public  class DalOperacion
    {
        private IConectarDB conectorDB;

        public DalOperacion()
        {
            conectorDB = FactoriaConectorDB.ObtenerConector("SqlServer");
        }

        public bool Agregar(Operaciones valor)
        {
            int registrosAfectados = 0;

            IDbCommand comando = this.conectorDB.CrearNuevoComando("INSERT INTO Operaciones VALUES(@Id_Tarjeta, @Fecha_Op, @Cod_Op, @Monto)");
            try
            {  
                this.conectorDB.AgregarParametroAComando(comando, "@Id_Tarjeta", valor.Id_Tarjeta);
                this.conectorDB.AgregarParametroAComando(comando, "@Fecha_Op", DateTime.Now);
                this.conectorDB.AgregarParametroAComando(comando, "@Cod_Op", valor.Cod_Op);
                this.conectorDB.AgregarParametroAComando(comando, "@Monto", valor.Monto);
          
                registrosAfectados = this.conectorDB.ExecuteNonQuery(comando);
            }
            catch
            {
                throw;
            }
            finally
            {
                this.conectorDB.Close(comando);
            }

            return (registrosAfectados > 0);
        }
    }
}
