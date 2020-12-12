using System;
using System.Text.Json.Serialization;
using ProdutosGFT.Domain.Util.Exceptions;

namespace ProdutosGFT.Domain.Entities
{
    public class Produto : DefaultEntity
    {
        public Produto(string nome, double valor,
         bool promocao, double valorPromo, string categoria,
         string imagem, long quantidade, long fornecedorId)
        {
            Nome = nome;
            CodigoProduto = Guid.NewGuid().ToString().Replace("-","");
            Valor = valor;
            Promocao = promocao;
            ValorPromo = valorPromo;
            Categoria = categoria;
            Imagem = imagem;
            Quantidade = quantidade;
            FornecedorId = fornecedorId;
        }

        public Produto(string nome, double valor,
         bool promocao, double valorPromo, string categoria,
         string imagem, long quantidade, long fornecedorId,
         long id)
         : base(id)
        {
            Nome = nome;
            CodigoProduto = Guid.NewGuid().ToString().Replace("-","");
            Valor = valor;
            Promocao = promocao;
            ValorPromo = valorPromo;
            Categoria = categoria;
            Imagem = imagem;
            Quantidade = quantidade;
            FornecedorId = fornecedorId;
        }

        /// <example>Geladeira</example>
        public string Nome { get; set; }
        
        /// <summary>
        /// Código de barra
        /// </summary>
        /// <example>12345678123412341234123456789abc</example>
        public string CodigoProduto { get; set; }

        /// <example>1000</example>
        public double Valor { get; set; }

        /// <example>true</example>
        public bool Promocao { get; set; }

        /// <example>500</example>
        public double ValorPromo { get; set; }

        /// <example>Eletrodomésticos</example>
        public string Categoria { get; set; }
        
        /// <summary>
        /// Nome da imagem do produto
        /// </summary>
        /// <example>geladeira.png</example>
        public string Imagem { get; set; }

        /// <example>10</example>
        public long Quantidade { get; set; }

        [JsonIgnore]
        public Fornecedor Fornecedor { get; set; }
        
        public long FornecedorId { get; set; }

        public bool HasStock(long quantidade = 1)
        {
            return this.Quantidade - quantidade >= 0;
        }

        public void ChangeStock(long quantidade = 1)
        {
            if (!HasStock(quantidade)) throw new InvalidEntityException(msg: $"O produto com Id '{Id}' não possui estoque suficiente!", field: $"Produtos");

            this.Quantidade = this.Quantidade - quantidade;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            var produto = (Produto)obj;
            return produto.Id == this.Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}