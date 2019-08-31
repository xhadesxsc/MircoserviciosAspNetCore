using RAEM.ApiSQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RAEM.ApiSQL.Services
{
    public interface ICompraServices
    {
        List<Compra> Listar();
        Compra Recuperar(int IdArticulo);
        Compra Insertar(Compra pedido);
    }
}
