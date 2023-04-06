using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{

    [SerializeField] private Image clockImage;
    [SerializeField] private Color clockColor;

    // Start is called before the first frame update
    private void Start()
    {
        clockColor.a = 1;
    }


    // Update is called once per frame
    private void Update()
    {
        clockImage.color = clockColor;
    }

}
