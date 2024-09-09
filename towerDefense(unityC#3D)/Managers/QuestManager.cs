using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : Singleton<QuestManager>
{
    public GameObject QuestPanel;
    public GameObject QuestItem;
    public Transform QuestScrollView;
    public List<QuestData> questDataList;

    private List<Quest> quests = new List<Quest>();
    private List<QuestUI> questUIs = new List<QuestUI>();

    private void OnEnable()
    {
        GameEvents.OnEnemyKilled += HandleEnemyKilled;
        GameEvents.OnAbilityUsed += HandleAbilityUsed;
        GameEvents.OnTowersLevelIncreased += HandleTowersLevelIncreased;
        GameEvents.OnResourcesSpent += HandleResourcesSpent;
        GameEvents.OnBossKilled += HandleBossKilled;
        GameEvents.OnPapaKilled += HandlePapaKilled;
        GameEvents.OnCriticalHitsDonned += HandleCriticalHitsDonned;
        GameEvents.OnWavesEnded += HandleWavesEnded;
        GameEvents.OnGoldSpent += HandleGoldSpent;
        GameEvents.OnCrystalsSpent += HandleCrystalsSpent;
        GameEvents.OnTowersImproved += HandleTowersImproved;
        GameEvents.OnHelpersImproved += HandleHelpersImproved;
    }

    private void OnDisable()
    {
        GameEvents.OnEnemyKilled -= HandleEnemyKilled;
        GameEvents.OnAbilityUsed -= HandleAbilityUsed;
        GameEvents.OnTowersLevelIncreased -= HandleTowersLevelIncreased;
        GameEvents.OnResourcesSpent -= HandleResourcesSpent;
        GameEvents.OnBossKilled -= HandleBossKilled;
        GameEvents.OnPapaKilled -= HandlePapaKilled;
        GameEvents.OnCriticalHitsDonned -= HandleCriticalHitsDonned;
        GameEvents.OnWavesEnded -= HandleWavesEnded;
        GameEvents.OnGoldSpent -= HandleGoldSpent;
        GameEvents.OnCrystalsSpent -= HandleCrystalsSpent;
        GameEvents.OnTowersImproved -= HandleTowersImproved;
        GameEvents.OnHelpersImproved -= HandleHelpersImproved;
    }

    private void HandleEnemyKilled(int value)
    {
        QuestStage currentStage = GetCurrentStageQuestForType(typeof(KillGoal));
        if (currentStage != null)
        {
            currentStage.UpdateStageProgress(value);
            Debug.Log("Kill enemy");
        }
    }

    private void HandleAbilityUsed(int value)
    {
        QuestStage currentStage = GetCurrentStageQuestForType(typeof(AbilityUseGoal));
        if (currentStage != null)
        {
            currentStage.UpdateStageProgress(value);
            Debug.Log("Ability Used");
        }
    }

    private void HandleTowersLevelIncreased(int value)
    {
        QuestStage currentStage = GetCurrentStageQuestForType(typeof(TowersLevelIncreasedGoal));
        if (currentStage != null)
        {
            currentStage.UpdateStageProgress(value);
            Debug.Log("Tower levelup");
        }
    }

    private void HandleResourcesSpent(int value)
    {
        QuestStage currentStage = GetCurrentStageQuestForType(typeof(ResourcesSpentGoal));
        if (currentStage != null)
        {
            currentStage.UpdateStageProgress(value);
            Debug.Log("Resources spent");
        }
    }

    private void HandleBossKilled(int value)
    {
        QuestStage currentStage = GetCurrentStageQuestForType(typeof(BossKilledGoal));
        if (currentStage != null)
        {
            currentStage.UpdateStageProgress(value);
            Debug.Log("Kill boss enemy");
        }
    }

    private void HandlePapaKilled(int value)
    {
        QuestStage currentStage = GetCurrentStageQuestForType(typeof(PapaKilledGoal));
        if (currentStage != null)
        {
            currentStage.UpdateStageProgress(value);
            Debug.Log("Papa Kill enemy");
        }
    }

    private void HandleCriticalHitsDonned(int value)
    {
        QuestStage currentStage = GetCurrentStageQuestForType(typeof(CriticalHitsDonnedGoal));
        if (currentStage != null)
        {
            currentStage.UpdateStageProgress(value);
            Debug.Log("critical hit");
        }
    }

    private void HandleWavesEnded(int value)
    {
        QuestStage currentStage = GetCurrentStageQuestForType(typeof(WavesEndedGoal));
        if (currentStage != null)
        {
            currentStage.UpdateStageProgress(value);
            Debug.Log("Waves ended");
        }
    }

    private void HandleGoldSpent(int value)
    {
        QuestStage currentStage = GetCurrentStageQuestForType(typeof(GoldSpentGoal));
        if (currentStage != null)
        {
            currentStage.UpdateStageProgress(value);
            Debug.Log("gold spent");
        }
    }

    private void HandleCrystalsSpent(int value)
    {
        QuestStage currentStage = GetCurrentStageQuestForType(typeof(CrystalsSpentGoal));
        if (currentStage != null)
        {
            currentStage.UpdateStageProgress(value);
            Debug.Log("crystals spent");
        }
    }

    private void HandleTowersImproved(int value)
    {
        QuestStage currentStage = GetCurrentStageQuestForType(typeof(TowersImprovedGoal));
        if (currentStage != null)
        {
            currentStage.UpdateStageProgress(value);
            Debug.Log("tower improved(merge)");
        }
    }

    private void HandleHelpersImproved(int value)
    {
        QuestStage currentStage = GetCurrentStageQuestForType(typeof(HelpersImprovedGoal));
        if (currentStage != null)
        {
            currentStage.UpdateStageProgress(value);
            Debug.Log("helpers improved");
        }
    }


    private void Awake()
    {   
        quests = new List<Quest>();
        var questDataArray = Resources.LoadAll<QuestData>("QuestImages");
        questDataList = new List<QuestData>(questDataArray);
        QuestConverter.ConvertJsonToQuests(ref quests);
    }

    private void Update()
    {
        UpdateQuests();
        UpdateAndLoadQuestsUI();
    }

    public void UpdateAndLoadQuestsUI()
    {
        if (questUIs.Count == 0)
        {
            foreach (var quest in quests)
            {
                GameObject newQuestInstance = Instantiate(QuestItem, QuestScrollView);

                QuestUI questUI = new QuestUI(newQuestInstance.transform);
                questUIs.Add(questUI);

                questUI.getRewardButton.onClick.AddListener(() => quest.GetReward());
            }
        }

        //TODO: refactor for
        for (int i = 0; i < quests.Count; i++)
        {
            var quest = quests[i];
            var questUI = questUIs[i];

            questUI.questImage.sprite = questDataList.Find(qd => qd.questName == i.ToString()).questImage;
            questUI.rewardImage.sprite = quest.stages[quest.currentStageIndex].reward.Type == RewardType.Gold
                ? questDataList.Find(qd => qd.questName == "gold").questImage
                : questDataList.Find(qd => qd.questName == "crystal").questImage;

            var goal = quest.stages[quest.currentStageIndex].goal;


            float progressBarSize = goal.CurrentCount < goal.TargetCount ? (float)goal.CurrentCount / (float)goal.TargetCount : 1;
            
            questUI.progressBar.localScale = new Vector2(progressBarSize, questUI.progressBar.localScale.y);

            questUI.nameQuest.text = quest.description;
            questUI.statusQuest.text = quest.stages[quest.currentStageIndex].GetStageProgressDescription();
            questUI.rewardAmountQuest.text = quest.stages[quest.currentStageIndex].reward.Amount.ToString();

            if (quest.isCompleted)
            {
                questUI.backRewardImage.color = new Color(1f,1f,1f, 1f);
                questUI.rewardImage.color = new Color(1f,1f,1f, 1f);
                questUI.backRewardImage.sprite = questDataList.Find(qd => qd.questName == "complete").questImage;
            }
            else
            {
                questUI.backRewardImage.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                questUI.rewardImage.color = new Color(0.5f, 0.5f, 0.5f, 1f);

                if (quest.stages[quest.currentStageIndex].isCompletedStage)
                {
                    questUI.backRewardImage.color = new Color(1f,1f,1f, 1f);
                    questUI.rewardImage.color = new Color(1f,1f,1f, 1f);
                }
                else
                {
                    questUI.backRewardImage.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    questUI.rewardImage.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                }
                
                questUI.getRewardButton.interactable = true;
                questUI.backRewardImage.sprite = questDataList.Find(qd => qd.questName == "ready").questImage;
            }
        }
    }

    public void AddQuest(Quest newQuest) => quests.Add(newQuest);

    public void UpdateQuests()
    {
        foreach (var quest in quests)
            quest.UpdateQuestStatus();
    }

    public void OpenQuestsPanel() => QuestPanel.SetActive(true);
    public void CloseQuestsPanel() => QuestPanel.SetActive(false);

    private QuestStage GetCurrentStageQuestForType(Type goalType)
    {
        foreach (var quest in quests)
            if (quest.stages[quest.currentStageIndex].goal.GetType() == goalType)
                return quest.stages[quest.currentStageIndex];
        return null;
    }

    private void OnApplicationQuit() => QuestConverter.ConvertQuestsToJson(quests);
}
