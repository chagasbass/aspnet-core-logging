namespace EffectiveLogging.Middlewares.Entities
{
    /// <summary>
    /// Objeto customizado para retorno de erros nas aplicações
    /// </summary>
    public  class ProblemDetail
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public int CodigoHttp { get; set; }
        public string Detalhe { get; set; }
        public string Tipo { get; set; }
        public string Instancia { get; set; }
        public string Link { get; set; }
        public string TipoDeDado { get; set; }

        public ProblemDetail()
        {
            TipoDeDado = @"application/problem+json";
        }
    }
}
