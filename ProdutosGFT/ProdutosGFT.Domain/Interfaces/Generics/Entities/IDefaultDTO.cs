using System.Collections.Generic;
using FluentValidation.Results;

namespace ProdutosGFT.Domain.Interfaces.Generics.Entities
{
    public interface IDefaultDTO<T> where T : class
    {
        T ToModel(long id = 0);
        IList<ValidationFailure> Errors { get; set; }
        bool IsValid();
    }
}