using Microsoft.EntityFrameworkCore;
using RAEM.ApiSQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RAEM.ApiSQL.Contexto
{
    public class DbVentasContext:DbContext
    {
        public DbVentasContext
        (DbContextOptions<DbVentasContext> options) : base(options)
        {

        }

        public DbSet<Compra> Compra { get; set; }
    }
}
