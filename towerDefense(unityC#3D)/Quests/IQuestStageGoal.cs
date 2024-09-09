public interface IQuestStageGoal
{
    bool IsGoalCompleted();
    void UpdateProgress(object progressData);
    string GetProgressDescription();
    int TargetCount { get; }
    int CurrentCount { get; }
}