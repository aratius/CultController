using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public struct UIKey
{
    public GameObject ui;
    public string key;
}

public class SelectUI : MonoBehaviour
{
    public string currentKey;
    public UnityEvent onSelect = new UnityEvent();

    [SerializeField] private UIKey[] _uis;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < this._uis.Length; i++)
        {
            Button ui = this._uis[i].ui.GetComponent<Button>();
            int index = i;
            ui.onClick.AddListener(() => {
                this._OnClick(index);
            });
            this._OnClick(0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void _OnClick(int index)
    {
        string key = this._uis[index].key;
        this.currentKey = key;
        this.onSelect.Invoke();

        // 色適用
        for(int i = 0; i < this._uis.Length; i++)
        {
            Image uiImage = this._uis[i].ui.GetComponent<Image>();
            float alpha = index == i ? .3f : .1f;
            Color crr = uiImage.color;
            uiImage.color = new Color(crr.r, crr.g, crr.b, alpha);
        }
    }

}
