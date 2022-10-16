using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarVertical : MonoBehaviour
{

    private float _vel = 0;

    void Update()
    {
        Vector2 pos = this.transform.position;
        pos.x += _vel * SpeedUI.Instance.speed * .1f;
        this.transform.position = pos;
        if(Mathf.Abs(pos.x) > 9f) Destroy(this.gameObject);
    }

    public void ToRight()
    {
        this._vel = 1f;
    }

    public void ToLeft()
    {
        this._vel = -1f;
    }

}
