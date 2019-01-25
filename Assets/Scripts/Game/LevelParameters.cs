using UnityEngine;

[CreateAssetMenu(fileName = "Level Parameters", menuName = "ScriptableObjects/LevelParameters", order = 1)]
public class LevelParameters : ScriptableObject
{
    public int[] enemiesPerWave;
    public Transform[] spawnLocations;
}
