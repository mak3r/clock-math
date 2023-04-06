using UnityEngine;
using UnityEngine.UI;

public class TimeDisplayScript : MonoBehaviour
{
    public Sprite[] numberSprites; // An array of the sprite images for the numbers 0-9 and the colon symbol
    private bool once = true;
    private RectTransform clockRowRectTrans;
    private RectTransform symbolRowRectTrans;
    private HorizontalLayoutGroup layoutGroup;


    private void Start()
    {
        //DumpSpriteArray();
        Activate("mathA",false);

    }

    private void Awake()
    {
        GameObject clockRow = GameObject.Find("ClockRow");
        layoutGroup = clockRow.GetComponent<HorizontalLayoutGroup>();
        clockRowRectTrans = clockRow.GetComponent<RectTransform>();
    }

    private void Update()
    {
        // Get the current time
        int hours = System.DateTime.Now.Hour;
        int minutes = System.DateTime.Now.Minute;
        int seconds = System.DateTime.Now.Second;

        // Update the sprites to display the current time
        if (hours/10 == 0) {
            Activate("hourtens", false);
        } else {
            SpriteRenderer hourtens = GameObject.Find("hourtens").GetComponent<SpriteRenderer>();
            if(hourtens) {
                hourtens.sprite = numberSprites[hours/10];
                hourtens.enabled = true;
            }
        }

        if (GameObject.Find("mathA")) {
            SpriteRenderer mathA = GameObject.Find("mathA").GetComponent<SpriteRenderer>();
            if(mathA) {
                mathA.sprite = numberSprites[12]; // The sprite at index 1 is a symbol
            }
        }

        SpriteRenderer hourones = GameObject.Find("hourones").GetComponent<SpriteRenderer>();
        if(hourones) {
            hourones.sprite = numberSprites[hours % 10]; // The sprite at index 1 corresponds to the ones position of the hour clock
        }

        SpriteRenderer separator = GameObject.Find("separator").GetComponent<SpriteRenderer>();
        if(separator) {
            separator.sprite = numberSprites[10]; // The sprite at index 2 corresponds to the colon symbol
            separator.enabled = seconds % 2 == 0;
        }

        SpriteRenderer minutestens = GameObject.Find("minutestens").GetComponent<SpriteRenderer>();
        if(hourones) {
            minutestens.sprite = numberSprites[minutes / 10]; // The sprite at index 3 corresponds to the tens position of the minutes value
        }
        
        SpriteRenderer minutesones = GameObject.Find("minutesones").GetComponent<SpriteRenderer>();
        if(minutesones) {
            minutesones.sprite = numberSprites[minutes % 10]; // The sprite at index 4 corresponds to the ones position of the minutes value
        }

        if (once) {
            Debug.Log("hours: " + hours);
            Debug.Log("minutes: " + minutes);
            Debug.Log("htens: " + (hours/10));
            Debug.Log("hones: " + (hours%10));
            DumpClockActive();
            once = false;
        }

    }

    private void ResetLayout() {
        // Calculate the size of the LayoutGroup
        int childCount = clockRowRectTrans.childCount;
        float totalWidth = layoutGroup.GetComponent<RectTransform>().rect.width;
        Debug.Log("ChildCount: " + childCount);
        Debug.Log("LayoutGroup rect: " + layoutGroup.GetComponent<RectTransform>().rect);
        Debug.Log("LayoutGroup width: " + totalWidth);
        if (childCount == 6) {
            layoutGroup.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 8f);
        } else if (childCount == 7) {
            layoutGroup.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 10f);
        } else {
            layoutGroup.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 6f);
        }
        // float cellWidth = (totalWidth / childCount);

        // // Set the cell size of the layout group.
        // layoutGroup.cellSize = new Vector2(cellWidth, cellSize.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("TimeDisplayScript.OnTriggerEnter2D()");
        // Check if the other object is the moving object
        if (other.CompareTag("Plus"))
        {
            Activate("mathA", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("TimeDisplayScript.OnCollisionEnter2D()");
        if (collision.gameObject.CompareTag("Plus"))
        {
            Debug.Log("Collision detected with static object");
            // Add code here to handle the collision
        }
    }

    public void Activate(string name, bool active) {
        // Find and get ClockRow RectTransform component
        GameObject clockRow = GameObject.Find("ClockRow");
        if (clockRow != null)
        {
            GameObject obj = clockRowRectTrans.Find(name).gameObject;
            obj.SetActive(active);
        }
        ResetLayout();
    }

    private int CountClockActive() {
        GameObject clockRow = GameObject.Find("ClockRow");
        return clockRow.GetComponentsInChildren<SpriteRenderer>().Length;
    }

    private void DumpSpriteArray() {
        Debug.Log("sprite array:" + numberSprites.Length);
        int i = 0;
        while (i < numberSprites.Length)
        {
            Debug.Log("Sprite name: " + numberSprites[i].name + ", Index: " + i);
            //numberSprites[i].sprite = numberImages[GetDigit(i)];
            i++;
        }
    }

    private void DumpClockActive() {
        int count = CountClockActive();
        Debug.Log("Number of active sprites in clock: " + count);
        for ( int i = 0; i < count; i++) {
            Debug.Log("Sprite " + i + ": " + GetComponentsInChildren<SpriteRenderer>()[i].name);
        }
    }
    

}
