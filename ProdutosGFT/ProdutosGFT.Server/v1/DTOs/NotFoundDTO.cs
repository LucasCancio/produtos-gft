namespace ProdutosGFT.Server.v1.DTOs
{
    public class NotFoundDTO
    {
        public NotFoundDTO(string message)
        {
            StatusCode = 404;
            Message = message;
            IsValid = false;
        }
        /// <example>404</example>
        public int StatusCode { get; private set; }
        /// <example>false</example>
        public bool IsValid { get; private set; }
        /// <example>Registro não encontrado.</example>
        public string Message { get; private set; }
    }
}