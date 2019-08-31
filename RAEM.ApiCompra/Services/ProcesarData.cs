using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RAEM.ApiSQL.Contexto;
using RAEM.ApiSQL.Helper;
using RAEM.ApiSQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RAEM.ApiSQL.Services
{
    public class ProcesarData:IProcesarData
    {
        private readonly ParametroConfig options;

        public ProcesarData(IOptions<ParametroConfig> options)
        {
            this.options = options.Value;
        }
        public void Procesar(Compra payload)
        {
            var opciones = new DbContextOptionsBuilder<DbVentasContext>();
            opciones.UseSqlServer(options.CnnDbVentas);
            using (DbVentasContext contexto = new DbVentasContext(opciones.Options))
            {
                contexto.Compra.Add(payload);
                contexto.SaveChanges();

            }

            string pruebas = "";
        }
    }
}
