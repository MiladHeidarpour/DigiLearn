using CommentModule.Domain;
using Common.Query.Filter;

namespace CommentModule.Services.Dtos;

public class CreateCommentCommand
{
    public string Text { get; set; }
    public Guid? ParentId { get; set; } = null;
    public Guid UserId { get; set; }
    public Guid EntityId { get; set; }
    public CommentType CommentType { get; set; }
}
