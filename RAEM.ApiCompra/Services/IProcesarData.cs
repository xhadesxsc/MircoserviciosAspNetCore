using RAEM.ApiSQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RAEM.ApiSQL.Services
{
    public interface IProcesarData
    {
        void Procesar(Compra payload);
    }
}
