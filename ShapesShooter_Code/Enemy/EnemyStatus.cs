using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public struct EnemyStatus
{
    public int enemyId;
    public int enemytype;
    public int hp;
    public Vector3 spawnPos;
    public int appearanceTime;
    public GameObject dropItem;
    public int dropItem_count;
    public int energyItem_count;
    public State state;
    public bool appearanceFlag;
}

public struct EnemyList
{
    public EnemyStatus[] enemyStatus;
    public int enemyCount;
    public int deathEnemyCount;
}

public enum State { Nomal, Non }
