using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;
using System.Reflection;
using System;

public class IQuestStageGoalConverter : JsonConverter
{
    public override bool CanConvert(Type objectType) => typeof(IQuestStageGoal).IsAssignableFrom(objectType);

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject jsonObject = JObject.Load(reader);
        string goalType = (string)jsonObject["GoalType"];
        IQuestStageGoal goal = null;

        var goalTypes = typeof(IQuestStageGoal).Assembly.GetTypes()
            .Where(t => typeof(IQuestStageGoal).IsAssignableFrom(t) && t.GetCustomAttribute<GoalTypeAttribute>()?.GoalType == goalType);

        foreach (var type in goalTypes)
        {
            goal = (IQuestStageGoal)Activator.CreateInstance(type, new object[] { (int)jsonObject["TargetCount"] });
            var currentCountProperty = type.GetProperty("CurrentCount");
            currentCountProperty.SetValue(goal, (int)jsonObject["CurrentCount"]);
            break;
        }

        if (goal == null)
        {
            throw new ArgumentException("Unknown goal type");
        }

        return goal;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        JObject jsonObject = new JObject();
        var type = value.GetType();
        var goalTypeAttribute = type.GetCustomAttribute<GoalTypeAttribute>();

        if (goalTypeAttribute != null)
        {
            jsonObject["GoalType"] = goalTypeAttribute.GoalType;
            var targetCountProperty = type.GetProperty("TargetCount");
            var currentCountProperty = type.GetProperty("CurrentCount");

            jsonObject["TargetCount"] = JToken.FromObject(targetCountProperty.GetValue(value));
            jsonObject["CurrentCount"] = JToken.FromObject(currentCountProperty.GetValue(value));

            jsonObject.WriteTo(writer);
        }
        else
        {
            throw new ArgumentException("Unknown goal type");
        }
    }
}