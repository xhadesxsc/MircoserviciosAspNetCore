using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RAEM.ApiSQL.Contexto;
using RAEM.ApiSQL.Model;

namespace RAEM.ApiSQL.Services
{
    public class CompraServices : ICompraServices
    {
        private readonly DbVentasContext context;

        public CompraServices(DbVentasContext context)
        {
            this.context = context;
        }

        public Compra Insertar(Compra pedido)
        {
            context.Compra.Add(pedido);
            context.SaveChanges();
            return pedido;
        }

        public List<Compra> Listar()
        {
            return context.Compra.ToList();
        }

        public Compra Recuperar(int IdPedido)
        {
            //return context.Pedido.Where(t => t.IdPedido == IdPedido).FirstOrDefault();

            return context.Compra.FirstOrDefault(t => t.IdCompra== IdPedido);
        }
    }
}
