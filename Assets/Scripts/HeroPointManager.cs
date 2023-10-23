using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPointManager : MonoBehaviour
{
    
    public void AddHeroPoints(HeroPointPair heroPointPair)
    {
        Hero heroToAddPoints = transform.GetChild((int)heroPointPair.heroName).GetComponent<Hero>();
        if (heroToAddPoints.gameObject.activeSelf)
        {
            heroToAddPoints.points += heroPointPair.points;
            heroToAddPoints.UpdatePointsText();
        }
            
        else
            if (heroPointPair.points < 0)
                heroToAddPoints.points += heroPointPair.points;
        
    }

    public void UnlockHero(int heroName)
    {
        transform.GetChild(heroName).gameObject.SetActive(true);
    }

}

public class HeroPointPair
{
    public HeroName heroName;
    public int points;

    public HeroPointPair(HeroName hn, int p)
    {
        heroName = hn;
        points = p;
    }
}
