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
  [SerializeField] private TextMesh _textMesh;
  private readonly Color _BASECOLOR = Color.white;

  void Start()
  {
    this._textMesh.text = this.index.ToString();
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    string tag = other.gameObject.tag;
    string instruction = tag;

    Color color = Color.white;

    if (instruction == "handsUp")
    {
      color = new Color(1f, 0f, 0f, 1f);

    }
    else if (instruction == "handsDown")
    {
      color = new Color(0f, 1f, 0f, 1f);

    }
    else if (instruction == "standUp")
    {
      color = new Color(0f, 0f, 1f, 1f);

    }
    else if (instruction == "sitDown")
    {
      color = new Color(1f, 1f, 0f, 1f);

    }
    else if (instruction == "tutorial")
    {
      color = new Color(1f, 0f, 1f, 1f);

    }
    else if (instruction == "finish")
    {
      color = new Color(0f, 1f, 1f, 1f);

    }
    else if (instruction == "stop")
    {
      color = new Color(0f, 0f, 0f, 1f);
    }
    else
    {
      Debug.LogWarning("undefined instruction");
      return;
    };

    if (this.index >= 0)
    {
      this._DoColor(color);
      OscSender.Instance.Send(this.index, $"/{instruction}", 0);
      // TODO: 1.8sec後にLEDとmaxへOSC
      Utils.DoItAfter(() =>
      {
        OscSender.Instance.Send(this.index, $"/tone", 1);  // maxへ
        OscSender.Instance.Send(this.index, $"/led/{this.index}", 1);  // TDへ
        Utils.DoItAfter(() =>
        {
          OscSender.Instance.Send(this.index, $"/tone", 0);
        }, 100);
      }, 1800);
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
