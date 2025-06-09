namespace MinhaAPI.Models;



public class Produto{
	public int Id {get; set; }
	public string Nome {get; set; } = string.Empty;
	public decimal Preco {get; set; }
	 public int CategoriaId { get; set; }  // Chave estrangeira
    public Categoria? Categoria { get; set; }  // Propriedade de navegação

}
