using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    [System.Serializable] public class enemiesPerLevelData
    {
        public int easy = 2;
        public int medium = 3;
        public int hard = 5;
    }

    public enemiesPerLevelData[] levels= new enemiesPerLevelData[3];
}
