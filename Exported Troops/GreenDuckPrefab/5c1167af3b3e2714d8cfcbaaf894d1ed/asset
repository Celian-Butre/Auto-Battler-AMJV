using UnityEngine;

public class CameraFly : MonoBehaviour
{
    [SerializeField] float mainSpeed; //regular speed
    [SerializeField] float shiftAdd; //multiplied by how long shift is held.  Basically running
    [SerializeField] float maxShift; //Maximum speed when holdin gshift
    [SerializeField] float camVelocity; //How sensitive it with mouse
    private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
    private float totalRun = 1.0f;

    void Update()
    {
        Vector3 r = GetRotInput();
        lastMouse = new Vector3(transform.eulerAngles.x + r.x, transform.eulerAngles.y + r.y, 0);
        //transform.eulerAngles = lastMouse;
        transform.rotation = Quaternion.Euler(lastMouse);
        //Mouse  camera angle done.  

        //Keyboard commands
        //float f = 0.0f;
        Vector3 p = GetBaseInput();
        if (p.sqrMagnitude > 0)
        { // only move while a direction key is pressed
            if (Input.GetKey(KeyCode.LeftShift))
            {
                totalRun += Time.deltaTime;
                p = p * totalRun * shiftAdd;
                p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
                p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
                p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
            }
            else
            {
                totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
                p = p * mainSpeed;
            }

            p = p * Time.deltaTime;
            Vector3 newPosition = transform.position;
            if (Input.GetKey(KeyCode.Space))
            { //If player wants to move on X and Z axis only
                transform.Translate(p);
                newPosition.x = transform.position.x;
                newPosition.z = transform.position.z;
                transform.position = newPosition;
            }
            else
            {
                transform.Translate(p);
            }
        }
        Debug.Log(r);
        Debug.Log(p);
    }

    private Vector3 GetBaseInput()
    { //returns the basic values, if it's 0 than it's not active.
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.UpArrow))
        {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }
        return p_Velocity;
    }

    private Vector3 GetRotInput()
    { //returns the basic values, if it's 0 than it's not active.
        Vector3 r_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.Z))
        {
            r_Velocity += new Vector3(0, 1,0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            r_Velocity += new Vector3(0, -1, 0);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            r_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            r_Velocity += new Vector3(1, 0, 0);
        }
        return camVelocity*r_Velocity;
    }
}

