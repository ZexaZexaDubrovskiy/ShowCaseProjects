using UnityEngine;

public class QuestStage
{
    public IQuestStageGoal goal;
    public Reward reward;
    public bool isCompletedStage;

    public QuestStage(IQuestStageGoal goal, Reward reward)
    {
        this.goal = goal;
        this.reward = reward;
        isCompletedStage = false;
    }

    public void UpdateStageProgress(object progressData)
    {
        if (!isCompletedStage)
        {
            goal.UpdateProgress(progressData);
            if (goal.IsGoalCompleted())
            {
                CompleteStage();
            }
        }
    }
    public bool IsStageCompleted() => isCompletedStage;
    
    private void CompleteStage()
    {
        if (!isCompletedStage && goal.IsGoalCompleted())
        {
            isCompletedStage = true;
            Debug.Log($"Этап квеста завершен.");
        }
    }

    public string GetStageProgressDescription() {
        if (goal == null)
        {
                Debug.Log("Error: goal not set");
            return "goal not set.";
        }
        return goal.GetProgressDescription(); 
    }
}