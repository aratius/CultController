using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GetTouchUI : MonoBehaviour, IPointerDownHandler
{

    public UnityEvent<Vector2> onGetTouchDown = new UnityEvent<Vector2>();

    public void OnPointerDown(PointerEventData e)
    {
        Debug.Log(e.position);
        this.onGetTouchDown.Invoke(e.position);
    }
}
