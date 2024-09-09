using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBonus : MonoBehaviour
{
    public static PlayerBonus Instance { get; private set; }

    [SerializeField] private int minGold = 1000;
    [SerializeField] private int maxGold = 10000;
    [SerializeField] private int minCrystals = 1;
    [SerializeField] private int maxCrystals = 4;

    private HelpersBoost _helpersBoost;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _helpersBoost = FindObjectOfType<HelpersBoost>();
    }

    public void AcceptBonus(Action onRewardGranted)
    {
        AdRewarded.Instance.LoadRewardedAd();
        AdRewarded.Instance.ShowRewardedAd(onRewardGranted);
    }

    public Reward GetRandomReward(RewardType type)
    {
        int minAmount = type == RewardType.Gold ? minGold : minCrystals;
        int maxAmount = type == RewardType.Gold ? maxGold : maxCrystals;

        int randomValue = UnityEngine.Random.Range(minAmount, maxAmount + 1);
        return new Reward(type, randomValue);
    }

    public void GrantReward(Reward reward, int multiplier = 1)
    {
        PlayerManager.Instance.AddReward(reward.Amount * multiplier, reward.Type);
    }

    public void ReplenishResources()
    {
        PlayerManager.Instance.CurrentAmmo += PlayerManager.Instance.MaxAmmo;
    }

    public void BoostHelpers()
    {
        if (_helpersBoost != null)
        {
            _helpersBoost.StartBoost();
        }
        else
        {
            Debug.LogWarning("HelpersBoost not found.");
        }
    }

    public void AutoMergeTowers()
    {
        TowerManager.Instance.ActiveAutoMerge();
    }

    public void AddSkills(int skillId)
    {
        var skillMap = new Dictionary<int, Skill>
        {
            { 1, new Shock() },
            { 2, new Rage() },
            { 3, new Strike() },
            { 4, new X3() }
        };

        if (skillMap.TryGetValue(skillId, out Skill skill))
        {
            skill.Activate();
            Debug.Log($"Skill activated: {skill.GetType().Name}");
        }
        else
        {
            Debug.LogWarning($"Skill with ID {skillId} not found.");
        }
    }
}
