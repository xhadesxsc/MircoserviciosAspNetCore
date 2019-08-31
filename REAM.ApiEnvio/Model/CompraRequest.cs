using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REAM.ApiEnvio.Model
{
    public class CompraRequest
    {
        public int IdCompra { get; set; }
        public string NombreProveedor { get; set; }
        public double Monto { get; set; }
    }
}
