using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Be
{
  public  class Operaciones
    {
        public int Id_Operacion { get; set; }
        public int Id_Tarjeta { get; set; }
        [Display(Name = "Retiro Fecha y Hora")]
        public string Fecha_Op  { get; set; }
        public int Cod_Op { get; set; }
        [Display(Name = "Retiro Monto $")]
        public string Monto { get; set; } 

        public Operaciones(int vId_Tarjeta,  int vCod_Op, string vMonto)
        {
            Id_Tarjeta = vId_Tarjeta; 
            Cod_Op = vCod_Op;
            Monto = vMonto;
        }
        public Operaciones()
        {
            
        }
    }
}
