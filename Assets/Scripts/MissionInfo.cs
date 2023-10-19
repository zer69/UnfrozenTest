using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MissionType
{
    Single,
    Dual
}

[System.Serializable]
public class MissionInfo
{

    [Header("Mission map info")]
    public string missionNumber;
    public Vector3 missionPosition;
    public List<string> previousMissionList;
    public List<string> missionsToUnlock;
    [Header("Mission Briefing")]
    public string missionName;
    public string briefingText;
    [Header("Mission Info")]
    public string missionText;
    public string allyText;
    public string enemyText;
    

    public MissionInfo()
    {

    }
}
