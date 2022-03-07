// using System.ComponentModel.DataAnnotations;
//
// namespace App.Models;
//
// public class Book
// {
//     [Key]
//     public Guid Id { get; set; }
//     public string Title { get; set; } = null!;
//
//     public List<Author> Authors { get; set; } = new();
// }
//
// public class Author
// {
//     [Key]
//     public Guid Id { get; set; }
//     public string Name { get; set; } = null!;
//
//     public List<Book> Books { get; set; } = new();
// }