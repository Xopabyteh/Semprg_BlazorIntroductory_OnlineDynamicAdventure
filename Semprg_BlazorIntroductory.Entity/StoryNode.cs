namespace Semprg_BlazorIntroductory.API.Model;

public class StoryNode
{
    /// <summary>
    /// How many actions can a node have as children.
    /// This doesn't apply to the root node. The root node can have however many actions.
    /// </summary>
    public const int MaxNonRootActionsPerNode = 2;

    public int Id { get; set; }
    public string StoryContent { get; init; }
    public ICollection<StoryAction> ActionsChildren { get; init; } = new List<StoryAction>();
    public StoryAction ParentAction { get; init; }
    public int ParentActionId { get; set; }
    public bool HasReachedMaxActions { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    // ReSharper disable once UnusedMember.Local
    public StoryNode() //For EF Core
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    { }
}