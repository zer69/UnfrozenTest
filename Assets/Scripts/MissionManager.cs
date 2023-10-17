using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class MissionManager : MonoBehaviour
{
    [SerializeField] private List<MissionInfo> missionInfoList = new List<MissionInfo>();
    [SerializeField] private Transform missionBriefingCanvas;

    private void Start()
    {
        for (int i = 1; i <=11; i++)
        {
            
            missionInfoList.Add(Resources.Load<MissionInfo>("MissionInfo/Mission" + i.ToString()));
        }
    }

    public void UpdateMissionBriefing(string missionNumber)
    {
        MissionInfo buffer = missionInfoList[0];
        foreach (MissionInfo info in missionInfoList)
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
