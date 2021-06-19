using System;
using UnityEngine;

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
                wrapAction: () => gameObject.transform.position += GetHorizontalDistanceValue() * Vector3.right,
                clampAction: () => gameObject.transform.position = new Vector3(GetLeftEdgeValue(), gameObject.transform.position.y, gameObject.transform.position.z)
            );
        }
        else if(gameObject.transform.position.x > _horizontalArea.GetRightEdgeValue(_rightEdgeOverride))
        {
            //Right edge
            ProcessBoundary
            (
                _horizontalBoundaryMode,
                wrapAction: () => gameObject.transform.position += GetHorizontalDistanceValue() * Vector3.left,
                clampAction: () => gameObject.transform.position = new Vector3(GetRightEdgeValue(), gameObject.transform.position.y, gameObject.transform.position.z)
            );
        }

        if(gameObject.transform.position.y <= _verticalArea.GetBottomEdgeValue(_bottomEdgeOverride))
        {
            //Bottom edge
            ProcessBoundary
            (
                _verticalBoundaryMode,
                wrapAction: () => gameObject.transform.position += GetVerticalDistanceValue() * Vector3.up,
                clampAction: () => gameObject.transform.position = new Vector3(gameObject.transform.position.x, GetBottomEdgeValue(), gameObject.transform.position.z)
            );
        }
        else if(gameObject.transform.position.y > _verticalArea.GetTopEdgeValue(_topEdgeOverride))
        {
            //Top edge
            ProcessBoundary
            (
                _verticalBoundaryMode,
                wrapAction: () => gameObject.transform.position += GetVerticalDistanceValue() * Vector3.down,
                clampAction: () => gameObject.transform.position = new Vector3(gameObject.transform.position.x, GetTopEdgeValue(), gameObject.transform.position.z)
            );
        }
    }

    public float GetLeftEdgeValue()
    {
        return _horizontalArea.GetLeftEdgeValue(_leftEdgeOverride);
    }

    public float GetRightEdgeValue()
    {
        return _horizontalArea.GetRightEdgeValue(_rightEdgeOverride);
    }

    public float GetBottomEdgeValue()
    {
        return _verticalArea.GetBottomEdgeValue(_bottomEdgeOverride);
    }

    public float GetTopEdgeValue()
    {
        return _verticalArea.GetTopEdgeValue(_topEdgeOverride);
    }

    public float GetHorizontalDistanceValue()
    {
        return _horizontalArea.GetHorizontalDistanceValue(HorizontalDistanceOverride);
    }

    public float GetVerticalDistanceValue()
    {
        return _verticalArea.GetVerticalDistanceValue(VerticalDistanceOverride);
    }

    public bool IsHorizontalOverrideValid()
    {
        return IsOverrideValid(_horizontalArea, _leftEdgeOverride, _rightEdgeOverride);
    }

    public bool IsVerticalOverrideValid()
    {
        return IsOverrideValid(_verticalArea, _bottomEdgeOverride, _topEdgeOverride);
    }

    public static bool IsOverrideValid(ScreenRegion screenRegion, float lowerEdge, float upperEdge)
    {
        return screenRegion != ScreenRegion.Custom || upperEdge - lowerEdge > 0;
    }

    private void ProcessBoundary(BoundaryMode boundaryMode, Action wrapAction, Action clampAction)
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
    }

    private void VerifyEdgeOverrides()
    {
        if(!IsHorizontalOverrideValid())
        {
            throw new Exception($"{nameof(_leftEdgeOverride)} must be less than {nameof(_rightEdgeOverride)} when overriding the edge values");
        }

        if(!IsVerticalOverrideValid())
        {
            throw new Exception($"{nameof(_bottomEdgeOverride)} must be less than {nameof(_topEdgeOverride)} when overriding the edge values");
        }
    }
}
