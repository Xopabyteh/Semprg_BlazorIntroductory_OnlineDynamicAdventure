namespace Semprg_BlazorIntroductory.Contracts;

public readonly record struct StoryActionsResponse(List<StoryActionResponse> StoryActions);
public readonly record struct StoryActionResponse(StoryNodeResponse Consequence, string Content);
public readonly record struct StoryNodeResponse(string Content, int NodeId);