using System.Buffers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Facade;

[ApiController]
[Route("[controller]")]
public class FacadeController : ControllerBase
{
    [HttpGet]
    public ActionResult<Author> Test(
        [FromServices] ArrayPool<char> pool,
        [FromServices] IOptions<MvcOptions> options)
    {
        // Create an author
        var author = new Author
        {
            Id = 1,
            Name = "John Smith",
            Books = new List<Book>()
        };

        // Create books and assign the author
        var book1 = new Book
        {
            Id = 1,
            Title = "Book 1",
            Author = author
        };

        var book2 = new Book
        {
            Id = 2,
            Title = "Book 2",
            Author = author
        };

        // Add books to the author's book list
        author.Books.Add(book1);
        author.Books.Add(book2);
        return Ok(author);
    }
}

public class FakeResponse
{
    public string Name { get; set; } = "Facade";
}

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Book> Books { get; set; }
}

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public Author Author { get; set; }
}
