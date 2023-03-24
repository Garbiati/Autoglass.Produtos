namespace AutoglassAPI.Application.DTOs
{
    public class FiltroProdutosDTO
    {
            public int? Codigo { get; set; }
            public string? Descricao { get; set; }
            public string? Situacao { get; set; }
            public DateTime? DataFabricacao { get; set; }
            public DateTime? DataValidade { get; set; }
            public int? CodigoFornecedor { get; set; }
            public string? DescricaoFornecedor { get; set; }
            public string? CNPJFornecedor { get; set; }
            public int? Pagina { get; set; } = 1;
            public int? TamanhoPagina { get; set; } = 10;
    }
}