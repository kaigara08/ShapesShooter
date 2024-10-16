using UnityEngine;

[System.Serializable]
public class EnemyDataEntity
{
    public int enemyID;
    public int stageID;
    public int enemytype;
    public int hp;
    public float spawnPos_x;
    public float spawnPos_y;
    public float spawnPos_z;
    public int appearanceTime;
    public int dropItem;
    public int dropItem_count;
    public int energy_count;
    public bool death;
}
