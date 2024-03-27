namespace Application.Templates.Commands;

public enum QuestionTemplateAddConflictOptions
{
    AddToAllChecklistQuestionsAndSetStatusInProgress,
    DoNothing
}

public enum QuestionTemplateEditConflictOptions
{
    EditOnAllChecklistQuestionsAndSetStatusInProgress,
    DoNothing
}

public enum QuestionTemplateDeleteConflictOptions
{
    DeleteOnAllChecklistQuestionsAndSetStatusInProgress,
    DeleteOnAllChecklistQuestions
}