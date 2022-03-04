// using App.Models.Entry;
//
// namespace App.Mappers;
//
// public static class EntryMapper
// {
//     public static void MapTo(this PersonDto dto, Person person)
//     {
//         person.Name = dto.Name;
//         person.Reputation = dto.Reputation ?? 5;
//         person.BirthDay = dto.BirthDay;
//     }
// }
//
// // public class EntryManager
// // {
// //     // private readonly ApplicationContext _db;
// //     //
// //     // public EntryService(ApplicationContext db)
// //     // {
// //     //     _db = db;
// //     // }
// //
// //     public Person Create(PersonDto dto)
// //     {
// //         var person = new Person()
// //         {
// //             Name = dto.Name,
// //             Reputation = dto.Reputation ?? 5,
// //             BirthDay = dto.BirthDay,
// //         };
// //
// //         return person;
// //     }
// //
// //     public Company Create(CompanyDto dto)
// //     {
// //         var company = new Company()
// //         {
// //             Name = dto.Name,
// //             Reputation = dto.Reputation ?? 5,
// //         };
// //
// //         return company;
// //     }
// //
// //     public Meet Create(MeetDto dto)
// //     {
// //         var meet = new Meet()
// //         {
// //             Name = dto.Name,
// //             Reputation = dto.Reputation ?? 5,
// //             StartAt = Convert.ToDateTime(dto.StartAt),
// //             EndAt = Convert.ToDateTime(dto.EndAt),
// //         };
// //
// //         return meet;
// //     }
// // }