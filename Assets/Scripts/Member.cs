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
  private bool _isStanding = false;
  private bool _isRaisingHands = false;

  void Start()
  {
    this._textMesh.text = this.index.ToString();
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    string tag = other.gameObject.tag;
    string instruction = tag;

    Color color = Color.white;

    string instructionToSend = "";

    if (instruction == "hands")
    {
      color = new Color(1f, 0f, 0f, 1f);
      instructionToSend = this._isRaisingHands ? "handsDown" : "handsUp";
      this._isRaisingHands = !this._isRaisingHands;
    }
    else if (instruction == "stand")
    {
      color = new Color(0f, 0f, 1f, 1f);
      instructionToSend = this._isStanding ? "sitDown" : "standUp";
      this._isStanding = !this._isStanding;
    }
    else if (instruction == "tutorial")
    {
      color = new Color(1f, 0f, 1f, 1f);
      instructionToSend = instruction;
    }
    else if (instruction == "finish")
    {
      color = new Color(0f, 1f, 1f, 1f);
      instructionToSend = instruction;
    }
    else if (instruction == "stop")
    {
      color = new Color(0f, 0f, 0f, 1f);
      instructionToSend = instruction;
    }
    else
    {
      Debug.LogWarning("undefined instruction");
      return;
    };

    if (this.index >= 0)
    {
      this._DoColor(color);
      OscSender.Instance.Send(this.index, $"/{instructionToSend}", 0);
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
