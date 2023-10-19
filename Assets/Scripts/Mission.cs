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
        
    }

    public void SetMissionNumber()
    {
        transform.Find("MissionNumber").GetComponent<TMP_Text>().text = gameObject.name;
    }

    public void ClickAMission()
    {
        missionPicked.Raise(gameObject.name);
        missionStatus = MissionStatus.Active;
        Debug.Log(GetComponent<RectTransform>().localPosition);
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
