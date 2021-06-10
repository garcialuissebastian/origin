using System;
using System.Collections.Generic;
using System.Text;
using Be;
using Dal;
namespace Bll
{
    public class BllTarjetas : IMapeadorCRUD<Tarjetas, Tarjetas>
    {
        private Dal.DalTarjetas _mapeador;

        public BllTarjetas()
        {
            _mapeador = new DalTarjetas();
        }

        private static BllTarjetas instancia = null;

        public static BllTarjetas DameInstancia()
        {
            if (instancia == null)
            {
                return new BllTarjetas();
            }
            else
            {
                return instancia;
            }
        }

     
    public bool ActualizarIntentos(Int64? v_nro, int v_tipo)
        {
            try
            {
                return this._mapeador.ActualizarIntentos( v_nro,  v_tipo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Retiro(Int32 v_tj, string v_monto)
        {
            try
            {
                return this._mapeador.Retiro( v_tj, v_monto);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<Tarjetas> Listar(Tarjetas filtro)
        {
            try
            {
                return this._mapeador.Listar(filtro);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Tarjetas ReporteUltimoRetiro(Int32 v_tj)
        {
            try
            {
                return this._mapeador.ReporteUltimoRetiro(  v_tj);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Tarjetas ConsultarUno(Tarjetas filtro)
        {
            return this._mapeador.ConsultarUno(filtro);
        }

        public bool Eliminar(Tarjetas valor)
        {
            throw new NotImplementedException();
        }
        public bool Agregar(Tarjetas valor)
        {
            throw new NotImplementedException();

        }

        public bool Modificar(Tarjetas valor)
        {
            throw new NotImplementedException();
        }
    }
}
