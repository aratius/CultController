using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
///
/// </summary>
public class Member : MonoBehaviour
{
    public int index = -999;
    private readonly Color _BASECOLOR = Color.white;

    public void Init(int i) {
        this.index = i;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        string tag = other.gameObject.tag;
        string instruction = tag;

        Color color = Color.white;

        if(instruction == "leftHandUp")
        {
            color = new Color(1f, 0f, 0f, 1f);

        } else if (instruction == "leftHandDown")
        {
            color = new Color(1f, 0f, 0f, 1f);

        } else if (instruction == "rightHandUp")
        {
            color = new Color(1f, 0f, 0f, 1f);

        } else if (instruction == "rightHandDown")
        {
            color = new Color(1f, 0f, 0f, 1f);

        } else if (instruction == "standUp")
        {
            color = new Color(1f, 0f, 0f, 1f);

        } else if (instruction == "standDown")
        {
            color = new Color(1f, 0f, 0f, 1f);

        } else {
            Debug.LogWarning("undefined instruction");
            return;
        };

        if(this.index >= 0)
        {
            this._DoColor(color);
            OscSender.Instance.Send(this.index, $"/{instruction}", 0);
        }
    }

    /// <summary>
    /// do color
    /// </summary>
    /// <param name="color"></param>
    private void _DoColor(Color color)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(this.GetComponent<SpriteRenderer>().DOColor(color, .1f));
        sequence.Append(this.GetComponent<SpriteRenderer>().DOColor(Color.white, 3f));
    }
}
