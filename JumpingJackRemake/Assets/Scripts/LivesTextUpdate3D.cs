using TMPro;
using UnityEngine;

public class LivesTextUpdate3D : MonoBehaviour
{
    private TextMeshProUGUI _livesText;

    private void Start()
    {
        _livesText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _livesText.text = $"x {LennyManager3D.Instance.Lives}";
    }
}
