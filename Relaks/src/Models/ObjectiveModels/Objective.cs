// namespace Relaks.Models.ObjectiveModels;
//
// public class Objective
// {
//     public enum CycleEnum
//     {
//         Daily,
//         Weekly,
//         
//         /// <summary>
//         /// 1-28
//         /// </summary>
//         Monthly,
//         
//         EveryHalfYear,
//         
//         FirstDayOfMonth,
//         LastDayOfMonth,
//         
//         Quarterly,
//         Yearly,
//     }
//
//     public enum PriorityEnum
//     {
//         Supreme,
//         High,
//         Normal,
//         Low
//     }
//     
//     public Guid Id { get; set; }
//     public string Title { get; set; } = null!;
//     public string Description { get; set; } = null!;
//     public bool? IsSuccess { get; set; }
//
//     public CycleEnum? Cycle { get; set; }
//     public DateTime? CycleDate { get; set; }
//     
//     public DateTime CreatedAt { get; set; }
//     public DateTime StartAt { get; set; }
//     public DateTime? EndAt { get; set; }
//
//     /// <summary>
//     /// Исполнители
//     /// </summary>
//     public List<BaseEntry> Performers { get; set; } = new();
//     
//
//     /// <summary>
//     /// Владельцы задачи (указывать единожды)
//     /// </summary>
//     public List<BaseEntry> Owners { get; set; } = new();
//     
//     public PriorityEnum Priority { get; set; }
// }