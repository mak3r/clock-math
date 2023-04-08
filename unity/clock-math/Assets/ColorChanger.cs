using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{

    [SerializeField] private Image clockImage;
    [SerializeField] private Color clockColor;
    private Color defaultColor = Color.green;
    private Color selectedColor = Color.red;
    private Color currentColor;
    private Color hoverColor = Color.cyan;
    private bool isTimeStopped = false;

    // Start is called before the first frame update
    private void Start()
    {

        clockColor.a = 1;
        clockImage.color = defaultColor;
        currentColor = clockImage.color;
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
        currentColor = clockImage.color;
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

    public void OnSelect() {
        Debug.Log("ColorChanger.onSelect()");
    }

    public void OnDeselect() {
        Debug.Log("ColorChanger.OnDeselect()");
    }

    public void OnPointerEnter() {
        Debug.Log("ColorChanger.OnPointerEnter()");
        clockImage.color = hoverColor;
    }

    public void OnPointerExit() {
        Debug.Log("ColorChanger.OnPointerExit()");
        clockImage.color = currentColor;
    }
}
