using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace ProdutosGFT.Domain.Util.Exceptions
{
    public class InvalidEntityException : Exception
    {
        public List<(string Key, string Value)> ErrorFields { get; set; } = new List<(string Key, string Value)>();
        public InvalidEntityException(IList<ValidationFailure> errors)
        {
            foreach (ValidationFailure error in errors)
            {
                ErrorFields.Add((error.PropertyName, error.ErrorMessage));
            }
        }

        public InvalidEntityException(string msg, string field) : base(msg)
        {
            this.ErrorFields.Add((field.Replace("'", ""), msg));
        }

        public InvalidEntityException(string msg, string field, Exception inner) : base(msg, inner)
        {
            this.ErrorFields.Add((field.Replace("'", ""), msg));
        }
    }
}