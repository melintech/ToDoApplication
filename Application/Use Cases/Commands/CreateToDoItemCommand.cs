using MediatR;

namespace Application.Use_Cases.Commands
{
    public class CreateToDoItemCommand : IRequest<Guid>
    {
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsDone { get; set; }
    }
}
