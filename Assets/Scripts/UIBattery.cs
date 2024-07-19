using UnityEngine;
using UnityEngine.UI;


public class UIBattery : MonoBehaviour
{
    private float currBattery;
    private float lowBattery = 0.2f;
    [SerializeField] private Image fillBatterBar;
    [SerializeField] private Sprite lowBatteryLevel;
    [SerializeField] private Sprite highBatteryLevel;
    private void Update()
    {   
        currBattery = SystemInfo.batteryLevel;
        if (currBattery <= lowBattery)
            fillBatterBar.sprite = lowBatteryLevel;
        else
            fillBatterBar.sprite = highBatteryLevel;

        fillBatterBar.fillAmount = currBattery; 
    }

    public void OnBackButtonClick()
    {
        SceneController.Instance.LoadPreviousScene();
    }
}
