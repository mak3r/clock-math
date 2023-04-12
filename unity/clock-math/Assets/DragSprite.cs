using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragSprite : MonoBehaviour
{
    private RectTransform clockRowRectTrans;
    public TimeDisplayScript timeDisplayScript;
    private Rigidbody2D selectedObject;
    private Vector3 offset;
    private Vector3 mousePosition;
    private Vector3 touchPosition;
    private Vector2 basePosition;
    List<string> symbolsList = new List<string> {"Plus", "Minus", "Equals"};

    private void Start()
    {
        // Find and get ClockRow RectTransform component
        GameObject clockRow = GameObject.Find("ClockRow");
        if (clockRow != null)
        {
            clockRowRectTrans = clockRow.GetComponent<RectTransform>();
        }
    }

    private void FixedUpdate() {
        if (selectedObject) {
            // selectedObject.MovePosition(mousePosition + offset);
            selectedObject.MovePosition(touchPosition + offset);
        }
    }


    void Update()
    {
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            if (touch.fingerId == 0) {
                touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                if (touch.phase == TouchPhase.Began) {
                    Collider2D targetObject = Physics2D.OverlapPoint(touchPosition);
                    if (targetObject)
                    {
                        if (symbolsList.Contains( targetObject.gameObject.tag)) {
                            selectedObject = targetObject.transform.gameObject.GetComponent<Rigidbody2D>();
                            basePosition = selectedObject.position;
                            offset = selectedObject.transform.position - touchPosition;
                        }
                    }
                }
                if (touch.phase == TouchPhase.Ended && selectedObject) {
                    selectedObject.MovePosition(basePosition);
                    selectedObject = null;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("DragSprite.OnTriggerEnter2D()");
        timeDisplayScript.UpdateSymbol(other.gameObject.tag, gameObject.tag);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (Input.touches.Length > 0) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) {
                timeDisplayScript.ResetSymbol(other.gameObject.tag, gameObject.tag);
            }
        }
    }
}
