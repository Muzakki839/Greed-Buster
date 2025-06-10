using TMPro;
using UnityEngine;

public class InputText : MonoBehaviour
{
    private TextMeshProUGUI inputText;

    private void Start()
    {
        inputText = GetComponent<TextMeshProUGUI>();

        inputText.text = string.Empty;
    }

    public void AddLetter(string letter)
    {
        if (inputText != null && inputText.text.Length < 7)
        {
            inputText.text += letter;
        }
    }

    public void Backspace()
    {
        if (inputText != null && inputText.text.Length > 0)
        {
            inputText.text = inputText.text[..^1];
        }
    }
}
