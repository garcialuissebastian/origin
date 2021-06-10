using System;
using System.Collections.Generic;
using System.Text;

namespace Dal
{
    interface IMapeadorCRUD<T, F>
    {
        bool Agregar(T valor);
        bool Modificar(T valor);
        bool Eliminar(T valor);
        List<T> Listar(F filtro);
        T ConsultarUno(F filtro);
    }
}
