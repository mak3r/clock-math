using UnityEngine;
using UnityEngine.UI;

public class TimeDisplayScript : MonoBehaviour
{
    public Sprite[] numberSprites; // An array of the sprite images for the numbers 0-9 and the colon symbol
    private RectTransform clockRowRectTrans;
    private RectTransform symbolRowRectTrans;
    private HorizontalLayoutGroup layoutGroup;
    private bool isMathMode = false;
    [SerializeField] private SecondsButton secondsButtonScript;
    private enum SPRITES {
        ZERO=0,ONE,TWO,THREE,FOUR,FIVE,SIX,SEVEN,EIGHT,NINE,COLON,PLUS,MINUS,EQUALS,
    };
    private SpriteRenderer separator;
    private SpriteRenderer mathA;
    

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
        separator = GameObject.Find("separator").GetComponent<SpriteRenderer>();
        mathA = GameObject.Find("mathA").GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isMathMode) {
            MathMode();
        } else {
            Activate("mathA", false);
            ClockMode();
        }

    }

    public void StopTime(bool stop) {
        isMathMode = stop;
    }

    private void MathMode() {
        separator.enabled = true;
    }

    private void ClockMode() {
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

        SpriteRenderer hourones = GameObject.Find("hourones").GetComponent<SpriteRenderer>();
        if(hourones) {
            hourones.sprite = numberSprites[hours % 10]; 
        }

        if(separator) {
            separator.sprite = numberSprites[(int)SPRITES.COLON]; 
            separator.enabled = seconds % 2 == 0;
        }

        SpriteRenderer minutestens = GameObject.Find("minutestens").GetComponent<SpriteRenderer>();
        if(hourones) {
            minutestens.sprite = numberSprites[minutes / 10]; 
        }
        
        SpriteRenderer minutesones = GameObject.Find("minutesones").GetComponent<SpriteRenderer>();
        if(minutesones) {
            minutesones.sprite = numberSprites[minutes % 10]; 
        }

    }

    private void ResetLayout() {
        // Calculate the size of the LayoutGroup
        int childCount = clockRowRectTrans.childCount;
        float totalWidth = layoutGroup.GetComponent<RectTransform>().rect.width;
        if (childCount == 6) {
            layoutGroup.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 8f);
        } else if (childCount == 7) {
            layoutGroup.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 10f);
        } else {
            layoutGroup.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 6f);
        }
    }

    public void Activate(string name, bool active) {
        // Find and get ClockRow RectTransform component
        GameObject clockRow = GameObject.Find("ClockRow");
        if (clockRow != null)
        {
            GameObject obj = clockRowRectTrans.Find(name).gameObject;
            obj.SetActive(active);
            if (active) {
                if (name == "mathA") {
                    mathA.sprite = numberSprites[(int)SPRITES.PLUS];
                    isMathMode = secondsButtonScript.EmulateClick();
                }
            }
        }
        ResetLayout();
    }

/**
DEBUG METHODS
*/

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

    private int CountClockActive() {
        GameObject clockRow = GameObject.Find("ClockRow");
        return clockRow.GetComponentsInChildren<SpriteRenderer>().Length;
    }

    private void DumpClockActive() {
        int count = CountClockActive();
        Debug.Log("Number of active sprites in clock: " + count);
        for ( int i = 0; i < count; i++) {
            Debug.Log("Sprite " + i + ": " + GetComponentsInChildren<SpriteRenderer>()[i].name);
        }
    }
    

}
