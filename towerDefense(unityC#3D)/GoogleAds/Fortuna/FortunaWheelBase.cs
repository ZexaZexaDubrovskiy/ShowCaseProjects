using System.Collections.Generic;
using UnityEngine;

public abstract class FortunaWheelBase : MonoBehaviour
{
    public Transform arrow;
    public float speed = 100f;
    public float[] sectorAngles;
    public int[] sectorContents;
    protected float currentRotation;
    protected int currentSector;

    public int CurrentSector
    {
        get { return currentSector; }
        protected set
        {
            if (value <= 0)
            {
                currentSector = 1;
            }
            else
            {
                currentSector = value;
            }
        }
    }

    protected virtual void Awake()
    {
        currentSector = 1;
        ValidateSectorData();
    }

    protected void Update()
    {
        RotateArrow();
        CalculateCurrentSector();
    }

    protected void RotateArrow()
    {
        currentRotation += speed * Time.deltaTime;
        if (currentRotation >= 360f) currentRotation -= 360f; 
        arrow.rotation = Quaternion.Euler(0f, 0f, -currentRotation);
    }
    
    protected float NormalizeAngle(float angle)
    {
        while (angle < 0f) angle += 360f;
        while (angle >= 360f) angle -= 360f;
        return angle;
    }

    protected abstract void CalculateCurrentSector();
    
    protected bool IsAngleInSector(float angle, float startAngle, float endAngle)
    {
        if (startAngle < endAngle) return angle >= startAngle && angle < endAngle;
        else return angle >= startAngle || angle < endAngle;
    }
    
    protected abstract void ValidateSectorData();
}
