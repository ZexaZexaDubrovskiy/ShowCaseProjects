using System;
using UnityEngine;

public class FortunaWheelTypeB : FortunaWheelBase
{
    public static FortunaWheelTypeB Instance { get; private set; }

    protected override void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Инициализация секторов и углов
        sectorContents = new[] { 1, 2, 1, 2 };
        sectorAngles = new[]
        {
            7.56f, 62.28f,
            62.29f, 113.4f,
            113.41f, 172.44f,
            172.45f, 7.55f
        };

        base.Awake();
    }

    protected override void CalculateCurrentSector()
    {
        float normalizedRotation = NormalizeAngle(currentRotation);

        for (int i = 0; i < sectorAngles.Length; i += 2)
        {
            float startAngle = sectorAngles[i];
            float endAngle = sectorAngles[i + 1];

            if (IsAngleInSector(normalizedRotation, startAngle, endAngle))
            {
                CurrentSector = i / 2 + 1;
                break;
            }
        }
    }

    protected override void ValidateSectorData()
    {
        if (sectorAngles == null || sectorAngles.Length == 0 || sectorAngles.Length % 2 != 0)
        {
            Debug.LogError("sectorAngles array is either empty or not initialized correctly!");
            return;
        }

        for (int i = 0; i < sectorAngles.Length; i++)
        {
            sectorAngles[i] = NormalizeAngle(sectorAngles[i]);
        }
    }
}
