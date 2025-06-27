using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CommentModule.Domain;

namespace CommentModule.Services.Dtos;

public class CommentDto
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
    public CommentType CommentType { get; set; }

    [ForeignKey("ParentId")]
    public List<CommentReplyDto> Replies { get; set; }
}