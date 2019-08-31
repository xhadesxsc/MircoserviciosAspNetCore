using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RAEM.ApiSQL.Model
{
    public class Compra
    {
        [Key]
        public int IdCompra { get; set; }
        public string NombreProveedor { get; set; }
        public double Monto { get; set; }
    }
}
