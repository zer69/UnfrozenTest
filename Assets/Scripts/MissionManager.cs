using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class MissionManager : MonoBehaviour
{
    [Header("Mission initialization stuff")]
    [SerializeField] private Transform missionRect;
    [SerializeField] public MissionList missionList;
    [SerializeField] private GameObject missionPrefab;

    [Header("Mission choice stuff")]
    [SerializeField] private Transform missionBriefingCanvas;
    

    private void Start()
    {
        foreach(MissionInfo missionInfo in missionList.missionInfoList)
        {
            GameObject newMission = Instantiate(missionPrefab, missionRect);
            Debug.Log(newMission.GetComponent<RectTransform>().anchoredPosition);
            newMission.GetComponent<RectTransform>().anchoredPosition = missionInfo.missionPosition;
            Debug.Log(newMission.GetComponent<RectTransform>().anchoredPosition);
            Debug.LogWarning(missionInfo.missionNumber);
            newMission.name = missionInfo.missionNumber;
            newMission.GetComponent<Mission>().SetMissionNumber();
        }
    }

    public void UpdateMissionBriefing(string missionNumber)
    {
        MissionInfo buffer = missionList.missionInfoList[0];
        foreach (MissionInfo info in missionList.missionInfoList)
        {
            if (info.missionNumber == missionNumber)
            {
                buffer = info;
                break;
            }
        }
            
                
        if (!missionBriefingCanvas.gameObject.activeSelf)
            missionBriefingCanvas.gameObject.SetActive(true);
        missionBriefingCanvas.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = buffer.missionName;
        //missionBriefingCanvas.GetChild(0).GetChild(1).GetComponent<Image>().sprite;
        missionBriefingCanvas.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text = buffer.briefingText;

    }
}
