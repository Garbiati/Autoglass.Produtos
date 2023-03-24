namespace AutoglassAPI.Application.DTOs
{
    public class FiltroFornecedorDTO
    {
            public int? Codigo { get; set; }
            public string? Descricao { get; set; }
            public string? CNPJ { get; set; }
            public int? Pagina { get; set; } = 1;
            public int? TamanhoPagina { get; set; } = 10;
    }
}