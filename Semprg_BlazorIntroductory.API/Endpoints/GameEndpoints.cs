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
                var errorMessage = await gameTreeRepository.AddStoryAction(
                    request.ToNodeId,
                    request.ActionContent,
                    request.ConsequenceContent);

                if(errorMessage is null)
                    return Results.Ok();

                // Error ->
                return Results.BadRequest(errorMessage);
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
                        new StoryNodeResponse(a.Consequence.StoryContent, a.Consequence.Id),
                        a.ActionContent))
                    .ToList();
                var response = new StoryActionsResponse(storyActionsResponses);

                return Results.Ok(response);
            });

        return routes;
    }
}