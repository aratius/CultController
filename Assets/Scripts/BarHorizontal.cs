using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarHorizontal : MonoBehaviour
{

    private float _vel = 0;

    void Update()
    {
        Vector2 pos = this.transform.position;
        pos.y += _vel * SpeedUI.Instance.speed * .1f;
        this.transform.position = pos;
        if(Mathf.Abs(pos.y) > 5f) Destroy(this.gameObject);
    }

    public void ToTop()
    {
        _vel = 1f;
    }

    public void ToBottom()
    {
        _vel = -1f;
    }

}
