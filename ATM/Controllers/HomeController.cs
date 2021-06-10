using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ATM.Models;
using Be;
using Bll;
using Newtonsoft.Json;

namespace ATM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IAutenticador _Autenticador;
        public HomeController(ILogger<HomeController> logger,IAutenticador autenticador)
        {
            _Autenticador = autenticador;
               _logger = logger;
        }

        public IActionResult Index()
        {
           
            return View();
        }

        [Route("Home/RetiroConfirmado")]
        public IActionResult  RetiroConfirmado()
        {
           Tarjetas Retiro = new Tarjetas();
            try
            {
                var tj = SessionHelper.GetObjectFromJson<Be.SessionTarjeta>(HttpContext.Session, "SessionWeb");
                if (tj == null)
                {
                    return RedirectToAction("Index");
                }

                Retiro = BllTarjetas.DameInstancia().ReporteUltimoRetiro(tj.Id_Tarjeta);
                return View(Retiro);
            }
            catch (Exception)
            {

                throw;
            }

           
        }

        public IActionResult ErrorRetiro()
        {
            var tj = SessionHelper.GetObjectFromJson<Be.SessionTarjeta>(HttpContext.Session, "SessionWeb");
            if (tj == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Retiro()
        {
            var tj = SessionHelper.GetObjectFromJson<Be.SessionTarjeta>(HttpContext.Session, "SessionWeb");
            if (tj == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Retiro(string Monto)
        {
            var tj = SessionHelper.GetObjectFromJson<Be.SessionTarjeta>(HttpContext.Session, "SessionWeb");
            if (tj == null)
            {
                return RedirectToAction("Index");
            }

            if (Monto != "" && Convert.ToDouble( Monto) >0)
            {
               if(  Bll.BllTarjetas.DameInstancia().Retiro(tj.Id_Tarjeta, Monto))
                {
                    return RedirectToAction("RetiroConfirmado");
                }
                else
                {
                    return RedirectToAction("ErrorRetiro");
                }
            }
            return View();
        }
        public IActionResult Balance()
        {
            var tj = SessionHelper.GetObjectFromJson<Be.SessionTarjeta>(HttpContext.Session, "SessionWeb");
            if (tj == null)
            {
                return RedirectToAction("Index");
            }  
            BllOperacion.DameInstancia().Agregar(new Operaciones(tj.Id_Tarjeta,1,"0")); // registra la operacion

            return View(BllTarjetas.DameInstancia().ConsultarUno(new Tarjetas(tj.Nro, tj.Pin))); // consulto nuevamente por si actualizo el monto
        }

        public IActionResult Salir()
        {      
            SessionHelper.SetObjectAsJson(HttpContext.Session, "SessionWeb",null);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Index(Be.SessionTarjeta v_aut)
        {
            try
            {
                 
              ResultadoAutenticacionByNro validado =  _Autenticador.iniciarByNro(v_aut);
              
                if (validado == ResultadoAutenticacionByNro.UsuarioBloqueado || validado ==  ResultadoAutenticacionByNro.UsuarioInvalido)
                {
                     TempData["validado"] = validado; 
                    return RedirectToAction("ErroAutentificacionNro" );  
                }
                else if (validado == ResultadoAutenticacionByNro.UsuarioValido)
                {
                    TempData["Aut"] = JsonConvert.SerializeObject (v_aut);
                 
                    return RedirectToAction("AutentificacionPin");

                } 
            }
            catch (Exception)
            {

                throw;
            }
          
            return View();
        }

        public IActionResult AutentificacionPin()
        {   
            var Aud = new SessionTarjeta();
            try
            {                
                if (TempData["Aut"] != null)
                {
                    Aud = JsonConvert.DeserializeObject<SessionTarjeta>(TempData["Aut"].ToString());
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                throw;
            }
            if (Aud.Nro<1)
            {
                return RedirectToAction("Index");
            }
            return View(Aud);
        }


        [HttpPost]
        public IActionResult AutentificacionPin(Be.SessionTarjeta v_aut)
        {
            try
            {
                SessionTarjeta validado = _Autenticador.iniciarByPin(v_aut);

                if (validado.ResultadoAutenticacion !=  Be.ResultadoAutenticacionByNro.UsuarioValido)
                {
                    TempData["validado"] = validado.ResultadoAutenticacion;
                    return RedirectToAction("ErroAutentificacionNro");
                }
                else if (validado.ResultadoAutenticacion == Be.ResultadoAutenticacionByNro.UsuarioValido)
                { 
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "SessionWeb", validado);
                    return RedirectToAction("Operaciones");

                }

                 
            }
            catch (Exception)
            {

                throw;
            }

            return View();
        }

        public IActionResult ErroAutentificacionNro()
        {
            try
            {
              
                ViewBag.MyMessage = (ResultadoAutenticacionByNro)TempData["validado"];
            }
            catch (Exception)
            {

                return RedirectToAction("Index");
          
            }
            
            return View();
        }
      

        public IActionResult Operaciones()
        {
        var tj =    SessionHelper.GetObjectFromJson<Be.SessionTarjeta>(HttpContext.Session, "SessionWeb");
            if (tj==null)
            {
                return RedirectToAction("Index");
            }
           
            return View();
        }

       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
