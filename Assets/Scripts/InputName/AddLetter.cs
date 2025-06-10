using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddLetter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inputText;

    private string letterToAdd;
    private Button button;

    private void Start()
    {
        letterToAdd = gameObject.name;
        if (inputText == null)
        {
            inputText = GameObject.Find("InputText").GetComponent<TextMeshProUGUI>();
        }
        button = GetComponent<Button>();

        button.onClick.AddListener(AddLetterToInput);
    }

    private void AddLetterToInput()
    {
        if (inputText != null && inputText.text.Length < 7)
        {
            inputText.text += letterToAdd;
        }
    }
}
