using System;
using ProdutosGFT.Domain.Interfaces.Generics.Entities;

namespace ProdutosGFT.Domain.Entities
{
    public class DefaultEntity : IDefaultEntity
    {
        public DefaultEntity(long id, bool isAtivo = true)
        {
            Id = id;
            DataCadastro = DateTime.Now;
            DataAlteracao = DateTime.Now;
            IsAtivo = isAtivo;
        }

        public DefaultEntity()
        {
            Id = 0;
            DataCadastro = DateTime.Now;
            DataAlteracao = DateTime.Now;
            IsAtivo = true;
        }

        public long Id { get; protected set; }
        public DateTime DataCadastro { get; protected set; }
        public DateTime DataAlteracao { get; set; }
        public bool IsAtivo { get; protected set; }

        public void Activate()
        {
            this.IsAtivo = true;
            this.DataAlteracao = DateTime.Now;
        }

        public void Desactivate()
        {
            this.IsAtivo = false;
            this.DataAlteracao = DateTime.Now;
        }
    }
}