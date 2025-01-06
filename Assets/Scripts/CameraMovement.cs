using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float cameraMovementSpeed;
    [SerializeField] float cameraRotationSpeed;
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
            Debug.Log("avancer");
            transform.Translate(Vector3.forward * Time.deltaTime * cameraMovementSpeed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Debug.Log("reculer");
            transform.Translate(-Vector3.forward * Time.deltaTime * cameraMovementSpeed);
        }
        if (Input.GetKey("w"))
        {
            Debug.Log("lever la tête");
            transform.Rotate(-cameraRotationSpeed * Time.deltaTime, 0.0f, 0.0f, Space.Self);
        }
        else if (Input.GetKey("s"))
        {
            Debug.Log("baisser la tête");
            transform.Rotate(cameraRotationSpeed * Time.deltaTime, 0.0f, 0.0f, Space.Self);
        }
        else if (Input.GetKey("a"))
        {
            Debug.Log("tourner la tête à gauche");
            transform.Rotate(0.0f, -cameraRotationSpeed * Time.deltaTime, 0.0f, Space.Self);
        }
        else if (Input.GetKey("d"))
        {
            Debug.Log("tourner la tête à droite");
            transform.Rotate(0.0f, cameraRotationSpeed * Time.deltaTime, 0.0f, Space.Self);
        }
        Debug.Log(transform.rotation);
        
        /*
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("Moving Left");
            transform.localPosition += new Vector3(-lateralSpeed, 0.0f, 0.0f);
        }
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("Moving Right");
            transform.localPosition += new Vector3(lateralSpeed, 0.0f, 0.0f);
        }
        if (Input.GetKey("w") || Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("Moving Up");
            transform.localPosition += new Vector3(-lateralSpeed, 0.0f, 0.0f);
        }
        if (Input.GetKey("s") || Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("Moving Down");
            transform.localPosition += new Vector3(lateralSpeed, 0.0f, 0.0f);
        }
        */
    }
}
