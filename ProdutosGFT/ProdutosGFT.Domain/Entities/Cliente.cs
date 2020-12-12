using System;
using ProdutosGFT.Domain.Util.Enums;

namespace ProdutosGFT.Domain.Entities
{
    public class Cliente : DefaultEntity
    {
        public Cliente(long id, string nome, string email, string senha, string documento, Role role)
        : base(id)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Documento = documento;
            Role = role;
        }

        public Cliente(string nome, string email, string senha, string documento, Role role)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Documento = documento;
            Role = role;
        }

        /// <example>Claudio Luis</example>
        public string Nome { get; set; }

        /// <example>claudio@gmail.com</example>
        public string Email { get; set; }

        /// <example>claudio12345</example>
        public string Senha { get; set; }

        /// <summary>
        /// CPF do cliente
        /// </summary>
        /// <example>999.999.999-99</example>
        public string Documento { get; set; }

        /// <summary>
        ///  Nível de acesso do cliente
        /// </summary>
        /// <example>USER</example>
        public Role Role { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            var cliente = (Cliente)obj;
            return cliente.Id == this.Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}