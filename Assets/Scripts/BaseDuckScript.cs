using UnityEngine;

public class BaseDuckScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool isEnemy;
    private bool hasCrown = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTeam(bool isOnEnemyTeam){
        isEnemy = isOnEnemyTeam;
    }

    public void giveCrown(){
        hasCrown = true;
    }
}
