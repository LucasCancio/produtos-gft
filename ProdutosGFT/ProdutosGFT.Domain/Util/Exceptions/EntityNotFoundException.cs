using System;

namespace ProdutosGFT.Domain.Util.Exceptions
{
    public class EntityNotFoundException: Exception
    {
        public EntityNotFoundException(string msg = "Entidade não encontrada!") : base(msg)
        { }

        public EntityNotFoundException(string msg, Exception inner) : base(msg, inner)
        { }
    }
}