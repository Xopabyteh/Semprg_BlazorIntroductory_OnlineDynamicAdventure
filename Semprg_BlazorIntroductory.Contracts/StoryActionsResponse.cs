namespace Semprg_BlazorIntroductory.Contracts;

public readonly record struct StoryActionsResponse(List<StoryActionResponse> StoryActions);
public readonly record struct StoryActionResponse(StoryNodeResponse Consequence, string Content);

public readonly record struct StoryNodeResponse(string Content, int NodeId)
{
    /// <summary>
    /// How many actions can a node have as children.
    /// This doesn't apply to the root node. The root node can have however many actions.
    /// </summary>
    public const int MaxNonRootActionsPerNode = 2;
}