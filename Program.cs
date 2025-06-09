using MinhaAPI.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Produto> produtos = new();
List<Categoria> categorias = new();

int proximoIdProduto = 1;

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
// GET /produtos/{id} - produto específico com categoria
app.MapGet("/produtos/{id}", (int id) =>
{
    var produto = produtos.FirstOrDefault(p => p.Id == id);
    if (produto == null)
        return Results.NotFound("Produto não encontrado");

    var categoria = categorias.FirstOrDefault(c => c.Id == produto.CategoriaId);

    return Results.Ok(new
    {
        produto.Id,
        produto.Nome,
        produto.Preco,
        Categoria = categoria != null ? categoria.Nome : "Categoria não encontrada"
    });
});

// POST /produtos - adicionar produto
app.MapPost("/produtos", (Produto produto) =>
{
    var categoria = categorias.FirstOrDefault(c => c.Id == produto.CategoriaId);
    if (categoria == null)
        return Results.BadRequest("Categoria não encontrada");

    produto.Id = proximoIdProduto++;
    produtos.Add(produto);
    return Results.Created($"/produtos/{produto.Id}", produto);
});

// PUT /produtos/{id} - atualizar produto
app.MapPut("/produtos/{id}", (int id, Produto produtoAtualizado) =>
{
    var produto = produtos.FirstOrDefault(p => p.Id == id);
    if (produto == null)
        return Results.NotFound("Produto não encontrado");

    var categoria = categorias.FirstOrDefault(c => c.Id == produtoAtualizado.CategoriaId);
    if (categoria == null)
        return Results.BadRequest("Categoria não encontrada");

    produto.Nome = produtoAtualizado.Nome;
    produto.Preco = produtoAtualizado.Preco;
    produto.CategoriaId = produtoAtualizado.CategoriaId;

    return Results.Ok(produto);
});


app.Run();








