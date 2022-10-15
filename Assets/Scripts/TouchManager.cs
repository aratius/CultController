using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Touch manager
/// </summary>
public class TouchManager {

    public UnityEvent<Vector2> onTouchStart = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> onTouchMove = new UnityEvent<Vector2>();
    public UnityEvent onTouchEnd = new UnityEvent();

    /// <summary>
    ///
    /// </summary>
    public void Update() {

        // エディタ
        if (Application.isEditor) {
            // 押した瞬間
            if (Input.GetMouseButtonDown(0)) {
                this.onTouchStart.Invoke(Input.mousePosition);
            }

            // 離した瞬間
            if (Input.GetMouseButtonUp(0)) {
                this.onTouchEnd.Invoke();
            }

            // 押しっぱなし
            if (Input.GetMouseButton(0)) {
                this.onTouchMove.Invoke(Input.mousePosition);
            }

        // 端末
        } else {
            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Began)
                {
                    this.onTouchStart.Invoke(touch.position);
                } else if (touch.phase == TouchPhase.Ended)
                {
                    this.onTouchEnd.Invoke();
                } else if (touch.phase == TouchPhase.Moved)
                {
                    this.onTouchMove.Invoke(touch.position);
                }
            }
        }
    }
}