using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Touch manager
/// </summary>
public class TouchManager {

    public UnityEvent<Vector2> onTouchStart = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> onTouchMove = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> onTouchEnd = new UnityEvent<Vector2>();

    private Vector2 _touchStartPosition = new Vector2(0, 0);
    public bool isMoved = false;

    /// <summary>
    ///
    /// </summary>
    public void Update() {

        // エディタ
        if (Application.isEditor) {
            // 押した瞬間
            if (Input.GetMouseButtonDown(0)) {
                this.onTouchStart.Invoke(Input.mousePosition);
                this._touchStartPosition = Input.mousePosition;
                this.isMoved = false;
            }

            // 離した瞬間
            if (Input.GetMouseButtonUp(0)) {
                this.onTouchEnd.Invoke(Input.mousePosition);
                this._touchStartPosition = new Vector2(-999f, -999f);
            }

            // 押しっぱなし
            if (Input.GetMouseButton(0)) {
                this.onTouchMove.Invoke(Input.mousePosition);
                float dist = Vector2.Distance(Input.mousePosition, this._touchStartPosition);
                if(dist > 10) this.isMoved = true;
            }

        // 端末
        } else {
            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Began)
                {
                    this.onTouchStart.Invoke(touch.position);
                    this._touchStartPosition = touch.position;
                    this.isMoved = false;
                } else if (touch.phase == TouchPhase.Ended)
                {
                    this.onTouchEnd.Invoke(touch.position);
                    this._touchStartPosition = new Vector2(-999f, -999f);
                } else if (touch.phase == TouchPhase.Moved)
                {
                    this.onTouchMove.Invoke(touch.position);
                    float dist = Vector2.Distance(touch.position, this._touchStartPosition);
                    if(dist > 10) this.isMoved = true;
                }
            }
        }
    }
}