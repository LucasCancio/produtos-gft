using System.Collections.Generic;

namespace ProdutosGFT.Server.v1.DTOs
{
    public class BadRequestDTO
    {
        public BadRequestDTO(List<ErrorFieldDTO> errors, int statusCode = 400)
        {
            StatusCode = statusCode;
            Errors = errors;
            IsValid = false;
        }
        /// <example>400</example>
        public int StatusCode { get; private set; }
        /// <example>false</example>
        public bool IsValid { get; private set; }
        public List<ErrorFieldDTO> Errors { get; private set; }
    }

    public class ErrorFieldDTO
    {
        /// <example>Campo</example>
        public string Field { get; set; }
        /// <example>'Campo' é obrigatório</example>
        public string Error { get; set; }
    }
}