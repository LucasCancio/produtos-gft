namespace ProdutosGFT.Server.v1.Services.Hateoas
{
    public class Link
    {
        public Link(string href, string rel, string method)
        {
            this.href = href;
            this.rel = rel.ToUpper().Replace(" ", "_");
            this.method = method.ToUpper();
        }

        /// <example>https://localhost:xxxx/api/Entidade/1</example>
        public string href { get; set; }
        /// <example>SELF</example>
        public string rel { get; set; }
        /// <example>GET</example>
        public string method { get; set; }

    }
}