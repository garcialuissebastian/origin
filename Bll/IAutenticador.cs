using Be;

namespace Bll
{
    public interface IAutenticador
    {
        ResultadoAutenticacionByNro iniciarByNro(SessionTarjeta v_tj);
        SessionTarjeta iniciarByPin(SessionTarjeta v_tj);
    }
}