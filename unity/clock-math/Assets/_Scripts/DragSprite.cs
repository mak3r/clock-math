using UnityEngine;

public class DragSprite : MonoBehaviour
{
    private TouchManager touchManager;
    private Vector3 touchPosition;
    private Vector2 basePosition;
    
    private RectTransform clockRowRectTrans;
    public TimeDisplay timeDisplay;
    private Rigidbody2D selectedObject;
    private Vector3 offset;
    private Vector3 mousePosition;
    List<string> symbolsList = new List<string> {"Plus", "Minus", "Equals"};

    private void Awake() {
        touchManager = TouchManager.Instance;
        timeDisplay = TimeDisplay.Instance;
    }
    private void Start()
    {
        // Find and get ClockRow RectTransform component
        GameObject clockRow = GameObject.Find("ClockRow");
        if (clockRow != null)
        {
            clockRowRectTrans = clockRow.GetComponent<RectTransform>();
        }
    }

    private void OnEnable(){
        touchManager.OnSelectItem += DragStart;
        touchManager.OnReleaseItem += DragEnd;
    }

    private void OnDisable() {
        touchManager.OnSelectItem -= DragStart;
        touchManager.OnReleaseItem -= DragEnd;
    }

    private void DragStart(Vector2 position) {
        basePosition = position;
    }

    private void DragEnd(Vector2 position) {
        Reset();
    }

    private void Reset() {

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
                        //Pickup rigidbody
                        if (symbolsList.Contains( targetObject.gameObject.tag)) {
                            selectedObject = targetObject.transform.gameObject.GetComponent<Rigidbody2D>();
                            basePosition = selectedObject.position;
                            offset = selectedObject.transform.position - touchPosition;
                        }
                    }
                }
                //Restore rigidbody to its original position
                if ((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) && selectedObject) {
                    selectedObject.MovePosition(basePosition);
                    selectedObject = null;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("DragSprite.OnTriggerEnter2D()");
        timeDisplay.UpdateSymbol(other.gameObject.tag, gameObject.tag);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (Input.touches.Length > 0) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) {
                timeDisplay.ResetSymbol(other.gameObject.tag, gameObject.tag);
            }
        }
    }
}
