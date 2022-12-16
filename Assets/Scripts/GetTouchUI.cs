using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GetTouchUI : MonoBehaviour, IPointerDownHandler
{

    public UnityEvent onGetTouchDown = new UnityEvent();

    public void OnPointerDown(PointerEventData e)
    {
        this.onGetTouchDown.Invoke();
    }
}
