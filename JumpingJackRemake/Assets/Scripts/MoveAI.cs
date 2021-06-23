using UnityEngine;

public enum MoveAIDirection
{
	LeftUp = 1,
	RightDown = 2
}

public class MoveAI : MonoBehaviour
{
	[SerializeField] private MoveAIDirection _moveDirection = MoveAIDirection.LeftUp;
	[SerializeField] private int _floorPositionOffset = 0;
	[SerializeField] [Range(0.0F, 5.0F)] private float _delayAfterFloorCycleReset = 2.0F;
	[SerializeField] private bool _includeTopFloor = false;

	private int _floorNumber;
	private float _hiddenTime;
	private SpriteRenderer _spriteRenderer;
	private BoxCollider2D _boxCollider;

	private int TopFloor => _includeTopFloor ? 8 : 7;
	private int BottomFloor => 1;
	private Vector3 HorizontalDirection => _moveDirection == MoveAIDirection.LeftUp ? Vector3.left : Vector3.right;

	public MoveAIDirection MoveDirection { get => _moveDirection; set => _moveDirection = value; }
	public bool IsSpawned { get; set; } = true;

	private void Start()
	{
		_hiddenTime = _delayAfterFloorCycleReset;
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_boxCollider = GetComponent<BoxCollider2D>();
		(int floorNumber, Vector2Int screenPosition) = PlacementManager.Instance.GetRandomPosition();
		_floorNumber = floorNumber;
		gameObject.transform.position = new Vector3(screenPosition.x, screenPosition.y, gameObject.transform.position.z);
		WarpManager.Instance.PlaceObjectOnFloor(gameObject, _floorNumber, _floorPositionOffset);
	}

	private void Update()
	{
		if(_hiddenTime >= _delayAfterFloorCycleReset)
		{
			_spriteRenderer.enabled = IsSpawned;
			_boxCollider.enabled = IsSpawned;
			gameObject.transform.Translate(Time.deltaTime * GameSettingsManager.Instance.RunSpeed * HorizontalDirection);
		}
		else
		{
			_hiddenTime += Time.deltaTime;
		}
	}

	public void WarpActivated()
	{
		ChangeFloors();
		WarpManager.Instance.PlaceObjectOnFloor(gameObject, _floorNumber, _floorPositionOffset);
	}

	private void ChangeFloors()
	{
		bool isMovingUp = _moveDirection == MoveAIDirection.LeftUp;
		_floorNumber += isMovingUp ? 1 : -1;

		if(isMovingUp && _floorNumber > TopFloor || !isMovingUp && _floorNumber < BottomFloor)
		{
			_hiddenTime = 0.0F;
			_floorNumber = isMovingUp ? BottomFloor : TopFloor;
			_spriteRenderer.enabled = false;
		}
	}
}
