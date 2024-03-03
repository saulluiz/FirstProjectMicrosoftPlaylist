
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var todos = new List<Todo>();
app.MapDelete("/todos/{id}", (int id) =>
{
    int index = todos.FindIndex((x) => x.Id == id);
    if (index != -1)
    {
        todos.Remove(todos[index]);
        return Results.Ok("Index Removido");

    }
    else return Results.NotFound("Index Não Encontrado");

});
app.MapGet("/todos", () => todos);
app.MapGet("/todos/{id}", (int id) =>
{

    var response = todos.Find((x) => x.Id == id);
    //if response==null retornar not found error
    if (response is null)
    {
        return Results.NotFound("Id não encontrado");
    }
    return Results.Ok(response);
}

);



app.MapPost("/todos", (Todo task) =>
{
    todos.Add(task);
    return TypedResults.Created("/todos/{id}", task);


});
app.MapPut("/todos/{id}", (int id, Todo Update) =>
{
    var index = todos.FindIndex((x) => x.Id == id);
    if (index == -1)
    {
        return Results.NotFound("Id não existe");
    }
    else
    {
        todos[index] = Update;
        return Results.Ok($"Componente de id {id} atualizado");
    }


});
app.Run();
public record Todo(int Id, string Name, DateTime DueDate, bool IsCompleted);