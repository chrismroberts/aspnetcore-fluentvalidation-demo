using System;
using System.Collections.Generic;
using FluentValidation;

namespace FluentValidationDemo.Models
{
    public class TaskItem
    {
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool RemindMe { get; set; }
        public int? ReminderMinutesBeforeDue { get; set; }
        public List<string> SubItems { get; set; }
    }

    public class TaskItemValidator : AbstractValidator<TaskItem>
    {
        public TaskItemValidator()
        {
            RuleFor(t => t.Description).NotEmpty();
            RuleFor(t => t.DueDate)
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("DueDate must be in the future");

            When(t => t.RemindMe == true, () =>
            {
                RuleFor(t => t.ReminderMinutesBeforeDue)
                    .NotNull()
                    .WithMessage("ReminderMinutesBeforeDue must be set")
                    .GreaterThan(0)
                    .WithMessage("ReminderMinutesBeforeDue must be greater than 0")
                    .Must(value => value % 15 == 0)
                    .WithMessage("ReminderMinutesBeforeDue must be a multiple of 15");
            });

            RuleForEach(t => t.SubItems)
                .NotEmpty()
                .WithMessage("Values in the SubItems array cannot be empty");
                
        }
    }
}