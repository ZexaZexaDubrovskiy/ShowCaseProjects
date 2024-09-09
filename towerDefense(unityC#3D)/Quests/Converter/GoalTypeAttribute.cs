using System;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class GoalTypeAttribute : Attribute
{
    public string GoalType { get; }

    public GoalTypeAttribute(string goalType) => GoalType = goalType;
    
}