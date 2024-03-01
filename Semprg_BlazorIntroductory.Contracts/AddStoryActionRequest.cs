namespace Semprg_BlazorIntroductory.Contracts;

public readonly record struct AddStoryActionRequest(
    int ToNodeId,
    string ActionContent,
    string ConsequenceContent);