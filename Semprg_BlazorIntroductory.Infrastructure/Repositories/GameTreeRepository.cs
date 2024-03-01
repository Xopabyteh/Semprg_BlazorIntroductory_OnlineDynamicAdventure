using Microsoft.EntityFrameworkCore;
using Semprg_BlazorIntroductory.API.Model;
using Semprg_BlazorIntroductory.Entity.Repositories;

namespace Semprg_BlazorIntroductory.Infrastructure.Repositories;

public class GameTreeRepository : IGameTreeRepository
{
    private readonly AppDbContext _dbContext;
    
    public GameTreeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string?> AddStoryAction(int toNodeId, string actionContent, string consequenceContent)
    {
        //Validate (I know domain validation shouldn't belong here, but it's just a quick project)
        //Check if the node exists
        var toNode = await _dbContext.StoryNodes
            .Include(s => s.ActionsChildren)
            .ThenInclude(a => a.Consequence)
            .SingleOrDefaultAsync(s => s.Id == toNodeId);

        if (toNode is null)
            return "The node to add the new story to doesn't exist.";

        //Check if the node has reached max actions
        if (toNodeId != 1 //Doesn't apply to root node
            && toNode.HasReachedMaxActions)
            return "The node has reached the maximum number of children.";

        //Add action to node
        var consequence = new StoryNode()
        {
            StoryContent = consequenceContent,
        };
        var newAction = new StoryAction
        {
            ActionContent = actionContent,
            StoryNodeParent = toNode,
            Consequence = consequence
        };

        await _dbContext.StoryActions.AddAsync(newAction);
        await _dbContext.StoryNodes.AddAsync(consequence);

        //Update the parent node (Check whether it has reached max actions)
        if (toNodeId != 1) //Doesn't apply to root node
        {
            var actionsCount = toNode.ActionsChildren.Count;
            toNode.HasReachedMaxActions = actionsCount >= StoryNode.MaxNonRootActionsPerNode;
        }

        await _dbContext.SaveChangesAsync();
        return null; //Success
    }

    public async Task<List<StoryAction>> GetStoryActions(int ofNodeId)
    {
        return await _dbContext.StoryActions
            .Include(a => a.StoryNodeParent)
            .Include(a => a.Consequence)
            .Where(a => a.StoryNodeParent!.Id == ofNodeId)
            .ToListAsync();
    }
}