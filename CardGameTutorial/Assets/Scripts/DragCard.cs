using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragCard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Vector3 offset;
    bool isDragging;
    Transform preParent;

    private void Update()
    {
        if (isDragging)
        {
            transform.position = Input.mousePosition - offset;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        preParent = transform.parent;
        isDragging = true;
        offset = Input.mousePosition - transform.position;
        transform.SetParent(GameObject.Find("Canvas").transform);

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        // RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.zero);
        // foreach (var hit in hits)
        // {
        //     if (hit.collider != null)
        //     {
        //         Debug.Log(hit.collider.tag);
        //     }
        // }
        transform.SetParent(preParent);
    }
}
