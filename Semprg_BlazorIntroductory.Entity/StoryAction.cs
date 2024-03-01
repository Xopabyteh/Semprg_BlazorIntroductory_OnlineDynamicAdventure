namespace Semprg_BlazorIntroductory.API.Model;

public class StoryAction
{
    public int Id { get; init; }
    public string ActionContent { get; init; }
    public StoryNode? StoryNodeParent { get; init; }
    //public int StoryNodeParentId { get; set; }
    public StoryNode? Consequence { get; init; }
    //public int ConsequenceId { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    // ReSharper disable once UnusedMember.Local
    public StoryAction() //For EF Core
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    { }
}