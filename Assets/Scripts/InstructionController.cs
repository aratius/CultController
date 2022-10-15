using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionController : MonoBehaviour
{

    [SerializeField] private SelectUI _instructionUI;
    [SerializeField] private SelectUI _shapeUI;
    [SerializeField] private GameObject _barHorizontal;
    [SerializeField] private GameObject _barVertical;
    [SerializeField] private GameObject _mouseFollwerPrefab;

    private TouchManager _touchManager = new TouchManager();
    private GameObject _mouseFollower;

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
    }

    /// <summary>
    ///
    /// </summary>
    private void _OnTouchEnd()
    {
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
        Debug.Log(position);
    }

    /// <summary>
    /// create bar horizontal
    /// </summary>
    /// <param name="instruction"></param>
    /// <param name="x"></param>
    public void CreateBarHorizontal(string instruction, float x)
    {

    }

    /// <summary>
    /// create bar vertical
    /// </summary>
    /// <param name="instruction"></param>
    /// <param name="y"></param>
    public void CreateBarVertical(string instruction, float y)
    {

    }

}
