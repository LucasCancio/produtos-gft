using ProdutosGFT.Domain.Util.Enums;

namespace ProdutosGFT.Server.v1.DTOs.ClienteDTOs
{
    public class TokenDTO
    {
        /// <example>Claudio Luis</example>
        public string Nome { get; set; }
        /// <example>claudio@gmail.com</example>
        public string Email { get; set; }
        public string Token { get; set; }
        /// <example>USER</example>
        public Role Role { get; set; }
    }
}