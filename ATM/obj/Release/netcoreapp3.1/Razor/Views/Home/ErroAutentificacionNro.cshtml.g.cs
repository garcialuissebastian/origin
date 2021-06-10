#pragma checksum "C:\Users\user\Documents\Cursos\ATM\ATM\ATM\Views\Home\ErroAutentificacionNro.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1369c0329bbafc20149d8b20ea4c1e1855393056"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_ErroAutentificacionNro), @"mvc.1.0.view", @"/Views/Home/ErroAutentificacionNro.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\user\Documents\Cursos\ATM\ATM\ATM\Views\_ViewImports.cshtml"
using ATM;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\user\Documents\Cursos\ATM\ATM\ATM\Views\_ViewImports.cshtml"
using ATM.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1369c0329bbafc20149d8b20ea4c1e1855393056", @"/Views/Home/ErroAutentificacionNro.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a9dc7277f1bba5b0e524d30552cf672246cb4e83", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_ErroAutentificacionNro : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\user\Documents\Cursos\ATM\ATM\ATM\Views\Home\ErroAutentificacionNro.cshtml"
  
    ViewData["Title"] = "ErroAutentificacionNro";
    Layout = "~/Views/Shared/_MasterAtm.cshtml";

    var tipoError = (Be.ResultadoAutenticacionByNro)ViewBag.MyMessage;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<div class=""text-center"">
    <h1 class=""display-4"">ATM - Cajero Atomático</h1>



</div>
<br/>
<h2>Se Produjo un error en la autentificación</h2>

<div class=""container"">
    <br />


    <div class=""row"">
        <div class=""alert alert-danger col-12"">
");
#nullable restore
#line 25 "C:\Users\user\Documents\Cursos\ATM\ATM\ATM\Views\Home\ErroAutentificacionNro.cshtml"
              
                if (tipoError == Be.ResultadoAutenticacionByNro.UsuarioBloqueado)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <h4> La tarjeta encuentra bloqueado.</h4>\r\n");
#nullable restore
#line 29 "C:\Users\user\Documents\Cursos\ATM\ATM\ATM\Views\Home\ErroAutentificacionNro.cshtml"
                }

                if (tipoError == Be.ResultadoAutenticacionByNro.UsuarioInvalido)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <h4> Los datos ingresados son incorrectos.</h4>\r\n");
#nullable restore
#line 34 "C:\Users\user\Documents\Cursos\ATM\ATM\ATM\Views\Home\ErroAutentificacionNro.cshtml"
                }

                if (tipoError == Be.ResultadoAutenticacionByNro.tjBloqueada)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <h4>Su tarjeta ha sido bloqueada porque por múltiples intentos.</h4>\r\n");
#nullable restore
#line 39 "C:\Users\user\Documents\Cursos\ATM\ATM\ATM\Views\Home\ErroAutentificacionNro.cshtml"
                }
            

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        </div>
    </div>


    <div class=""row"">

        <button onclick=""Volver()"" class=""btn btn-info"">Volver</button>
    </div>
    <script>
        function Volver() {
            window.location.href=""/""
        }
    </script>
</div>
");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
