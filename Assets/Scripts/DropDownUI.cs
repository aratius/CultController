using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownUI : MonoBehaviour
{

    public bool isActive = false;

    [SerializeField] private GameObject _button;
    [SerializeField] private SelectUI[] _uis;

    // Start is called before the first frame update
    void Start()
    {
        this._button.GetComponent<Button>().onClick.AddListener(this.On);

        foreach(SelectUI ui in this._uis)
        {
            ui.onSelect.AddListener(this.Off);
        }
    }

    public void On()
    {
        this.isActive = true;
            Color crr = this._button.GetComponent<Image>().color;
            this._button.GetComponent<Image>().color = new Color(crr.r, crr.g, crr.b, .3f);
    }

    public void Off()
    {
        this.isActive = false;
        Color crr = this._button.GetComponent<Image>().color;
        this._button.GetComponent<Image>().color = new Color(crr.r, crr.g, crr.b, .1f);
    }

}
