using System;

namespace ProdutosGFT.Server.v1.DTOs
{
    public class SuccessDTO<T> where T : class
    {
        public SuccessDTO(T result, int statusCode = 200)
        {
            StatusCode = statusCode;
            Result = result;
            IsValid = true;
        }
        /// <example>200</example>
        public int StatusCode { get; private set; }
        /// <example>true</example>
        public bool IsValid { get; private set; }
        public T Result { get; private set; }
    }
}