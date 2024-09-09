public class QuestTypesBase : IQuestStageGoal
{
    public int TargetCount { get; protected set; }
    public int CurrentCount { get; set; }

    public QuestTypesBase(int targetCount)
    {
        TargetCount = targetCount;
        CurrentCount = 0;
    }

    public virtual bool IsGoalCompleted() => CurrentCount >= TargetCount;

    public virtual void UpdateProgress(object progressData)
    {
        if (progressData is int progress)
            CurrentCount += progress;
    }

    public virtual string GetProgressDescription() => $"{CurrentCount} / {TargetCount}";
}