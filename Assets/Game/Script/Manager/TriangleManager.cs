/// <summary>
/// This is a manager script using instance mode.
/// Triangle means player life, once player tap the cube, there will lose 
/// one triangle, 
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleManager : MonoBehaviour {
    #region Instance part
    public static TriangleManager instance;
    #endregion

    #region Enum Part
    private enum TriangleGeneratingState {
        NONE,
        GENERATING,
        HIDING
    }
    #endregion

    #region Show In Inspector
    [Header ("Triangle Setting")]
    [SerializeField]
    private int _initTriangleCount = 5;

    [Header ("Generate Setting")]
    [SerializeField]
    private Vector2 _triangleOffset = new Vector2 (178, 0);

    [SerializeField]
    private Transform _triangleOrigin;

    [SerializeField]
    private float _generatingDelay = 0.3f;

    [SerializeField]
    private float _hidingDelay = 0.3f;

    [Header ("Prefab")]
    [SerializeField]
    private Triangle _trianglePrefab;
    #endregion

    #region Hide In Inspector

    private List<Triangle> _triangleList = new List<Triangle> ();

    private TriangleGeneratingState _TGState = TriangleGeneratingState.NONE;

    private float _timer = 0f;
    #endregion

    #region Get Part
    public int GetEnabledTriangleCount () {
        int count = 0;
        for (int i = 0; i < _triangleList.Count; ++i) {
            if (_triangleList[i].Get_triangleState () == Triangle.TriangleState.ENABLED) {
                ++count;
            }
        }
        return count;
    }
    #endregion

    #region Init Part
    private void Awake () {
        Init ();
    }

    private void Init () {
        InitInstance ();
    }

    private void InitInstance () {
        instance = this;
    }
    #endregion

    #region Update Part
    private void Update () {
        switch (_TGState) {
            case TriangleGeneratingState.NONE:
                UpdatedNoneState ();
                break;
            case TriangleGeneratingState.GENERATING:
                UpdatedGeneratingState ();
                break;
            case TriangleGeneratingState.HIDING:
                UpdatedHidingState ();
                break;
        }
    }

    private void UpdatedNoneState () {

    }

    private void UpdatedGeneratingState () {
        if (_triangleList.Count < _initTriangleCount) {
            if (_timer >= _generatingDelay) {
                Triangle triangle = Instantiate (_trianglePrefab, _triangleOrigin.parent);
                triangle.transform.localPosition =
                    _triangleOrigin.localPosition + (Vector3) _triangleOffset * _triangleList.Count;
                _triangleList.Add (triangle);
                _timer = 0f;
            } else {
                _timer += Time.deltaTime;
            }
        } else {
            SwitchState (TriangleGeneratingState.NONE);
        }
    }

    private void UpdatedHidingState () {
        if (_triangleList.Count > 0) {
            if (_timer >= _generatingDelay) {
                _triangleList[0].HideTriangle ();
                _triangleList.RemoveAt (0);
                _timer = 0f;
            } else {
                _timer += Time.deltaTime;
            }
        } else {
            SwitchState (TriangleGeneratingState.NONE);
        }
    }
    #endregion

    #region Collision Part

    #endregion

    #region State Part
    private void SwitchState (TriangleGeneratingState state) {
        if (_TGState == state) {
            return;
        }

        _TGState = state;
        switch (_TGState) {
            case TriangleGeneratingState.NONE:
                InitNoneState ();
                break;
            case TriangleGeneratingState.GENERATING:
                InitGeneratingState ();
                break;
            case TriangleGeneratingState.HIDING:
                InitHidingState ();
                break;
        }
    }

    private void InitNoneState () {

    }

    private void InitGeneratingState () {
        _triangleList.Clear ();
        _timer = _generatingDelay;
    }

    private void InitHidingState () {
        _timer = _hidingDelay;
    }
    #endregion

    #region Function Part
    public void InitTriangle () {
        SwitchState (TriangleGeneratingState.GENERATING);
    }

    public void LoseOneTriangle () {
        if (GetEnabledTriangleCount () <= 0) {
            return;
        }
        for (int i = 0; i < _triangleList.Count; ++i) {
            if (_triangleList[i].Get_triangleState () != Triangle.TriangleState.DISABLED) {
                _triangleList[i].DisableTriangle ();
                // if (GetEnabledTriangleCount () <= 0) {

                // }
                return;
            }
        }
    }

    public void AddOneTriangle () {
        if (GetEnabledTriangleCount () > _initTriangleCount) {
            return;
        }
        for (int i = _triangleList.Count - 1; i >= 0; --i) {
            if (_triangleList[i].Get_triangleState () != Triangle.TriangleState.ENABLED) {
                _triangleList[i].EnableTriangle ();
                return;
            }
        }
    }

    public void StartHideAllTriangle () {
        SwitchState (TriangleGeneratingState.HIDING);
    }
    #endregion
}