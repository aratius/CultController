using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUI : SingletonMonoBehaviour<SpeedUI>
{

    [SerializeField] private Slider _slider;

    public float speed {
        get {
            return this._slider.value;
        }
    }

}
