﻿using CommentModule.Domain;
using CommentModule.Services;
using CommentModule.Services.Dtos;
using Common.Application;
using DigiLearn.Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigiLearn.Web.Controllers;

public class CommentController : BaseController
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateComment(CreateCommentCommand command)
    {
        command.UserId = User.GetUserId();
        return await AjaxTryCatch(async () => await _commentService.CreateComment(command));
    }

    [Route("/comment/getByFilter")]
    public async Task<IActionResult> GetComments([FromQuery] Guid entityId, [FromQuery] CommentType commentType, [FromQuery] int pageId = 1)
    {
        var model = await _commentService.GetCommentByFilter(new CommentFilterParams()
        {
            CommentType = commentType,
            EntityId = entityId,
            PageId = pageId,
        });
        return PartialView("Comments/_Comments", model);
    }

    [Authorize]
    [HttpPost("/comment/delete")]
    public async Task<IActionResult> DeleteComment(Guid commentId)
    {
        var comment = await _commentService.GetCommentById(commentId);
        if (comment is null || comment.UserId != User.GetUserId())
        {
            return await AjaxTryCatch(() => Task.FromResult(OperationResult.NotFound()));
        }
        return await AjaxTryCatch(async () => await _commentService.DeleteComment(commentId));
    }
}