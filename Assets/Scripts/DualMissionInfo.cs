using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DualMissionInfo
{
    [Header("Mission 1 map info")]
    public string mission1Number;
    [Header("Mission 1 Briefing")]
    public string mission1Name;
    public string briefing1Text;
    [Header("Mission 1 Info")]
    public string mission1Text;
    public string ally1Text;
    public string enemy1Text;

    [Header("Mission 2 map info")]
    public string mission2Number;
    [Header("Mission 2 Briefing")]
    public string mission2Name;
    public string briefing2Text;
    [Header("Mission 2 Info")]
    public string mission2Text;
    public string ally2Text;
    public string enemy2Text;

    [Header("Map info")]
    public List<string> previousMissionList;
    public List<string> missionsToUnlock;
    public Vector3 missionPosition;
    public DualMissionInfo()
    {

    }
}
