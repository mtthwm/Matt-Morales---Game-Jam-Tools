using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoSelectButton : MonoBehaviour
{
    private Button _btn;

    void Start()
    {
        _btn = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _btn.Select();
    }

}
