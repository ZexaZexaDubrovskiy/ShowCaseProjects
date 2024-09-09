using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class QuestConverter
{
    private static string _pathToFile = Application.dataPath + "/Scripts/Quests/quests.json";

    public static void ConvertQuestsToJson(List<Quest> quests)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings();
        settings.Converters.Add(new IQuestStageGoalConverter());
        string json = JsonConvert.SerializeObject(quests, Formatting.Indented, settings);

        File.WriteAllText(_pathToFile, json);
    }

    public static void ConvertJsonToQuests(ref List<Quest> quests)
    {
        string json = File.ReadAllText(_pathToFile);
        JsonSerializerSettings settings = new JsonSerializerSettings();
        settings.Converters.Add(new IQuestStageGoalConverter());

        quests = JsonConvert.DeserializeObject<List<Quest>>(json, settings);
    }
}