using FluentValidation;
using TaskManager.DTO;

namespace TaskManager.Validators;

public class TaskDtoValidator : AbstractValidator<TaskDto>
{
    public TaskDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Status).Must(status =>
            new[] { "Pending", "InProgress", "Completed" }.Contains(status));
    }
}
