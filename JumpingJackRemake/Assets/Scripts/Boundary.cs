using System;
using UnityEngine;
using UnityEngine.Events;

public enum BoundaryMode
{
    None = 1,
    Wrap = 2,
    Clamp = 3
}

public class Boundary : MonoBehaviour
{
    [SerializeField] [HideInInspector] private BoundaryMode _horizontalBoundaryMode = BoundaryMode.Wrap;
    [SerializeField] [HideInInspector] private BoundaryMode _verticalBoundaryMode = BoundaryMode.Wrap;
    [SerializeField] [HideInInspector] private ScreenRegion _horizontalArea = ScreenRegion.PlayableArea;
    [SerializeField] [HideInInspector] private ScreenRegion _verticalArea = ScreenRegion.PlayableArea;
    [SerializeField] [HideInInspector] private float _leftEdgeOverride = -1.0F;
    [SerializeField] [HideInInspector] private float _rightEdgeOverride = 1.0F;
    [SerializeField] [HideInInspector] private float _bottomEdgeOverride = -1.0F;
    [SerializeField] [HideInInspector] private float _topEdgeOverride = 1.0F;
    [SerializeField] [HideInInspector] private UnityEvent _leftEdgeCallback = null;
    [SerializeField] [HideInInspector] private UnityEvent _rightEdgeCallback = null;
    [SerializeField] [HideInInspector] private UnityEvent _bottomEdgeCallback = null;
    [SerializeField] [HideInInspector] private UnityEvent _topEdgeCallback = null;

    private float LeftEdgeValue => _horizontalArea.GetLeftEdgeValue(_leftEdgeOverride);
    private float RightEdgeValue => _horizontalArea.GetRightEdgeValue(_rightEdgeOverride);
    private float BottomEdgeValue => _verticalArea.GetBottomEdgeValue(_bottomEdgeOverride);
    private float TopEdgeValue => _verticalArea.GetTopEdgeValue(_topEdgeOverride);
    private float HorizontalDistanceValue => _horizontalArea.GetHorizontalDistanceValue(HorizontalDistanceOverride);
    private float VerticalDistanceValue => _verticalArea.GetVerticalDistanceValue(VerticalDistanceOverride);
    private float HorizontalDistanceOverride => _rightEdgeOverride - _leftEdgeOverride;
    private float VerticalDistanceOverride => _topEdgeOverride - _bottomEdgeOverride;

    private void Start()
    {
        VerifyEdgeOverrides();
    }

    private void LateUpdate()
    {
        if(_horizontalArea == 0 || _verticalArea == 0) //These are uninitialized for the first frame. Skipping works
        {
            return;
        }

        if(gameObject.transform.position.x <= _horizontalArea.GetLeftEdgeValue(_leftEdgeOverride))
        {
            //Left edge
            ProcessBoundary
            (
                _horizontalBoundaryMode,
                wrapAction: () => gameObject.transform.position += HorizontalDistanceValue * Vector3.right,
                clampAction: () => gameObject.transform.position = new Vector3(LeftEdgeValue, gameObject.transform.position.y, gameObject.transform.position.z),
                _leftEdgeCallback
            );
        }
        else if(gameObject.transform.position.x > _horizontalArea.GetRightEdgeValue(_rightEdgeOverride))
        {
            //Right edge
            ProcessBoundary
            (
                _horizontalBoundaryMode,
                wrapAction: () => gameObject.transform.position += HorizontalDistanceValue * Vector3.left,
                clampAction: () => gameObject.transform.position = new Vector3(RightEdgeValue, gameObject.transform.position.y, gameObject.transform.position.z),
                _rightEdgeCallback
            );
        }

        if(gameObject.transform.position.y <= _verticalArea.GetBottomEdgeValue(_bottomEdgeOverride))
        {
            //Bottom edge
            ProcessBoundary
            (
                _verticalBoundaryMode,
                wrapAction: () => gameObject.transform.position += VerticalDistanceValue * Vector3.up,
                clampAction: () => gameObject.transform.position = new Vector3(gameObject.transform.position.x, BottomEdgeValue, gameObject.transform.position.z),
                _bottomEdgeCallback
            );
        }
        else if(gameObject.transform.position.y > _verticalArea.GetTopEdgeValue(_topEdgeOverride))
        {
            //Top edge
            ProcessBoundary
            (
                _verticalBoundaryMode,
                wrapAction: () => gameObject.transform.position += VerticalDistanceValue * Vector3.down,
                clampAction: () => gameObject.transform.position = new Vector3(gameObject.transform.position.x, TopEdgeValue, gameObject.transform.position.z),
                _topEdgeCallback
            );
        }
    }

    public static bool IsOverrideValid(ScreenRegion screenRegion, float lowerEdge, float upperEdge)
    {
        return screenRegion != ScreenRegion.Custom || upperEdge - lowerEdge > 0;
    }

    private void ProcessBoundary(BoundaryMode boundaryMode, Action wrapAction, Action clampAction, UnityEvent callback)
    {
        switch(boundaryMode)
        {
            case BoundaryMode.None:
                return;
            case BoundaryMode.Wrap:
                wrapAction.Invoke();
                break;
            case BoundaryMode.Clamp:
                clampAction.Invoke();
                break;
            default:
                throw new NotImplementedException();
        }

        if(callback != null)
		{
            callback.Invoke();
		}
    }

    private void VerifyEdgeOverrides()
    {
        if(!IsOverrideValid(_horizontalArea, _leftEdgeOverride, _rightEdgeOverride))
        {
            throw new Exception($"{nameof(_leftEdgeOverride)} must be less than {nameof(_rightEdgeOverride)} when overriding the edge values");
        }

        if(!IsOverrideValid(_verticalArea, _bottomEdgeOverride, _topEdgeOverride))
        {
            throw new Exception($"{nameof(_bottomEdgeOverride)} must be less than {nameof(_topEdgeOverride)} when overriding the edge values");
        }
    }
}
