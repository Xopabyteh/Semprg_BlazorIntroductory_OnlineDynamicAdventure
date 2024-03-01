using Semprg_BlazorIntroductory.API.Model;

namespace Semprg_BlazorIntroductory.Entity.Repositories;

public interface IGameTreeRepository
{
    /// <returns>Null if success or Error message</returns>
    public Task<string?> AddStoryAction(int toNodeId, string actionContent, string consequenceContent);
    /// <returns>A Non-null list. It may be empty.</returns>
    public Task<List<StoryAction>> GetStoryActions(int ofNodeId);
}