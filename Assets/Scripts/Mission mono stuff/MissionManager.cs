using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;
using System;

public class MissionManager : MonoBehaviour
{
    [Header("Mission initialization stuff")]
    [SerializeField] private Transform missionRect;
    [SerializeField] public MissionList missionList;
    [SerializeField] private GameObject missionPrefab;
    [SerializeField] private GameObject dualMissionPrefab;

    [Header("Mission choice stuff")]
    [SerializeField] private Transform missionBriefingCanvas;
    [SerializeField] private string currentMissionNumber;
    private Button startMissionButton;
    private bool heroIsPicked = false;
    [SerializeField] private HeroName pickedHeroName;

    [Header("Mission info stuff")]
    [SerializeField] private Transform missionInfoCanvas;

    [Header("Mission completion stuff")]
    [SerializeField] private heroPoint_GameEvent heroPointGain;
    [SerializeField] private int_GameEvent unlockHero;
    private bool[] unlockedCharacters = new bool[(int)HeroName.HeroCount];


    private void Start()
    {
        
        //Create missions in world
        foreach(MissionInfo missionInfo in missionList.missionInfoList)
        {
            GameObject newMission = Instantiate(missionPrefab, missionRect);
            newMission.GetComponent<RectTransform>().anchoredPosition = missionInfo.missionPosition;
            newMission.name = missionInfo.missionNumber;
            newMission.GetComponent<Mission>().SetMissionNumber();

            if (newMission.transform.GetSiblingIndex() == 0)
                newMission.GetComponent<Mission>().SetMissionStatus(MissionStatus.Unlocked);
        }

        foreach(DualMissionInfo dualMissionInfo in missionList.dualMissionInfoList)
        {
            GameObject newMission = Instantiate(dualMissionPrefab, missionRect);
            newMission.GetComponent<RectTransform>().anchoredPosition = dualMissionInfo.missionPosition;
            newMission.name = dualMissionInfo.missionGlobalNumber;
            
            newMission.transform.GetChild(0).name = dualMissionInfo.mission1.missionNumber;
            newMission.transform.GetChild(0).GetComponent<Mission>().SetMissionNumber();

            newMission.transform.GetChild(1).name = dualMissionInfo.mission2.missionNumber;
            newMission.transform.GetChild(1).GetComponent<Mission>().SetMissionNumber();

        }

        startMissionButton = missionBriefingCanvas.GetChild(0).GetChild(3).GetComponent<Button>();
        ShowNextMissions("1");
        for (int i = 0; i < (int)HeroName.HeroCount; i++)
            unlockedCharacters[i] = false;
        unlockedCharacters[0] = true;
    }

    private void ShowNextMissions(string missionNumber)
    {
        MissionInfo buffer = FindMissionInfo(missionNumber);

        if (buffer.missionsToUnlock.Count > 0)
        {
            foreach (string mn in buffer.missionsToUnlock)
            {
                SetNewMissionStatus(mn, MissionStatus.Locked, MissionStatus.Shown);
            }
        }
    }

    

    public void UpdateMissionBriefing(string missionNumber)
    {

        currentMissionNumber = missionNumber;
        MissionInfo buffer = FindMissionInfo(missionNumber);
        if (!missionBriefingCanvas.gameObject.activeSelf)
            missionBriefingCanvas.gameObject.SetActive(true);

        missionBriefingCanvas.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = buffer.missionName;
        missionBriefingCanvas.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text = buffer.briefingText;

        startMissionButton.interactable = heroIsPicked;

    }

    public void UpdateMissionText()
    {
        missionBriefingCanvas.gameObject.SetActive(false);
        MissionInfo buffer = FindMissionInfo(currentMissionNumber);

        missionInfoCanvas.gameObject.SetActive(true);
        missionInfoCanvas.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = buffer.missionName;

        if (currentMissionNumber == "9")
        {
            string newAllyText = buffer.allyText + (unlockedCharacters[(int)HeroName.Crow] ? "������" : "����");
            string newEnemyText = buffer.enemyText + (unlockedCharacters[(int)HeroName.Crow] ? "����" : "������");

            missionInfoCanvas.GetChild(0).GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = newAllyText;
            missionInfoCanvas.GetChild(0).GetChild(0).GetChild(3).GetComponent<TMP_Text>().text = newEnemyText;
        }
        else
        {
            missionInfoCanvas.GetChild(0).GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = buffer.allyText;
            missionInfoCanvas.GetChild(0).GetChild(0).GetChild(3).GetComponent<TMP_Text>().text = buffer.enemyText;
        }
        //missionInfoCanvas.GetChild(0).GetChild(0).GetChild(2).GetComponent<Image>().sprite = set sprite here
        missionInfoCanvas.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = buffer.missionText;
    }

    private MissionInfo FindMissionInfo(string missionNumber)
    {
        foreach (MissionInfo info in missionList.missionInfoList)
        {
            if (info.missionNumber == missionNumber)
            {
                return info;
            }
        }
        foreach (DualMissionInfo info in missionList.dualMissionInfoList)
        {
            if (info.mission1.missionNumber == missionNumber)
                return info.mission1;
            if (info.mission2.missionNumber == missionNumber)
                return info.mission2;
        }
        return null;
    }

    public void FinishMission()
    {
        MissionInfo buffer = FindMissionInfo(currentMissionNumber);

        Transform missionBuffer = missionRect.Find(currentMissionNumber);
        if (missionBuffer == null)
        {
            foreach (Transform mission in missionRect)
            {
                if (mission.tag == "DualMission")
                {
                    Transform dualMissionBuffer = mission.Find(currentMissionNumber);
                    dualMissionBuffer.GetComponent<Mission>().SetMissionStatus(MissionStatus.Completed);
                    dualMissionBuffer.parent.GetChild(dualMissionBuffer.GetSiblingIndex() == 0 ? 1 : 0).GetComponent<Mission>().SetMissionStatus(MissionStatus.LockedTemp);
                    LockNextMissions(dualMissionBuffer.parent.GetChild(dualMissionBuffer.GetSiblingIndex() == 0 ? 1 : 0).name);
                }
            }
        }
        else
            missionBuffer.GetComponent<Mission>().SetMissionStatus(MissionStatus.Completed);
    
        missionInfoCanvas.gameObject.SetActive(false);
        heroIsPicked = false;

        heroPointGain.Raise(new HeroPointPair(pickedHeroName, buffer.globalPointBonus));
        for (int i = 0; i < (int) HeroName.HeroCount; i++)
        {
            heroPointGain.Raise(new HeroPointPair((HeroName)i, buffer.localPointBonus[i]));
        }

        if (buffer.heroUnlockList.Count > 0)
        {
            foreach (HeroName hn in buffer.heroUnlockList)
            {
                unlockHero.Raise((int)hn);
                unlockedCharacters[(int)hn] = true;
            }
        }
            
        foreach (string mn in buffer.missionsToUnlock)
        {
                
            if (missionRect.Find(mn).GetComponent<Mission>() == null)
            {
                missionRect.Find(mn).GetChild(0).GetComponent<Mission>().SetMissionStatus(MissionStatus.Unlocked);
                ShowNextMissions(missionRect.Find(mn).GetChild(0).name);
                missionRect.Find(mn).GetChild(1).GetComponent<Mission>().SetMissionStatus(MissionStatus.Unlocked);
                ShowNextMissions(missionRect.Find(mn).GetChild(1).name);
            }
            else
            {   
                if (CanUnlockMission(FindMissionInfo(mn)))
                {
                    missionRect.Find(mn).GetComponent<Mission>().SetMissionStatus(MissionStatus.Unlocked);
                    ShowNextMissions(mn);
                }
                
            }
        }   
    }

    private bool CanUnlockMission(MissionInfo missionInfo)
    {
        foreach (string prerequities in missionInfo.previousMissionList)
        {
            if (prerequities.Length > 2)
            {
                if (missionRect.Find(prerequities.Substring(0, prerequities.Length - 2)).Find(prerequities).GetComponent<Mission>().GetMissionStatus() < MissionStatus.Completed)
                {
                    return false;
                }
                
            }
            else
                if (missionRect.Find(prerequities).GetComponent<Mission>().GetMissionStatus() < MissionStatus.Completed)
                    return false;
        }

        return true;
    }

    private void LockNextMissions(string name)
    {
        MissionInfo buffer = FindMissionInfo(name);
        foreach (string nextMission in buffer.missionsToUnlock)
        {
            missionRect.Find(nextMission).GetComponent<Mission>().SetMissionStatus(MissionStatus.LockedTemp);
        }
    }

    private void SetNewMissionStatus(string missionNumber, MissionStatus statusToCheck, MissionStatus statusToSet)
    {
        if (missionRect.Find(missionNumber).GetComponent<Mission>() == null)
        {
            if (missionRect.Find(missionNumber).GetChild(0).GetComponent<Mission>().GetMissionStatus() == statusToCheck)
            {
                missionRect.Find(missionNumber).GetChild(0).GetComponent<Mission>().SetMissionStatus(statusToSet);
                missionRect.Find(missionNumber).GetChild(1).GetComponent<Mission>().SetMissionStatus(statusToSet);
            }
        }
        else
            if (missionRect.Find(missionNumber).GetComponent<Mission>().GetMissionStatus() == statusToCheck)
            missionRect.Find(missionNumber).GetComponent<Mission>().SetMissionStatus(statusToSet);
    }

    public void SetPickedHero(int heroName)
    {
        if (missionBriefingCanvas.gameObject.activeSelf)
        {
            heroIsPicked = true;
            pickedHeroName = (HeroName)heroName;
            startMissionButton.interactable = heroIsPicked;
        }
        
    }
}
