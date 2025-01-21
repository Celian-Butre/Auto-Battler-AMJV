using UnityEngine;

public class Lazer : MonoBehaviour
{
    private float i = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        i += 1;
        if (i>0)
            {
            Destroy(gameObject);
            }
    }
}
