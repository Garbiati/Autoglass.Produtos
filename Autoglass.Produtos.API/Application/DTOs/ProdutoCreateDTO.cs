namespace AutoglassAPI.Application.DTOs
{
    public class ProdutoCreateDTO
    {
        public string Descricao { get; set; }
        public string  DataFabricacao { get; set; }
        public string  DataValidade { get; set; }
        public int FornecedorId { get; set; }


    }
}
