using System;
using System.Collections.Generic;
using System.Text;
using Be;
namespace Bll
{
   public class BllOperacion 
    { 
        private Dal.DalOperacion _mapeador;

        public BllOperacion()
        {
            _mapeador = new Dal.DalOperacion();
        }

        private static BllOperacion instancia = null;

        public static BllOperacion DameInstancia()
        {
            if (instancia == null)
            {
                return new BllOperacion();
            }
            else
            {
                return instancia;
            }
        }

        public bool Agregar(Be.Operaciones valor)
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



    }
}
