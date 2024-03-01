using Microsoft.AspNetCore.Mvc;
using Semprg_BlazorIntroductory.Contracts;
using Semprg_BlazorIntroductory.Entity.Repositories;

namespace Semprg_BlazorIntroductory.API.Endpoints;

public static class GameEndpoints
{
    public static IEndpointRouteBuilder AddGameEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapPost(
            "/add-action",
            async (
                [FromBody] AddStoryActionRequest request,
                IGameTreeRepository gameTreeRepository) =>
            {
                var createdAction = await gameTreeRepository.AddStoryAction(
                    request.ToNodeId,
                    request.ActionContent,
                    request.ConsequenceContent);

                if (createdAction is null)
                    return Results.BadRequest();

                var response = new StoryActionResponse(
                    new StoryNodeResponse(createdAction.Consequence!.StoryContent, createdAction.Consequence.Id),
                    createdAction.ActionContent);

                return Results.Ok(response);
            });

        routes.MapGet(
            "/story-actions",
            async (
                [FromQuery] int ofNodeId,
                IGameTreeRepository gameTreeRepository) =>
            {
                var storyActions = await gameTreeRepository.GetStoryActions(ofNodeId);

                //Map response
                var storyActionsResponses = storyActions.Select(a => new StoryActionResponse(
                        new StoryNodeResponse(a.Consequence!.StoryContent, a.Consequence.Id),
                        a.ActionContent))
                    .ToList();
                var response = new StoryActionsResponse(storyActionsResponses);

                return Results.Ok(response);
            });
        routes.MapGet(
            "/story-node",
            async (
                [FromQuery] int nodeId,
                IGameTreeRepository gameTreeRepository) =>
            {
                var storyNode = await gameTreeRepository.GetStoryNodeById(nodeId);
                if (storyNode is null)
                    return Results.NotFound();

                var response =
                    new StoryNodeResponse(storyNode.StoryContent, storyNode.Id);

                return Results.Ok(response);
            });

        return routes;
    }
}