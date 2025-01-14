using UnityEngine;
using System.Collections.Generic;
public class UnlockedLevelsManager : MonoBehaviour
{
    

    [SerializeField] private bool mainMenuLevel;
    static public List<int> unlockedLevels; //0 = locked, 1 = unlocked not beaten, 2 = beaten easy, 3 = beaten medium, 4 = beaten hard
    private int howManyLevels = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (mainMenuLevel)
        {
            unlockedLevels = new List<int>();
            for (int i = 0; i < howManyLevels; i++)
            {
                unlockedLevels.Add(0);
            }

            unlockedLevels[0] = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void beatCurrentLevel(int currentLevel, int difficulty) //difficulty = 1,2,3
    {
        if (!mainMenuLevel)
        {
            if (currentLevel != howManyLevels - 1)
            {
                if (unlockedLevels[currentLevel + 1] == 0)
                {
                    unlockedLevels[currentLevel] = 1;
                }
            }

            if (unlockedLevels[currentLevel] < difficulty + 1)
            {
                unlockedLevels[currentLevel] = difficulty + 1;
            }
        }
    }

    public int getLevelStatus(int level)
    {
        return unlockedLevels[level];
    }
}
