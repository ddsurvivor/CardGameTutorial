using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowFollow : MonoBehaviour
{
    public Vector2 startPoint;
    RectTransform arrow;
    Vector2 endPoint;

    float arrowLength;
    Vector2 arrowPosition;
    float theta;
    // Start is called before the first frame update
    void Start()
    {
        arrow = transform.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        endPoint = Input.mousePosition - new Vector3(960.0f, 540.0f, 0.0f);

        arrowLength = Mathf.Sqrt((endPoint.x - startPoint.x) * (endPoint.x - startPoint.x) + (endPoint.y - startPoint.y) * (endPoint.y - startPoint.y));
        arrowPosition = new Vector2((endPoint.x + startPoint.x) / 2, (endPoint.y + startPoint.y) / 2);
        theta = Mathf.Atan2((endPoint.y - startPoint.y), (endPoint.x - startPoint.x));

        arrow.localPosition = arrowPosition;
        arrow.sizeDelta = new Vector2(arrowLength, arrow.sizeDelta.y);
        arrow.localEulerAngles = new Vector3(0.0f, 0.0f, theta * 180 / Mathf.PI);
    }
    public void SetStartPoint(Vector2 _startPoint)
    {
        startPoint = _startPoint - new Vector2(960.0f, 540.0f);
    }
}
