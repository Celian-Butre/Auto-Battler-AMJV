using UnityEngine;

public class CanvaUIListener : MonoBehaviour
{
    private bool UiAllow;
    //False = Empeche le spawn d'ennemi quand on modifie ses propriété sur le canva. True si on est pas sur le canva pour laisser spawn
    private void OnMouseOver()
    {
        UiAllow = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!(UiAllow)) { }
        else { UiAllow = true; }
    }

    public bool getMouseOverUI()
    {
        return UiAllow;
    }
}
