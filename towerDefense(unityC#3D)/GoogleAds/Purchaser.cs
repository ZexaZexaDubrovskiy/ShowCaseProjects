using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

// Singleton pattern
public class Purchaser : MonoBehaviour
{
    public static Purchaser Instance { get; private set; }
    private readonly Dictionary<string, IPurchaseCommand> purchaseCommands;

    // Constructor Injection (Dependency Injection)
    public Purchaser(IRewardFactory rewardFactory)
    {
        purchaseCommands = new Dictionary<string, IPurchaseCommand>
        {
            { "TD.superpack", new AddMultipleRewardsCommand(rewardFactory.CreateReward(100, RewardType.Crystal), rewardFactory.CreateReward(50000, RewardType.Gold)) },
            { "TD.megapack", new AddSingleRewardCommand(rewardFactory.CreateReward(1000000, RewardType.Crystal)) },
            { "TD.25crystal", new AddSingleRewardCommand(rewardFactory.CreateReward(25, RewardType.Crystal)) },
            { "TD.50crystal", new AddSingleRewardCommand(rewardFactory.CreateReward(50, RewardType.Crystal)) },
            { "TD.150crystal", new AddSingleRewardCommand(rewardFactory.CreateReward(150, RewardType.Crystal)) },
            { "TD.10kgold", new AddSingleRewardCommand(rewardFactory.CreateReward(10000, RewardType.Gold)) },
            { "TD.25kgold", new AddSingleRewardCommand(rewardFactory.CreateReward(25000, RewardType.Gold)) },
            { "TD.100kgold", new AddSingleRewardCommand(rewardFactory.CreateReward(100000, RewardType.Gold)) }
        };
    }

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

    public void OnPurchaseCompleted(Product product)
    {
        if (purchaseCommands.TryGetValue(product.definition.id, out var command))
        {
            command.Execute();
        }
        else
        {
            Debug.LogWarning($"Product ID not found: {product.definition.id}");
        }
    }
}
