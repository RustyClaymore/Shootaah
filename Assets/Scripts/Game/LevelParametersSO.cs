using UnityEngine;

[CreateAssetMenu(fileName = "Level Parameters", menuName = "ScriptableObjects/LevelParameters", order = 1)]
public class LevelParametersSO : ScriptableObject
{
    public int[] enemiesPerWave;
    public float timeBeforeWaveSpawn;
}
