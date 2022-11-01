using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripples : MonoBehaviour
{

    [SerializeField] Material _material;
    private float _vel = 0;

    void Start()
    {
      this._material = new Material(this._material.shader);
      this.GetComponent<SpriteRenderer>().material = this._material;
    }

    void Update()
    {
      Vector2 localScale = this.transform.localScale;
      localScale += new Vector2(_vel, _vel) * .1f;
      this.transform.localScale = localScale;
      this._material.SetFloat("_Scale", localScale.x);
      if(localScale.x > 50f) Destroy(this.gameObject);
    }

    public void Spread()
    {
      _vel = 1f;
    }

}
