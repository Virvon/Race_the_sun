using UnityEngine;

namespace Assets.RaceTheSun.Sources.Data
{
    public static class DataExtansions
    {
        public static T ToDeserialized<T>(this string json) =>
            JsonUtility.FromJson<T>(json);

        public static string ToJson(this object obj) =>
            JsonUtility.ToJson(obj);
    }
}