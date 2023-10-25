using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum MissionStatus
{
    Locked,
    Unlocked,
    Active,
    Shown,
    Completed,
    LockedTemp
}

public class Mission : MonoBehaviour
{
    [SerializeField] private string_GameEvent missionPicked;
    [SerializeField] private MissionStatus missionStatus = MissionStatus.Locked;
    private MissionStatus oldStatus;

    private void Update()
    {
        if (oldStatus != missionStatus)
            OnMissionStatusChange();
        oldStatus = missionStatus;
    }

    public void SetMissionNumber()
    {
        transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = gameObject.name;
        missionStatus = MissionStatus.Locked;
        if (transform.parent.tag == "DualMission")
            transform.parent.GetComponent<Image>().enabled = false;
        OnMissionStatusChange();
    }

    public void SetMissionStatus(MissionStatus ms)
    {
        missionStatus = ms;
    }

    public MissionStatus GetMissionStatus()
    {
        return missionStatus;
    }

    public void ClickAMission()
    {
        missionPicked.Raise(gameObject.name);
        missionStatus = MissionStatus.Active;
    }

    private void OnMissionStatusChange()
    {
        switch (missionStatus)
        {
            case MissionStatus.Locked:
                SetImageActive(false);
                break;

            case MissionStatus.Unlocked:
                SetImageActive(true);
                GetComponent<Button>().interactable = true;
                break;

            case MissionStatus.Active:
                break;

            case MissionStatus.Shown:
            case MissionStatus.LockedTemp:
                SetImageActive(true);
                GetComponent<Button>().interactable = false;
                break;

            case MissionStatus.Completed:
                GetComponent<Button>().interactable = true;
                transform.GetChild(0).GetComponent<Image>().color = new Color(0.2f, 0.9f, 0.1f, 1f);
                GetComponent<Button>().enabled = false;
                break;
        }
    }

    private void SetImageActive(bool state)
    {
        if (transform.parent.tag == "DualMission")
        {
            transform.parent.GetComponent<Image>().enabled = state;
        }
        transform.GetChild(0).gameObject.SetActive(state);
    }
}
