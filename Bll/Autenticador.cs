using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Be;
using Dal;
using Microsoft.AspNetCore.Http;

namespace Bll
{
    public class Autenticador : IAutenticador
    {
        private Dal.DalTarjetas _mapeador;

        public Autenticador()
        {
            _mapeador = new DalTarjetas();
        }


        public ResultadoAutenticacionByNro iniciarByNro(Be.SessionTarjeta v_tj)
        {
            var valido = ResultadoAutenticacionByNro.UsuarioInvalido;
            try
            {

                var tj = _mapeador.ConsultarUno(new Tarjetas(v_tj.Nro, v_tj.Pin));

                if (tj != null)
                {
                    valido = ResultadoAutenticacionByNro.UsuarioValido;

                    if (tj.Bloqueo == "S")
                    {
                        valido = ResultadoAutenticacionByNro.UsuarioBloqueado;
                    }
                }


                return valido;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public SessionTarjeta iniciarByPin(Be.SessionTarjeta v_tj)
        {
            var valido = ResultadoAutenticacionByNro.UsuarioInvalido;
            try
            {
                v_tj.Pin = Encriptor.DameInstancia().GetMD5(v_tj.Pin.ToString());

                var tj = _mapeador.ConsultarUno(new Tarjetas(v_tj.Nro, v_tj.Pin));

                if (tj != null)
                {
                    if (tj.Bloqueo == "S")
                    {
                        valido = ResultadoAutenticacionByNro.UsuarioBloqueado;
                    }
                    else // si ingreso y no esta bloquedo
                    {
                        BllTarjetas.DameInstancia().ActualizarIntentos(v_tj.Nro, 1);
                        valido = ResultadoAutenticacionByNro.UsuarioValido;
                    }
                }
                else
                {
                    tj = new Tarjetas();
                    if (BllTarjetas.DameInstancia().ActualizarIntentos(v_tj.Nro, 0))//Se blooqueó la tj
                    {
                        valido = ResultadoAutenticacionByNro.tjBloqueada;

                    }
                }
                 
                return new SessionTarjeta(tj.Id_Tarjeta, v_tj.Nro, v_tj.Pin, valido);
            }
            catch (Exception)
            {

                throw;
            }


        }


    }
}
