/// <summary>
/// This is fake cube, which used on main menu, to show Cube Boom title
/// It only keep the tap function
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FakeCube : MonoBehaviour {
    #region Instance part
    #endregion

    #region State Part
    public enum FakeCubeState {
        NONE,
        TAP
    }
    #endregion

    #region Show In Inspector
    [Header ("Boom Character")]
    [SerializeField]
    private char _boomChar = 'A';

    [Header ("Audio Setting")]
    [SerializeField]
    private int _keyNum = 0;

    [Header ("Animation Part")]
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private float _initDelay = 0f;

    [SerializeField]
    private string _initCubeAnimName = "InitCube";

    [SerializeField]
    private string _tapAnimName = "Tap";

    [SerializeField]
    private string _hideAnimName = "Hide";

    [Header ("Component")]
    [SerializeField]
    private Text _lifeNumText;
    #endregion

    #region Hide In Inspector
    private int _boomNum;
    private int _lifeNum;
    private FakeCubeState _cubeState = FakeCubeState.NONE;
    #endregion

    #region Get Part
    public FakeCubeState Get_fakeCubeState () {
        return _cubeState;
    }

    public int Get_boomNum () {
        return _boomNum;
    }
    #endregion

    #region Init Part
    private void Awake () {
        Init ();
    }

    private void Init () {
        InitVariable ();
        UpdateUIDisplay ();
    }

    private void InitVariable () {
        _boomNum = _boomChar;
        _lifeNum = _boomNum;
    }

    private void Start () {
        Invoke ("InitAnimAndAudio", _initDelay);
    }

    private void InitAnimAndAudio () {
        _animator.Play (_initCubeAnimName, 0, 0);
        AudioManager.instance.PlayGenerateSFX ();
    }
    #endregion

    #region Update Part

    #endregion

    #region Collision Part

    #endregion

    #region State Part

    private void SwitchState (FakeCubeState state) {
        if (_cubeState == state) {
            return;
        }

        _cubeState = state;
        switch (_cubeState) {
            case FakeCubeState.NONE:
                InitNoneState ();
                break;
            case FakeCubeState.TAP:
                InitTapState ();
                break;
        }
    }

    private void InitNoneState () {

    }

    private void InitTapState () {
        AudioManager.instance.PlayTapSFX (_keyNum);
        _animator.Play (_tapAnimName, 0, 0);
    }
    #endregion

    #region Function Part
    public void PlayerTap () {
        SwitchState (FakeCubeState.TAP);
    }

    private void LoseLife () {
        --_lifeNum;
        UpdateUIDisplay ();
        SwitchState (FakeCubeState.NONE);
    }

    private void UpdateUIDisplay () {
        if (_lifeNum <= 32) {
            _lifeNum = 126;
        } else if (_lifeNum >= 127) {
            _lifeNum = 33;
        }
        _lifeNumText.text = ((char) _lifeNum).ToString ();
    }

    private void DirectDestroyCube () {
        Destroy (gameObject);
    }

    public void HideCube () {
        _animator.Play (_hideAnimName, 0, 0);
    }

    #endregion
}