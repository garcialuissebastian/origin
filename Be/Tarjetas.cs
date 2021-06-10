using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Be
{
  public  class Tarjetas
    {
    
        
       
        public int Id_Tarjeta { get; set; }

        [Required(ErrorMessage = "Obligatorio")]

        [Display(Name = "Tarjeta Número")]
        public Int64 ?Nro  { get; set; }


        [Display(Name = "Fecha Vencimiento")]
        public string Fecha_Vto { get; set; }
        [Display(Name = "Monto disponible $")]
        public  string Balance { get; set; }
        public  string Bloqueo { get; set; }
        public int Intentos { get; set; }
        public string Pin { get; set; }
        public List<Operaciones> ListOperaciones { get; set; } = new List<Operaciones>();


        public Tarjetas(  Int64? vNro, string vPin)
        {
            Nro = vNro;
            Pin = vPin;
        }
        public Tarjetas()
        {
             
        }
    }
}
