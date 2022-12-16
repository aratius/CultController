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
    private bool _isTouching = false;

    public TouchManager(GetTouchUI container)
    {
        container.onGetTouchDown.AddListener(() => {
            if (Application.isEditor) {
                this.onTouchStart.Invoke(Input.mousePosition);
                this._touchStartPosition = Input.mousePosition;
            } else if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
                this.onTouchStart.Invoke(touch.position);
                this._touchStartPosition = touch.position;
            }
            this.isMoved = false;
            this._isTouching = true;
        });
    }

    /// <summary>
    ///
    /// </summary>
    public void Update() {

        // エディタ
        if (Application.isEditor) {

            // 離した瞬間
            if (Input.GetMouseButtonUp(0)) {
                if(this._isTouching)
                {
                    this.onTouchEnd.Invoke(Input.mousePosition);
                }
                this._touchStartPosition = new Vector2(-999f, -999f);
                this._isTouching = false;
            }

            // 押しっぱなし
            if (Input.GetMouseButton(0) && this._isTouching) {
                this.onTouchMove.Invoke(Input.mousePosition);
                float dist = Vector2.Distance(Input.mousePosition, this._touchStartPosition);
                if(dist > 10) this.isMoved = true;
            }

        // 端末
        } else {
            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Ended)
                {
                    if(this._isTouching)
                    {
                        this.onTouchEnd.Invoke(touch.position);
                    }
                    this._touchStartPosition = new Vector2(-999f, -999f);
                    this._isTouching = false;
                } else if (touch.phase == TouchPhase.Moved && this._isTouching)
                {
                    this.onTouchMove.Invoke(touch.position);
                    float dist = Vector2.Distance(touch.position, this._touchStartPosition);
                    if(dist > 10) this.isMoved = true;
                }
            }
        }
    }
}