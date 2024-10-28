using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Use_Cases.Commands
{
    public class CreateToDoItemCommandValidator : AbstractValidator<CreateToDoItemCommand>
    {
        public CreateToDoItemCommandValidator()
        {
            RuleFor(x => x.IsDone).NotEmpty();
            RuleFor(x => x.Description).NotEmpty().MaximumLength(200);
        }
    }
}
