using System.Collections.Generic;
using System;

public static class GlobalData 
{
    public static event Action<PlantModel.Names, int> OnCounterValueChanged;
    public static event Action<int> OnExperienceValueChanged;
    public static List<PlantCounter> PlantCounters = new();
    public static int Experience;
    public static bool AreChooseButtonsExist;
    public static void Reset()
    {
        OnCounterValueChanged = null;
        OnExperienceValueChanged = null;
        Experience = 0;             
        PlantCounters = new();
        AreChooseButtonsExist = false;
    }
    public static void AddExperience(int value)
    {
        Experience += value;
        OnExperienceValueChanged?.Invoke(Experience);
    }
    public static void AddCounterValue(PlantModel.Names name, int value)
    {
        for (int i = 0; i < PlantCounters.Count; i++)
        {
            if (PlantCounters[0].Name == name)
            {
                PlantCounters[0].Count += value;
                OnCounterValueChanged?.Invoke(name, PlantCounters[0].Count);
            }            
        }    
    }
}
