using Humanizer;
using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreTodo.Models
{
    public class TodoItem : IComparable<TodoItem>
    {
        public Guid Id { get; set; }

        public bool IsDone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage="{0} is required")]
        public string Title { get; set; } = null!;

		[Display(Name = "Due at")]
		[DataType(DataType.Date, ErrorMessage="Date only")]
        [EndDateValidator]
        public DateTimeOffset? DueAt { get; set; }

		[Display(Name = "Start Date")]
		[DataType(DataType.Date, ErrorMessage="Date only")]
        [StartDateValidator]
        public DateTimeOffset? StartDate { get; set; }

		[Display(Name = "Number of days")]
		[Range(1, 20000, ErrorMessage = "{0} is out of range")]
        public int? NumberOfDays { get; set; }

        [Range(0, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Priority { get; set; }

        public string? UserId { get; set; }

        //Functions

        public string getPriority()
        {
            return Priority == 0 ? "" : Priority.ToString();
        }

        public string getNumberOfDays()
        {
            return NumberOfDays == null ? "" : NumberOfDays.ToString();
        }

        public string getStartDate()
        {
            return StartDate == null ? "" : StartDate.Humanize();
        }

        public string getDueAt()
        {
            return DueAt == null ? "" : DueAt.Humanize();
        }

        public int CompareTo(TodoItem other)
        {
            if (other == null) return 1;
            return Priority.CompareTo(other.Priority);
        }

        // Define the is greater than operator.
        public static bool operator >(TodoItem operand1, TodoItem operand2)
        {
            return operand1.CompareTo(operand2) > 0;
        }

        // Define the is less than operator.
        public static bool operator <(TodoItem operand1, TodoItem operand2)
        {
            return operand1.CompareTo(operand2) < 0;
        }

        // Define the is greater than or equal to operator.
        public static bool operator >=(TodoItem operand1, TodoItem operand2)
        {
            return operand1.CompareTo(operand2) >= 0;
        }

        // Define the is less than or equal to operator.
        public static bool operator <=(TodoItem operand1, TodoItem operand2)
        {
            return operand1.CompareTo(operand2) <= 0;
        }
    }
}