namespace AutoglassAPI.Domain.Entities
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string CNPJ { get; set; }
        public ICollection<Produto> Produtos { get; set; }
    }
}