using System;
using System.Text.Json.Serialization;

namespace ProdutosGFT.Domain.Entities
{
    public class Venda : DefaultEntity
    {
        public Venda(long clienteId, double totalCompra, DateTime dataCompra)
        {
            ClienteId = clienteId;
            TotalCompra = totalCompra;
            DataCompra = dataCompra;
        }

        public Venda(Cliente cliente, double totalCompra, DateTime dataCompra)
        {
            Cliente = cliente;
            TotalCompra = totalCompra;
            DataCompra = dataCompra;
        }

        public Venda(Cliente cliente, double totalCompra, DateTime dataCompra,
        long id)
        : base(id)
        {
            Cliente = cliente;
            TotalCompra = totalCompra;
            DataCompra = dataCompra;
        }


        public Venda(long clienteId, double totalCompra, DateTime dataCompra,
        long id)
        : base(id)
        {
            ClienteId = clienteId;
            TotalCompra = totalCompra;
            DataCompra = dataCompra;
        }

        [JsonIgnore]
        public Cliente Cliente { get; set; }
        public long ClienteId { get; set; }
        public double TotalCompra { get; set; }
        public DateTime DataCompra { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            var venda = (Venda)obj;
            return venda.Id == this.Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}