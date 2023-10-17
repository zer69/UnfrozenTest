using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum MissionStatus
{
    None,
    Unlocked,
    Locked,
    Active,
    Completed
}

public class Mission : MonoBehaviour
{
    [SerializeField] private string missionNumber;
    [SerializeField] private string_GameEvent missionPicked;
    [SerializeField] private MissionStatus missionStatus;
    private MissionStatus oldStatus;

    private void Update()
    {
        if (oldStatus != missionStatus)
            OnMissionStatusChange();
        oldStatus = missionStatus;
    }

    private void Start()
    {
        transform.Find("MissionNumber").GetComponent<TMP_Text>().text = missionNumber;
    }

    public void ClickAMission()
    {
        missionPicked.Raise(missionNumber);
        missionStatus = MissionStatus.Active;
    }

    private void OnMissionStatusChange()
    {
        switch (missionStatus)
        {
            case MissionStatus.Unlocked:
                break;
            case MissionStatus.Locked:
                break;
            case MissionStatus.Active:
                break;
            case MissionStatus.Completed:
                break;
        }
    }
}
