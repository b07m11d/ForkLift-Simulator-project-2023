using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLookAtCam : MonoBehaviour
{
    GameObject FirstPersonCamObj;

    // Start is called before the first frame update
    void Start()
    {
        FirstPersonCamObj =  GameObject.Find("FirstPersonCam");
        if (RecordUserDate.modeChoose == RecordUserDate.ModeChoose.PC)
        {
            this.transform.LookAt(FirstPersonCamObj.transform);
            this.transform.Rotate(new Vector3(0, -6.47f,-1.875f)); // by Lin 2023-11-15
            this.GetComponent<RectTransform>().localScale = new Vector2(-this.GetComponent<RectTransform>().localScale.x,
                                                                        this.GetComponent<RectTransform>().localScale.y);
        }
    }

   
}
