using System;
using System.Collections.Generic;

namespace ProdutosGFT.Domain.Entities
{
    public class Fornecedor : DefaultEntity
    {
        public Fornecedor(string nome, string cnpj, long id)
        : base(id)
        {
            Nome = nome;
            Cnpj = cnpj;
        }

        public Fornecedor(string nome, string cnpj)
        {
            Nome = nome;
            Cnpj = cnpj;
        }

        /// <example>Lojinha do seu Zé</example>
        public string Nome { get; set; }
        /// <example>99.999.999/9999-99</example>
        public string Cnpj { get; set; }
        public List<Produto> Produtos { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            var fornecedor = (Fornecedor)obj;
            return fornecedor.Id == this.Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}