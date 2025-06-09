using MinhaAPI.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Produto> produtos = new();
List<Categoria> categorias = new();


app.MapGet("/produtos", () =>
{
    var listaComCategoria = produtos.Select(p =>
    {
        
        var categoria = categorias.FirstOrDefault(c => c.Id == p.CategoriaId);
        return new
        {
            p.Id,
            p.Nome,
            p.Preco,
            Categoria = categoria // aqui retorna o objeto completo
        };
    });

    return Results.Ok(listaComCategoria);
});


app.Run();








