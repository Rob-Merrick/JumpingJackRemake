using UnityEngine;

public class KonamiCodeHint : MonoBehaviour
{
    [SerializeField] private GameObject _regular1;
    [SerializeField] private GameObject _regular2;
    [SerializeField] private GameObject _regular3;
    [SerializeField] private GameObject _regular4;
    [SerializeField] private GameObject _regular5;
    [SerializeField] private GameObject _regular6;
    [SerializeField] private GameObject _regular7;
    [SerializeField] private GameObject _regular8;
    [SerializeField] private GameObject _konami1;
    [SerializeField] private GameObject _konami2;
    [SerializeField] private GameObject _konami3;
    [SerializeField] private GameObject _konami4;
    [SerializeField] private GameObject _konami5;
    [SerializeField] private GameObject _konami6;
    [SerializeField] private GameObject _konami7;
    [SerializeField] private GameObject _konami8;

    private float _totalTime = 0.0F;

    private void Update()
    {
        _totalTime += Time.deltaTime;

        if(_totalTime >= 5.0F)
		{
            _totalTime = -0.5F;
		}

        bool isRegular = _totalTime >= 0.0F;
        _regular1.SetActive(isRegular);
        _regular2.SetActive(isRegular);
        _regular3.SetActive(isRegular);
        _regular4.SetActive(isRegular);
        _regular5.SetActive(isRegular);
        _regular6.SetActive(isRegular);
        _regular7.SetActive(isRegular);
        _regular8.SetActive(isRegular);
        _konami1.SetActive(!isRegular);
        _konami2.SetActive(!isRegular);
        _konami3.SetActive(!isRegular);
        _konami4.SetActive(!isRegular);
        _konami5.SetActive(!isRegular);
        _konami6.SetActive(!isRegular);
        _konami7.SetActive(!isRegular);
        _konami8.SetActive(!isRegular);
    }
}
