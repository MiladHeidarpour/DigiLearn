using CommentModule.Domain;
using Common.Query.Filter;

namespace CommentModule.Services.Dtos;

public class CommentFilterResult : BaseFilter<CommentDto>
{

}

public class AllCommentFilterResult : BaseFilter<CommentReplyDto>
{

}

public class CommentFilterParams : BaseFilterParam
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public CommentType? CommentType { get; set; } = null;
    public Guid? EntityId { get; set; } = null;
    public string? Name { get; set; }
    public string? Family { get; set; }
}