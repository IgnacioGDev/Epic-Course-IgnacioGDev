using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtons : MonoBehaviour
{
    [SerializeField]
    private Image _button;
    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Image>();
        _button.enabled = true;
    }

    public void OnMouseDown()
    {
        Debug.Log("BUTTON PRESSED" + gameObject.name);
        _button.enabled = true;
    }

    private void OnMouseUp()
    {
        _button.enabled = false;
    }

}
