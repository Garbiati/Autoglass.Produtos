namespace AutoglassAPI.Application.DTOs
{
    public class FornecedorListaDTO
    {
        public int Total  {get;set;} 
        public int Exibindo  {get;set;}
        public int Pagina  {get;set;} 
        public int TamanhoPagina  {get;set;} 
        public IEnumerable<FornecedorDTO>? Fornecedores { get; set; }
    }
}