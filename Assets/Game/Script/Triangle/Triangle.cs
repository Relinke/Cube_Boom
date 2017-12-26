using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour {
    #region Instance part
    #endregion

    #region Enum Part
    public enum TriangleState {
        NONE,
        ENABLED,
        DISABLED
    }
    #endregion

    #region Show In Inspector
    [Header ("Animation Setting")]
    [SerializeField]
    private string _initAnimName = "Init";
    [SerializeField]
    private string _enableAnimName = "Enable";

    [SerializeField]
    private string _disableAnimName = "Disable";

    [SerializeField]
    private string _hidingAnimName = "Hide";

    [Header ("Component")]
    [SerializeField]
    private Animator _animator;
    #endregion

    #region Hide In Inspector
    private TriangleState _triangleState = TriangleState.NONE;
    #endregion

    #region Get Part
    public TriangleState Get_triangleState () {
        return _triangleState;
    }
    #endregion

    #region Init Part
    private void Awake () {
        Init ();
    }

    private void Init () {
        PlayInitAnim ();
    }

    private void PlayInitAnim () {
        _animator.Play (_initAnimName, 0, 0);
    }
    #endregion

    #region Update Part

    #endregion

    #region Collision Part

    #endregion

    #region State Part
    private void SwitchState (TriangleState state) {
        if (_triangleState == state) {
            return;
        }

        _triangleState = state;
        switch (_triangleState) {
            case TriangleState.ENABLED:
                InitEnableState ();
                break;
            case TriangleState.DISABLED:
                InitDisableState ();
                break;
        }
    }

    private void InitEnableState () {
        _animator.Play (_enableAnimName, 0, 0);
    }

    private void InitDisableState () {
        _animator.Play (_disableAnimName, 0, 0);
    }
    #endregion

    #region Function Part
    public void EnableTriangle () {
        SwitchState (TriangleState.ENABLED);
    }

    public void DisableTriangle () {
        SwitchState (TriangleState.DISABLED);
    }

    public void HideTriangle () {
        _animator.Play (_hidingAnimName, 0, 0);
    }

    private void DirectDestroy () {
        Destroy (gameObject);
    }
    #endregion
}