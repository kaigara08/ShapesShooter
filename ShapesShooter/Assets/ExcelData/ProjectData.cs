using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class ProjectData : ScriptableObject
{
	public List<EnemyDataEntity> EnemyData; // Replace 'EntityType' to an actual type that is serializable.
}
