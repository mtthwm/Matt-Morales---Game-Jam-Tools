using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoSelectButton : MonoBehaviour
{
    private Button _btn;

    void OnEnable()
    {
        _btn = GetComponent<Button>();
        _btn.Select();
    }

}
