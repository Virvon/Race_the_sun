using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWindowData", menuName = "StaticData/Window static data")]
public class WindowStaticData : ScriptableObject
{
    public List<WindowConfig> Configs;
}