using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Shape {
    public GameObject go;
    public string key;
    public Shape(GameObject _go, string _key) {
        this.go = _go;
        this.key = _key;
    }
}

public class InstructionController : MonoBehaviour
{

    [SerializeField] private SelectUI _instructionUI;
    [SerializeField] private SelectUI _shapeUI;
    [SerializeField] private DropDownUI _dropDownUI;
    [SerializeField] private GameObject _barHorizontal;
    [SerializeField] private GameObject _barVertical;
    [SerializeField] private GameObject _mouseFollwerPrefab;

    private TouchManager _touchManager = new TouchManager();
    private GameObject _mouseFollower;
    private Shape _currentShape;
    private bool isActiveTouch = false;

    void Start()
    {
        this._touchManager.onTouchStart.AddListener(this._OnTouchStart);
        this._touchManager.onTouchEnd.AddListener(this._OnTouchEnd);
        this._touchManager.onTouchMove.AddListener(this._OnTouchMove);
    }

    void Update()
    {
        this._touchManager.Update();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="position"></param>
    private void _OnTouchStart(Vector2 position)
    {
        this._mouseFollower = Instantiate(this._mouseFollwerPrefab, this.transform);
        Vector2 worldPos = (position / new Vector2(1920f, 1080f) - Vector2.one * .5f) * Vector2.one * 2f * new Vector2(9f, 5f);
        this._mouseFollower.transform.position = worldPos;

        if(this._dropDownUI.isActive) {
            string shapeKey = this._shapeUI.currentKey;
            string instructionKey = this._instructionUI.currentKey;

            GameObject go = this.gameObject;
            if(shapeKey == "barHorizontal")
            {
                go = this._CreateBarHorizontal(instructionKey, this._mouseFollower.transform.position.x);
            } else if (shapeKey == "barVertical")
            {
                go = this._CreateBarVertical(instructionKey, this._mouseFollower.transform.position.y);
            }

            if(go != this.gameObject) {
                go.tag = instructionKey;
                this._currentShape = new Shape(go, shapeKey);
            }

            this.isActiveTouch = true;
        }
    }

    /// <summary>
    ///
    /// </summary>
    private void _OnTouchEnd(Vector2 position)
    {
        Vector2 worldPos = (position / new Vector2(1920f, 1080f) - Vector2.one * .5f) * Vector2.one * 2f * new Vector2(9f, 5f);
        string shapeKey = this._shapeUI.currentKey;
        string instructionKey = this._instructionUI.currentKey;

        if(this._touchManager.isMoved)
        {
            if(this.isActiveTouch && this._dropDownUI.isActive)
            {
                GameObject go = this._currentShape.go;
                string key = this._currentShape.key;

                if(key == "barHorizontal")
                {
                    if(worldPos.y < 0) go.GetComponent<BarHorizontal>().ToTop();
                    else go.GetComponent<BarHorizontal>().ToBottom();
                } else if (key == "barVertical")
                {
                    if(worldPos.x < 0) go.GetComponent<BarVertical>().ToRight();
                    else go.GetComponent<BarVertical>().ToLeft();
                }
            }
        } else
        {
            Destroy(this._currentShape.go);
        }

        this.isActiveTouch = false;
        Destroy(this._mouseFollower);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="position"></param>
    private void _OnTouchMove(Vector2 position)
    {
        Vector2 worldPos = (position / new Vector2(1920f, 1080f) - Vector2.one * .5f) * Vector2.one * 2f * new Vector2(9f, 5f);
        this._mouseFollower.transform.position = worldPos;

        if(this.isActiveTouch && this._dropDownUI.isActive) {
            GameObject go = this._currentShape.go;
            Vector2 pos = go.transform.position;
            string key = this._currentShape.key;
            if(key == "barHorizontal")
            {
                pos.x = 0;
                pos.y = worldPos.y;
            } else if (key == "barVertical")
            {
                pos.x = worldPos.x;
                pos.y = 0;
            }
            go.transform.position = pos;
        }
    }

    /// <summary>
    /// create bar horizontal
    /// </summary>
    /// <param name="instruction"></param>
    /// <param name="x"></param>
    private GameObject _CreateBarHorizontal(string instruction, float x)
    {
        return Instantiate(this._barHorizontal, this.transform);
    }

    /// <summary>
    /// create bar vertical
    /// </summary>
    /// <param name="instruction"></param>
    /// <param name="y"></param>
    private GameObject _CreateBarVertical(string instruction, float y)
    {
        return Instantiate(this._barVertical, this.transform);
    }

}
