using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace FormatterNewtonSoftJson.Controllers;

[ApiController]
[Route("[controller]")]
public class InterfaceController : ControllerBase
{
    [HttpGet(Name = "Interface")]
    public ActionResult<InterfaceAuthor> Get()
    {
        // Create an author
        var interfaceAuthor = new InterfaceAuthor
        {
            Id = 1,
            Name = "John Smith",
            Books = new List<InterfaceBook>()
        };

        // Create books and assign the author
        var book1 = new InterfaceBook
        {
            Id = 1,
            Title = "Book 1"
        };

        var book2 = new InterfaceBook
        {
            Id = 2,
            Title = "Book 2"
        };

        // Add books to the author's book list
        interfaceAuthor.Books.Add(book1);
        interfaceAuthor.Books.Add(book2);
        return Ok(interfaceAuthor);
    }
}

public class InterfaceAuthor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<InterfaceBook> Books { get; set; }
}

public class InterfaceBook
{
    public int Id { get; set; }
    public string Title { get; set; }
}
