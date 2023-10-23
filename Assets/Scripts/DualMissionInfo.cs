using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DualMissionInfo
{
    [Header("Map global info")]
    public string missionGlobalNumber;
    public Vector3 missionPosition;
    [Header("Mission 1")]
    public MissionInfo mission1;

    [Header("Mission 2")]
    public MissionInfo mission2;

    
    
    public DualMissionInfo()
    {

    }
}
