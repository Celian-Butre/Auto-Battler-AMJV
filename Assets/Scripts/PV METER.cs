using UnityEngine;
using UnityEngine.UI;

public class PVMETER : MonoBehaviour
{
    [SerializeField] Image PVMeter;
    private float PVm;
    private float PVMax;
    public Gradient colorGradient;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("JE SUIS LA");
        var gradient = new Gradient();

        var colors = new GradientColorKey[2];
        colors[0] = new GradientColorKey(Color.red, 0.0f);
        colors[1] = new GradientColorKey(Color.green, 1.0f);

        var alphas = new GradientAlphaKey[2];
        alphas[0] = new GradientAlphaKey(1.0f, 0.0f);
        alphas[1] = new GradientAlphaKey(1.0f, 0.0f);

        gradient.SetKeys(colors, alphas);

        PVm = GetComponent<BaseDuckScript>().getHealth() / GetComponent<BaseDuckScript>().getBaseHealth();
        Debug.Log(PVm);
        PVMeter.fillAmount = PVm;
        PVMeter.color = colorGradient.Evaluate(PVm);
    }
}
