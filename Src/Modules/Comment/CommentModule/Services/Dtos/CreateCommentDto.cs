using System.ComponentModel.DataAnnotations;
using CommentModule.Domain;
using Common.Query.Filter;

namespace CommentModule.Services.Dtos;

public class CreateCommentCommand
{
    [Required]
    public string Text { get; set; }
    public Guid? ParentId { get; set; } = null;
    public Guid UserId { get; set; }=Guid.Empty;
    public Guid EntityId { get; set; }
    public CommentType CommentType { get; set; }
}
