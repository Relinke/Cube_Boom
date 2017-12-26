/// <summary>
/// This is the only main object of game, it has 3 main attribute,
/// _boomNum means letting it boom need how many times tap, and how much damage can it cause
/// _lifeNum means how many times tap before cube boom,
/// As _position, as its name is, so it is.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cube : MonoBehaviour {
    #region Instance part
    #endregion

    #region State Part
    public enum CubeState {
        NONE,
        TAP,
        BOOM
    }
    #endregion

    #region Show In Inspector
    [Header ("Animation Part")]
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private string _initCubeAnimName = "InitCube";

    [SerializeField]
    private string _tapAnimName = "Tap";

    [SerializeField]
    private string _boomAnimName = "Boom";

    [SerializeField]
    private string _hideAnimName = "Hide";

    [Header ("Component")]
    [SerializeField]
    private Text _lifeNumText;
    #endregion

    #region Hide In Inspector
    private int _boomNum;
    private int _lifeNum;
    private int _row;
    private int _column;

    private CubeState _cubeState = CubeState.NONE;
    #endregion

    #region Get Part
    public CubeState Get_cubeState () {
        return _cubeState;
    }

    public int Get_boomNum () {
        return _boomNum;
    }
    #endregion

    #region Init Part
    public void Init (int boomNum, int r, int c) {
        InitAnim ();
        InitVariable (boomNum, r, c);
        InitUI ();
        InitAudio ();
    }

    private void InitAnim () {
        _animator.Play (_initCubeAnimName, 0, 0);
    }

    private void InitVariable (int boomNum, int r, int c) {
        _boomNum = boomNum;
        _lifeNum = _boomNum;
        _row = r;
        _column = c;
    }

    private void InitUI () {
        UpdateUIDisplay ();
    }

    private void InitAudio () {
        AudioManager.instance.PlayGenerateSFX ();
    }
    #endregion

    #region Update Part

    #endregion

    #region Collision Part

    #endregion

    private void SwitchState (CubeState state) {
        if (_cubeState == state) {
            return;
        }

        _cubeState = state;
        switch (_cubeState) {
            case CubeState.NONE:
                InitNoneState ();
                break;
            case CubeState.TAP:
                InitTapState ();
                break;
            case CubeState.BOOM:
                InitBoomState ();
                break;
        }
    }

    private void InitNoneState () {

    }

    private void InitTapState () {
        _animator.Play (_tapAnimName, 0, 0);

    }

    private void InitBoomState () {
        _animator.Play (_boomAnimName, 0, 0);
    }
    #region Function Part
    public void PlayerTap () {
        if (CubeManager.instance.Get_generatingState () != CubeManager.ManagerState.NONE ||
            TriangleManager.instance.GetEnabledTriangleCount () <= 0) {
            return;
        }
        SwitchState (CubeState.TAP);
        AudioManager.instance.PlayTapSFX ();
        CubeManager.instance.SettleTap ();
        TriangleManager.instance.LoseOneTriangle ();
    }

    public void InsideTap () {
        SwitchState (CubeState.TAP);
        AudioManager.instance.PlayTapSFX ();
    }

    public void Boom () {
        SwitchState (CubeState.BOOM);
    }

    private void LoseLife () {
        --_lifeNum;
        UpdateUIDisplay ();
        if (_lifeNum <= 0) {
            Boom ();
        } else {
            SwitchState (CubeState.NONE);
        }
    }

    private void UpdateUIDisplay () {
        _lifeNumText.text = _lifeNum.ToString ();
    }

    private void DestroyCube () {
        CubeManager.instance.BoomAt (_row, _column);
        Destroy (gameObject);
    }

    private void DirectDestroyCube () {
        Destroy (gameObject);
    }

    public void HideCube () {
        _animator.Play (_hideAnimName, 0, 0);
    }

    private void PlayBoomSFX () {
        AudioManager.instance.PlayBoomSFX ();
    }
    #endregion
}