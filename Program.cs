using System;
using System.Linq;
using EFGetStarted;
using Microsoft.EntityFrameworkCore;
using Task = EFGetStarted.Task;

using var db = new BloggingContext();

// Note: This sample requires the database to be created before running.
Console.WriteLine($"Database path: {db.DbPath}.");

// Create
Console.WriteLine("Inserting a new blog");
db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
await db.SaveChangesAsync();

// Read
Console.WriteLine("Querying for a blog");
var blog = await db.Blogs
    .OrderBy(b => b.BlogId)
    .FirstAsync();

// Update
Console.WriteLine("Updating the blog and adding a post");
blog.Url = "https://devblogs.microsoft.com/dotnet";
blog.Posts.Add(
    new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
await db.SaveChangesAsync();

// Delete
Console.WriteLine("Delete the blog");
db.Remove(blog);
await db.SaveChangesAsync();
printincompleteTasksAndTodos();

// using (BloggingContext context = new())
// {
//     var tasks = context.Tasks.Include(task => task.Todos);
//     foreach (var task in tasks)
//     {
//         Console.WriteLine($"Task: {task.Name}");
//         foreach (var todo in task.Todos)
//         {
//             Console.WriteLine($"- {todo.Name}");
//         }
//     }
// }

static void printincompleteTasksAndTodos()
{
    var context = new BloggingContext();
    var tasks = context.Tasks.Include(task => task.Todos);
    foreach (var task in tasks)
    {
        var incompleteTodos = task.Todos.Where(todo => !todo.IsComplete).ToList();
        if (incompleteTodos.Any())
        {
            Console.WriteLine($"Task: {task.Name}");
            foreach (var todo in incompleteTodos)
            {
                Console.WriteLine($"- {todo.Name}");
            }
        }
    }
}

// SeedTasks();
// static void SeedTasks()
// {
//     using var db = new BloggingContext();
//     Task task = new()
//     { 
//         Name = "Produce softwere", Todos = [
//          new Todo(){Name = "Write code",},
//          new Todo(){Name = "Complie source"},
//          new Todo(){Name = "test Program"}
//         ]
//     };
//     db.Tasks.Add(task);
//     task = new()
//     {
//         Name = "Brew coffee", Todos = [
//         new Todo(){Name = "Pour water"},
//         new Todo(){Name = "Pour coffee"},
//         new Todo(){Name = "Turn on"}
//         ]
//     };
//     db.Tasks.Add(task);
//     db.SaveChanges();
// }

