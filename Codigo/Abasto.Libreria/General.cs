using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abasto.Libreria
{
    public static class General
    {
        public static string ReplaceAll(string valor, string quitar, string nuevo)
        {
            if (!string.IsNullOrEmpty(valor) && quitar != nuevo)
            {
                string result = valor;
                try
                {
                    valor = valor.Trim();
                    var repite = quitar.Contains(quitar);
                    while (valor.Contains(quitar))
                    {
                        valor = valor.Replace(quitar, nuevo);
                        if (repite) break;
                    }
                    return valor.Trim();
                }
                catch
                {
                    return result;
                }
            }
            else
            {
                return valor;
            }
        }
        public static string MensajeTxn(string estado = "")
        {
            string mensaje = string.Empty;
            if (estado == "C") mensaje = "Registro Actualizado Correctamente";
            else if (estado == "R") mensaje = "Registro Rechazado Correctamente";
            else if (estado == "E") mensaje = "Registro Enviado Correctamente";
            else if (estado == "A") mensaje = "Registro Aprobado Correctamente";
            else if (estado == "I") mensaje = "Registro Iniciado Correctamente";
            else if (estado == "X") mensaje = "Registro Anulado Correctamente";
            return mensaje;
        }
    }
}
