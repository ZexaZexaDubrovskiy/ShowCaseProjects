using System;
using UnityEngine;

public class FortunaWheelTypeA : FortunaWheelBase
{
    public static FortunaWheelTypeA Instance { get; private set; }

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
        sectorContents = new[] { 1, 2, 3, 4 };
        sectorAngles = new[]
        {
            255.61f, 40.32f,
            40.33f, 116.64f,
            116.65f, 172.8f,
            172.9f, 255.6f
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
