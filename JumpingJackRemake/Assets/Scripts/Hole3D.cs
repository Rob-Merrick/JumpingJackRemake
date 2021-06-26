using System.Collections;
using UnityEngine;

public class Hole3D : MonoBehaviour
{
    [SerializeField] private float _startRotationRadians = 0.0F;
    [SerializeField] private MoveAIDirection _moveDirection = MoveAIDirection.LeftUp;

    //private float _minSize = 0.01F;

    private float _size;
    private float _timeToLive;
    private float _totalTimeInExistance;
    private float _totalShrinkTime;
    private bool _isShrinking;
    private bool _isGrowing;
    private bool _isInitialized = false;
    
    public int FloorNumber { get; set; }
    public int HoleIndex { get; set; }
    public float CurrentRotation { get; private set; }
    public float StartRotationRadians { get => _startRotationRadians; set => _startRotationRadians = value; }
    public float Size => _size;
    public MoveAIDirection MoveDirection { get => _moveDirection; set => _moveDirection = value; }

    public void Initialize()
	{
        _size = HoleManager3D.Instance.HoleSizeRadians;//_minSize;
        //_timeToLive = Random.Range(3.0F, 15.0F);
        //_totalTimeInExistance = 0.0F;
        //_totalShrinkTime = 0.0F;
        //_isShrinking = false;
        //_isGrowing = true;
        CurrentRotation = _startRotationRadians;
        CurrentRotation = Mathf.Repeat(CurrentRotation, 2.0F * Mathf.PI);
        //StartCoroutine(GrowFromNothingCoroutine());
        _isInitialized = true;
    }

	private void Update()
    {
        if(!_isInitialized)
		{
            throw new System.Exception($"Don't forget to call {nameof(Hole3D)}.{nameof(Initialize)}() for {name}");
		}

        CurrentRotation += _moveDirection == MoveAIDirection.LeftUp ? HoleManager3D.Instance.RotationalSpeedRadians * Time.deltaTime : -HoleManager3D.Instance.RotationalSpeedRadians * Time.deltaTime;
        CurrentRotation = Mathf.Repeat(CurrentRotation, 2.0F * Mathf.PI);
  //      _totalTimeInExistance += Time.deltaTime;

		//if(!_isGrowing && !_isShrinking && _totalTimeInExistance >= _timeToLive)
		//{
		//	_isShrinking = true;
  //          StartCoroutine(ShrinkToNothingCoroutine());
  //      }
	}

 //   private IEnumerator GrowFromNothingCoroutine()
	//{
 //       while(Application.isPlaying && _size < HoleManager3D.Instance.HoleSizeRadians)
	//	{
 //           _size = Mathf.Lerp(_minSize, HoleManager3D.Instance.HoleSizeRadians, _totalTimeInExistance / HoleManager3D.Instance.GrowShrinkTime);
 //           yield return null;
 //       }

 //       _isGrowing = false;
	//}

 //   private IEnumerator ShrinkToNothingCoroutine()
	//{
 //       while(Application.isPlaying && _size > _minSize)
	//	{
 //           _size = Mathf.Lerp(HoleManager3D.Instance.HoleSizeRadians, _minSize, _totalShrinkTime / HoleManager3D.Instance.GrowShrinkTime);
 //           _totalShrinkTime += Time.deltaTime;
 //           yield return null;
	//	}

 //       FloorManager3D.Instance.WarpHole(this);
 //   }
}
