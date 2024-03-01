using Semprg_BlazorIntroductory.API.Model;

namespace Semprg_BlazorIntroductory.Entity.Repositories;

public interface IGameTreeRepository
{
    /// <returns>Added story action, null if error</returns>
    public Task<StoryAction?> AddStoryAction(int toNodeId, string actionContent, string consequenceContent);
    /// <returns>A Non-null list. It may be empty.</returns>
    public Task<List<StoryAction>> GetStoryActions(int ofNodeId);

    public Task<StoryNode?> GetStoryNodeById(int nodeId);
}