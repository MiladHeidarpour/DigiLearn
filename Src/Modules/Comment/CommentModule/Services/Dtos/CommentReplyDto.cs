using System.ComponentModel.DataAnnotations;
using CommentModule.Domain;

namespace CommentModule.Services.Dtos;

public class CommentReplyDto
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }
    public Guid UserId { get; set; }
    public Guid EntityId { get; set; }

    [Required]
    [MaxLength(500)]
    public string Text { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public Guid? ParentId { get; set; }
    public CommentType CommentType { get; set; }
}