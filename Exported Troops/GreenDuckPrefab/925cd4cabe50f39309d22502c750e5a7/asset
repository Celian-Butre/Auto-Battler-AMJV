using UnityEngine;

public class CameraMovementFlat : MonoBehaviour
{
    [SerializeField] float cameraMovementSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        //avancer reculer
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //Debug.Log("avancer");
            transform.Translate(Vector3.forward * Time.deltaTime * cameraMovementSpeed, Space.World);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //Debug.Log("reculer");
            transform.Translate(-Vector3.forward * Time.deltaTime * cameraMovementSpeed, Space.World);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Debug.Log("gauche");
            transform.Translate(-Vector3.right * Time.deltaTime * cameraMovementSpeed, Space.World);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Debug.Log("droite");
            transform.Translate(Vector3.right * Time.deltaTime * cameraMovementSpeed, Space.World);
        }
        
    }
}
