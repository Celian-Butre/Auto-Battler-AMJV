using UnityEngine;

public class SpawnBaseDuck : MonoBehaviour
{
    [SerializeField] GameObject baseDuckPrefab;
    [SerializeField] GameObject theCamera;
    LayerMask layerMask;
    private bool didHit;

    private RaycastHit hit;

    private Vector3 directionToMouse;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        layerMask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        didHit = (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask));

        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Left mouse button clicked!");
            if(didHit){
                Instantiate(baseDuckPrefab, (hit.point + new Vector3(0f, baseDuckPrefab.GetComponent<Renderer>().bounds.size.y / 2f ,0f)), Quaternion.identity);
            }
        }
    }
}
