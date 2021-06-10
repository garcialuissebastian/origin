using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Be;
namespace Dal
{
  public  class DalTipo_Operacion : IMapeadorCRUD<Tipo_Operacion, Tipo_Operacion>
    {
        private IConectarDB conectorDB;

     public DalTipo_Operacion()
        {
            conectorDB = FactoriaConectorDB.ObtenerConector("SqlServer");
        }

        public bool Agregar(Tipo_Operacion valor)
        {
            int registrosAfectados = 0;

            IDbCommand comando = this.conectorDB.CrearNuevoComando("INSERT INTO Tipo_Operacion VALUES(@Descripcion, @Aud)");
            try
            {
                this.conectorDB.AgregarParametroAComando(comando, "@Descripcion", valor.Descripcion);
                this.conectorDB.AgregarParametroAComando(comando, "@nombre", "GETDATE()"); 

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

        public Tipo_Operacion ConsultarUno(Tipo_Operacion filtro)
        {
            List<Tipo_Operacion> lista = this.Listar(filtro);
            if (lista.Count > 0)
                return lista[0];
            else
                return null;
        }

        public bool Eliminar(Tipo_Operacion valor)
        {
            int registrosAfectados = 0;
            IDbCommand comando = this.conectorDB.CrearNuevoComando("delete from  Tipo_Operacion WHERE  Id_op=@Id_op;");
            try
            {
                this.conectorDB.AgregarParametroAComando(comando, "@Id_op", valor.Id_op);

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

        public List<Tipo_Operacion> Listar(Tipo_Operacion filtro)
        {
            List<Tipo_Operacion> lista = new List<Tipo_Operacion>();

            string cmdTxt = "SELECT Id_op, Descripcion ";
            cmdTxt += "FROM Tipo_Operacion ";
            cmdTxt += "WHERE (Id_op = @Id_op OR @Id_op IS NULL) ";
            cmdTxt += "AND (Descripcion LIKE '%' + @Descripcion + '%' OR @Descripcion IS NULL) "; 
            cmdTxt += "ORDER BY  Descripcio ASC;";
            IDbCommand comando = this.conectorDB.CrearNuevoComando(cmdTxt);
            try
            {
                if (filtro != null && filtro.Id_op > 0)
                    this.conectorDB.AgregarParametroAComando(comando, "@Id_op", filtro.Id_op);
                else
                    this.conectorDB.AgregarParametroAComando(comando, "@Id_op", DBNull.Value);

                if (filtro != null && !string.IsNullOrEmpty(filtro.Descripcion))
                    this.conectorDB.AgregarParametroAComando(comando, "@Descripcion", filtro.Descripcion);
                else
                    this.conectorDB.AgregarParametroAComando(comando, "@Descripcion", DBNull.Value);
                
                
                IDataReader lector = this.conectorDB.ExecuteReader(comando);
                while (lector.Read())
                {
                    
                    Tipo_Operacion entidad = new Tipo_Operacion();
                    entidad.Id_op= Convert.ToInt32(lector["Id_op"]);
                    entidad.Descripcion = Convert.ToString(lector["Descripcion"]);
                    
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

        public bool Modificar(Tipo_Operacion valor)
        {
            int registrosAfectados = 0;
            IDbCommand comando = this.conectorDB.CrearNuevoComando("update Tipo_Operacion set Descripcion=@Descripcion,Aud=@Aud  WHERE  Id_op=@Id_op;");
            try
            {
                this.conectorDB.AgregarParametroAComando(comando, "@Id_op", valor.Id_op);
                this.conectorDB.AgregarParametroAComando(comando, "@Descripcion", valor.Descripcion);
                this.conectorDB.AgregarParametroAComando(comando, "@Aud", "GETDATE()");
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




















































