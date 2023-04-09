using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondsButton : MonoBehaviour
{

    [SerializeField] private Image clockImage;
    [SerializeField] private Color clockColor;
    [SerializeField] private Button button;
    [SerializeField] private TimeDisplayScript timeDisplayScript;
    private Color defaultColor = Color.green;
    private Color selectedColor = Color.red;
    private Color hoverColor = Color.cyan;
    private bool isTimeStopped = false;

    // Start is called before the first frame update
    private void Start()
    {

        clockColor.a = 1;
        clockImage.color = defaultColor;
        clockColor = clockImage.color;
    }


    // Update is called once per frame
    public void OnClick()
    {
        Debug.Log("ColorChanger.OnClick()");
        if (isTimeStopped) {
            clockImage.color = defaultColor;
            isTimeStopped = false;
        } else {
            clockImage.color = selectedColor;
            isTimeStopped = true;
        }
        clockColor = clockImage.color;
        timeDisplayScript.StopTime(isTimeStopped);
    }

    private void Update()
    {
        if (!isTimeStopped) {
            int seconds = System.DateTime.Now.Second;
            //float timeInSeconds = Time.time % 60f; // Get current time in seconds
            float fillAmount = seconds / 60f; // Convert time to fill amount (0 to 1)
            clockImage.fillAmount = fillAmount; // Set the fill amount of the circle
        }
    }

    public bool EmulateClick() {
        button.onClick.Invoke();
        return isTimeStopped;
    }

}
