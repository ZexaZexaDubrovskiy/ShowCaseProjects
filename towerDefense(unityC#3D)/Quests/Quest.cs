using System.Collections.Generic;
using UnityEngine;
public class Quest
{
    public int id;
    public string description;
    public Reward reward;
    public bool isCompleted;
    public List<QuestStage> stages;
    public int currentStageIndex;

    public Quest(int id, string description, Reward reward)
    {
        this.id = id;
        this.description = description;
        this.reward = reward;
        isCompleted = false;
        stages = new List<QuestStage>();
        currentStageIndex = 0;
    }

    public void AddStage(QuestStage stage) => stages.Add(stage);

    public void UpdateQuestStatus()
    {
        if (currentStageIndex < stages.Count && stages[currentStageIndex].IsStageCompleted())
            CompleteQuest();
    }

    private void CompleteQuest()
    {
        if (currentStageIndex == stages.Count - 1)
        {
            isCompleted = true;
            Debug.Log($"Квест полностью завершен");
        }
    }

    public void GetReward()
    {
        if (currentStageIndex < stages.Count && stages[currentStageIndex].IsStageCompleted())
        {
            if (currentStageIndex == stages.Count - 1)
            {
                isCompleted = true;
                PlayerManager.Instance.AddReward(reward.Amount, reward.Type);
                Debug.Log($"Квест завершен: {description}. Награда: {reward.Amount}-{reward.Type}.");
            }
            else
            {
                Reward stageReward = stages[currentStageIndex].reward;
                PlayerManager.Instance.AddReward(stageReward.Amount, stageReward.Type);
                Debug.Log($"Этап квеста завершен: {description}. Награда: {stageReward.Amount} {stageReward.Type}.");
                currentStageIndex++;
            }
        }
        else
            Debug.Log("Этап квеста не завершен или квест уже завершен.");

    }
}