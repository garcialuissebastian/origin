using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Be
{
  public  class SessionTarjeta
    {
        public int Id_Tarjeta { get; set; }

        [Required(ErrorMessage = "Obligatorio")] 
        public Int64? Nro { get; set; }
        public string Pin { get; set; }

        public ResultadoAutenticacionByNro ResultadoAutenticacion { get; set; }

        public SessionTarjeta(int vId_Tarjeta, Int64? vNro, string vPin, ResultadoAutenticacionByNro vResultadoAutenticacion)
        {
            Id_Tarjeta = vId_Tarjeta;
            Nro = vNro;
            Pin = vPin;
            ResultadoAutenticacion = vResultadoAutenticacion;
        }
        public SessionTarjeta()
        {

        }
    }
}
