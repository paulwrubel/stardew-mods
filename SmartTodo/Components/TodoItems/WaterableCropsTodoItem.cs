// using SmartTodo.Models;
// using StardewValley;

// namespace SmartTodo.Components.TodoItems
// {
//     /// <summary>A WaterableCropsTodoItem todo item.</summary>
//     /// <remarks>Initializes a new instance of the <see cref="WaterableCropsTodoItem"/> class.</remarks>
//     /// <param name="text">The text of the todo item.</param>
//     internal class WaterableCropsTodoItem : BaseTodoItem
//     {
//         private readonly GameLocation Location;

//         private int RemainingUnwateredCount { get; set; }

//         public WaterableCropsTodoItem(GameLocation location, bool isChecked = false, Action<ITodoItem>? addToCompletedCache = null)
//             : base("", isChecked, 19, addToCompletedCache)
//         {
//             this.Location = location;
//             this.RemainingUnwateredCount = location.getTotalUnwateredCrops();

//             this.UpdateText();
//         }

//         public override void OnUpdateTicked()
//         {
//             if (!IsChecked)
//             {
//                 var unwateredCount = this.Location.getTotalUnwateredCrops();
//                 if (unwateredCount != this.RemainingUnwateredCount)
//                 {
//                     this.RemainingUnwateredCount = unwateredCount;

//                     this.UpdateText();

//                     if (this.RemainingUnwateredCount == 0)
//                     {
//                         this.MarkCompleted();
//                     }
//                 }
//             }
//         }

//         private void UpdateText()
//         {
//             this.Text = $"Water crops ({this.Location.GetDisplayName() ?? this.Location.Name}) ({this.RemainingUnwateredCount} remaining)";
//         }
//     }
// }