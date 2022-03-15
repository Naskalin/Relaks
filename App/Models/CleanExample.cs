// using System.ComponentModel.DataAnnotations;
// using JsonApiDotNetCore.Resources;
// using JsonApiDotNetCore.Resources.Annotations;
//
// namespace App.Models;
//
// public class BaseBook : Identifiable<Guid>
// {
//     [Attr]
//     public string Title { get; set; } = null!;
// }
//
// [Resource(PublicName = "books")]
// public class Book : BaseBook
// {
//     [HasOne]
//     public Author Author { get; set; } = null!;
// }
//
// [Resource(PublicName = "authors")]
// public class Author : Identifiable<Guid>
// {
//     [Attr]
//     public string Name { get; set; } = null!;
//
//     [HasMany]
//     public List<Book> Books { get; set; } = new();
// }