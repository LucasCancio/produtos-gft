namespace ProdutosGFT.Server.v1.DTOs
{
    public class InternalErrorDTO
    {
        public InternalErrorDTO(string message)
        {
            StatusCode = 500;
            Message = message;
            IsValid = false;
        }
        /// <example>500</example>
        public int StatusCode { get; private set; }
        /// <example>false</example>
        public bool IsValid { get; private set; }
        /// <example>Ocorreu um erro inesperado! ...</example>
        public string Message { get; private set; }
    }
}