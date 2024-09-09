using UnityEngine;
using UnityEngine.UI;

public class QuestUI
{
    public Image questImage; // передняя картинка квеста
    public Image rewardImage; // передняя картинка награды
    public Image backRewardImage; // задняя картинка награды
    public Transform progressBar; // шкала заполнености 
    public Button getRewardButton; //кнопка сбора награды
    public TMPro.TextMeshProUGUI nameQuest; // название квеста(цель)
    public TMPro.TextMeshProUGUI statusQuest; // сколько выполненно из скольки
    public TMPro.TextMeshProUGUI rewardAmountQuest; // кол-во награды за квест

    public QuestUI(Transform questTransform)
    {
        questImage = FindDeepChild(questTransform, "questImage").GetComponent<Image>();
        rewardImage = FindDeepChild(questTransform, "rewardImage").GetComponent<Image>();
        backRewardImage = FindDeepChild(questTransform, "backRewardImage").GetComponent<Image>();
        progressBar = FindDeepChild(questTransform, "progressBar").GetComponent<Transform>();

        getRewardButton = FindDeepChild(questTransform, "getRewardButton").GetComponent<Button>();

        nameQuest = FindDeepChild(questTransform, "nameQuest").GetComponent<TMPro.TextMeshProUGUI>();
        statusQuest = FindDeepChild(questTransform, "statusQuest").GetComponent<TMPro.TextMeshProUGUI>();
        rewardAmountQuest = FindDeepChild(questTransform, "rewardAmountQuest").GetComponent<TMPro.TextMeshProUGUI>();
    }

    public Transform FindDeepChild(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
                return child;

            Transform result = FindDeepChild(child, name);
            if (result != null)
                return result;
        }

        return null;
    }
}