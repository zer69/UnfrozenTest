using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MissionList : ScriptableObject
{
    public List<MissionInfo> missionInfoList;
    public List<DualMissionInfo> dualMissionInfoList;
}
