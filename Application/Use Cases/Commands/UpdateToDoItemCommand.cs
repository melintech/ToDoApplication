using MediatR;

namespace Application.Use_Cases.Commands
{
    public class UpdateToDoItemCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsDone { get; set; }
    }
}
