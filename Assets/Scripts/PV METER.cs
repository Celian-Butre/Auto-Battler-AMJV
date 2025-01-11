using UnityEngine;
using UnityEngine.UI;


public class PVMETER : MonoBehaviour
{
    [SerializeField] Image PVMeter;
    private float PVm;
    public Gradient colorGradient;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PVm = 0.2f;

    }

    // Update is called once per frame
    public void TakeDamage(float d)
    {
        PVm = PVm - d;
    }
    void Update()
    {

        var gradient = new Gradient();

        var colors = new GradientColorKey[2];
        colors[0] = new GradientColorKey(Color.red, 0.0f);
        colors[1] = new GradientColorKey(Color.green, 1.0f);

        var alphas = new GradientAlphaKey[2];
        alphas[0] = new GradientAlphaKey(1.0f, 0.0f);
        alphas[1] = new GradientAlphaKey(1.0f, 0.0f);

        gradient.SetKeys(colors, alphas);

        PVMeter.fillAmount = PVm;
        PVMeter.color = colorGradient.Evaluate(PVm);
    }
}
