using System;
using System.Collections.Generic;
using System.Text;
using Be;
using Dal;
namespace Bll
{
  public  class BllTipo_Operacion : IMapeadorCRUD<Tipo_Operacion, Tipo_Operacion>
    {

        private  Dal.DalTipo_Operacion   _mapeador;

        public BllTipo_Operacion()
        {
            _mapeador = new  DalTipo_Operacion();
        }

        private static BllTipo_Operacion instancia = null;

        public static BllTipo_Operacion DameInstancia()
        {
            if (instancia == null)
            {
                return new BllTipo_Operacion();
            }
            else
            {
                return instancia;
            }
        }

        public bool Agregar(Tipo_Operacion valor)
        {
            try
            {
                
                return this._mapeador.Agregar(valor);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Modificar(Tipo_Operacion valor)
        {
            try
            {
                return this._mapeador.Modificar(valor);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Eliminar(Tipo_Operacion valor)
        {
            try
            {
                return this._mapeador.Eliminar(valor);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Tipo_Operacion> Listar(Tipo_Operacion filtro)
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

        public Tipo_Operacion ConsultarUno(Tipo_Operacion filtro)
        {
            try
            {
                return this._mapeador.ConsultarUno(filtro);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
