using System;

namespace ProdutosGFT.Domain.Interfaces.Generics.Entities
{
    public interface IDefaultEntity
    {
        long Id { get; }
        DateTime DataCadastro { get;}
        DateTime DataAlteracao { get; set;}
        bool IsAtivo { get; }

        void Activate();
        void Desactivate();
    }
}