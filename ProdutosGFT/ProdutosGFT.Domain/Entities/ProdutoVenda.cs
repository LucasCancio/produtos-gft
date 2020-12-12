namespace ProdutosGFT.Domain.Entities
{
    public class ProdutoVenda
    {
        public ProdutoVenda(long id, long produtoId, long vendaId, int quantidade = 1)
        {
            Id = id;
            ProdutoId = produtoId;
            VendaId = vendaId;
            Quantidade = quantidade;
        }

        public ProdutoVenda(long produtoId, int quantidade = 1)
        {
            ProdutoId = produtoId;
            Quantidade = quantidade;
        }

        public ProdutoVenda(long produtoId, long vendaId, int quantidade = 1)
        {
            ProdutoId = produtoId;
            VendaId = vendaId;
            Quantidade = quantidade;
        }

        public ProdutoVenda(long produtoId, Venda venda, int quantidade = 1)
        {
            ProdutoId = produtoId;
            Venda = venda;
            Quantidade = quantidade;
        }

        public ProdutoVenda(Produto produto, Venda venda, int quantidade = 1)
        {
            Produto = produto;
            Venda = venda;
            Quantidade = quantidade;
        }

        public ProdutoVenda(Produto produto, long vendaId, int quantidade = 1)
        {
            Produto = produto;
            VendaId = vendaId;
            Quantidade = quantidade;
        }

        public ProdutoVenda(long id, Produto produto, Venda venda, int quantidade = 1)
        {
            Id = id;
            Produto = produto;
            Venda = venda;
            Quantidade = quantidade;
        }

        public long Id { get; set; }
        public Produto Produto { get; set; }
        public long ProdutoId { get; set; }
        public Venda Venda { get; set; }
        public long VendaId { get; set; }
        public int Quantidade { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            var produtoVenda = (ProdutoVenda)obj;
            return produtoVenda.ProdutoId == this.ProdutoId && produtoVenda.VendaId == this.VendaId;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}