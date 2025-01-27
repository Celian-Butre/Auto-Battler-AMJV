using UnityEngine;
using System.Collections.Generic;
public class UnlockedLevelsManager : MonoBehaviour
{
    [SerializeField] private bool mainMenuLevel;
    static public List<int> unlockedLevels = null; //0 = locked, 1 = unlocked not beaten, 2 = beaten easy, 3 = beaten medium, 4 = beaten hard
    private int howManyLevels = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (mainMenuLevel)
        {
            if (unlockedLevels == null){
                unlockedLevels = new List<int>();
                for (int i = 0; i < howManyLevels; i++)
                {
                    unlockedLevels.Add(0);
                }

                unlockedLevels[0] = 4;
                unlockedLevels[1] = 3;
                unlockedLevels[2] = 2;
                unlockedLevels[3] = 1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void beatCurrentLevel(int currentLevel, int difficulty) //difficulty = 1,2,3
    {
        if (unlockedLevels != null){
            if (!mainMenuLevel)
            {
                if (currentLevel != howManyLevels - 1)
                {
                    if (unlockedLevels[currentLevel] == 0)
                    {
                        unlockedLevels[currentLevel] = 1;
                    }
                }

                if (unlockedLevels[currentLevel - 1] < difficulty + 1)
                {
                    unlockedLevels[currentLevel - 1] = difficulty + 1;
                }
            }
        }
    }

    public int getLevelStatus(int level)
    {
        if (unlockedLevels != null)
        {
            return unlockedLevels[level - 1];
        }
        return 0;
    }

    public List<int> getAllStatuses()
    {
        return unlockedLevels;
    }
}
