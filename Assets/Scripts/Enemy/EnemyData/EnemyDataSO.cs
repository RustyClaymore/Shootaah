﻿using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 1)]
public class EnemyDataSO : ScriptableObject
{
    public int maxHealth;
    public float speed;

    public int impactDamage;

    public float roamRadius;
    public float chaseDistance;
    public float maxShootDistance;
    public float minShootDistance;

    public int minDiamondValue;
    public int maxDiamondValue;
}