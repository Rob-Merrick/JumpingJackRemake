using TMPro;
using UnityEngine;

public class LevelsTextUpdate3D : MonoBehaviour
{
    private TextMeshProUGUI _livesText;

    private void Start()
    {
        _livesText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _livesText.text = $"={GameManager3D.Instance.Level}";
    }
}
