using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public struct PresetKey
{
  public GameObject ui;
  public int index;
}

public class PresetUI : MonoBehaviour
{
  [SerializeField] private PresetKey[] _uis;

  // Start is called before the first frame update
  void Start()
  {
    for (int i = 0; i < this._uis.Length; i++)
    {
      Button ui = this._uis[i].ui.GetComponent<Button>();
      int index = i;
      ui.onClick.AddListener(() =>
      {
        this._OnClick(this._uis[index].index);
      });
    }
    this._OnClick(0);
  }

  private void _OnClick(int index)
  {
    OscSender.Instance.Send(-1, "/preset", index);
    OscSender.Instance.Send(-1, "/preset", index);
    OscSender.Instance.Send(-1, "/preset", index);

    // 色適用
    for (int i = 0; i < this._uis.Length; i++)
    {
      Image uiImage = this._uis[i].ui.GetComponent<Image>();
      float alpha = index == i ? .3f : .1f;
      Color crr = uiImage.color;
      uiImage.color = new Color(crr.r, crr.g, crr.b, alpha);
    }
  }

}
