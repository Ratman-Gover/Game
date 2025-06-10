using UnityEngine;

public class FlashlightToggle : MonoBehaviour
{
    public Light flashlight;
    private bool isOn = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isOn = !isOn;
            flashlight.enabled = isOn;
        }
    }
}
