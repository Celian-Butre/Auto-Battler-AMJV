using UnityEngine;
using System.Collections;

public class Daffy : MonoBehaviour
{
    [SerializeField] private GameObject Sword;
    float duration = 3.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //à supprimer
        float SPEED = 10.0f;
        float ROT = 100.0f;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("avancer");
            transform.Translate(Vector3.forward * Time.deltaTime * SPEED);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Debug.Log("reculer");
            transform.Translate(-Vector3.forward * Time.deltaTime * SPEED);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("tourner la tête à gauche");
            transform.Rotate(0.0f, -ROT * Time.deltaTime, 0.0f, Space.Self);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("tourner la tête à droite");
            transform.Rotate(0.0f, ROT * Time.deltaTime, 0.0f, Space.Self);
        }
        if (Input.GetKey(KeyCode.R))
        {
            Debug.Log("Attaque");
            StartCoroutine(Rotate360());
        }
    }

    IEnumerator Rotate360()
    {

        Quaternion startRot = Sword.transform.rotation;
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            Sword.transform.rotation = startRot * Quaternion.AngleAxis(t / duration * 360f, Vector3.right); 
            yield return null;
        }
        Sword.transform.rotation=Quaternion.identity;
        t = 0;
    }
}

    

