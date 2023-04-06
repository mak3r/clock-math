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
    private Vector2 basePosition;

    private void Start()
    {
        // Find and get ClockRow RectTransform component
        GameObject clockRow = GameObject.Find("ClockRow");
        if (clockRow != null)
        {
            clockRowRectTrans = clockRow.GetComponent<RectTransform>();
        }
    }

    private void Awake() {

    }

    private void OnMouseDown()
    {
        Debug.Log("Mouse Down ********* ");
        selectedObject = GetComponent<Rigidbody2D>();
        basePosition = selectedObject.position;
        selectedObject = null;
    }

    private void OnMouseDrag()
    {

    }

    private void FixedUpdate() {
        if (selectedObject) {
            selectedObject.MovePosition(mousePosition + offset);
        }
    }

    private void OnMouseUp()
    {
        Debug.Log("Mouse Up ********* ");
        selectedObject.MovePosition(basePosition);
    }

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject)
            {
                selectedObject = targetObject.transform.gameObject.GetComponent<Rigidbody2D>();
                offset = selectedObject.transform.position - mousePosition;
            }
        }
        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            selectedObject = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("DragSprite.OnTriggerEnter2D()");
        if (other.CompareTag("hourones"))
        {
            timeDisplayScript.Activate("mathA", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("DragSprite.OnCollisionEnter2D()");
        if (collision.gameObject.CompareTag("hourones"))
        {
            Debug.Log("Collision detected with static object");
            // Add code here to handle the collision
        }
    }

}
