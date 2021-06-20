using UnityEngine;

public class MoveAI : MonoBehaviour
{
	[SerializeField] private int _floorPositionOffset = 0;

	private int _floorNumber;
	private float _hiddenTime = 2.0F;
	private SpriteRenderer _spriteRenderer;

	private void Start()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_floorNumber = Random.Range(1, 8);
		WarpManager.Instance.PlaceObjectOnFloor(gameObject, _floorNumber, _floorPositionOffset);
		gameObject.transform.position += Random.Range(ScreenManager.Instance.PlayableAreaLeftEdge, ScreenManager.Instance.PlayableAreaRightEdge) * Vector3.right;
	}

	private void Update()
	{
		if(_hiddenTime >= 2.0F)
		{
			_spriteRenderer.enabled = true;
			gameObject.transform.Translate(Time.deltaTime * GameSettingsManager.Instance.RunSpeed * Vector3.left);
		}
		else
		{
			_hiddenTime += Time.deltaTime;
		}
	}

	public void WarpActivated()
	{
		_floorNumber++;

		if(_floorNumber >= 8)
		{
			_hiddenTime = 0.0F;
			_floorNumber = 1;
			_spriteRenderer.enabled = false;
		}

		WarpManager.Instance.PlaceObjectOnFloor(gameObject, _floorNumber, _floorPositionOffset);
	}
}
