using Microsoft.EntityFrameworkCore;
using Semprg_BlazorIntroductory.API.Model;

namespace Semprg_BlazorIntroductory.Infrastructure.Services;

public class GameTreeService
{
    private readonly AppDbContext _dbContext;

    public GameTreeService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task EnsureRootExistsAsync(string rootStoryContent)
    {
        //await _dbContext.Database.EnsureDeletedAsync();
        await _dbContext.Database.EnsureCreatedAsync();

        if (await _dbContext.StoryNodes.AnyAsync(n => n.Id == 0))
            return; //Root node exists

        // -> No root node
        var rootAction = new StoryAction()
        {
            ActionContent = "",
            Consequence = null,
            StoryNodeParent = null,
        };

        var rootStoryNode = new StoryNode()
        {
            ParentActionId = rootAction.Id,
            ParentAction = rootAction,
            StoryContent = rootStoryContent,
            ActionsChildren = new List<StoryAction>(),
            HasReachedMaxActions = false,
        };

        _dbContext.StoryActions.Add(rootAction);
        _dbContext.StoryNodes.Add(rootStoryNode);

        await _dbContext.SaveChangesAsync();
    }
}