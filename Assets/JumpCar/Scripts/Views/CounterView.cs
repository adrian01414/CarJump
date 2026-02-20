using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CounterView : MonoBehaviour
{
    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    public void SetValue(string value)
    {
        _text.text = value;
    }
}
