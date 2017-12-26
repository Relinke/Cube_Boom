/// <summary>
/// This is a manager script using instance mode.
/// The function of this is managing game stage.
/// Main menu enter game, I create a fake main menu in game part, and direct active game menu
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class StageManager : MonoBehaviour {
    #region Instance part
    public static StageManager instance;
    #endregion

    #region Enum Part
    public enum Stage {
        NONE,
        MAIN_MENU,
        GAMING,
        RESULT_MENU
    }
    #endregion

    #region Show In Inspector
    [Header ("Animation - Main Menu Part")]
    [SerializeField]
    private Animator _mainMenuAnimator;

    [SerializeField]
    private string _initMainMenuAnimName = "Init";

    [SerializeField]
    private string _exitMainMenuAnimName = "Exit";

    [Header ("Animation - Gaming Part")]
    [SerializeField]
    private Animator _gamingAnimator;

    [SerializeField]
    private string _initGamingAnimName = "Init";

    [SerializeField]
    private string _exitGamingAnimName = "Exit";

    [Header ("Animation - Result Part")]
    [SerializeField]
    private Animator _resultAnimator;

    [SerializeField]
    private string _initResultAnimName = "Init";

    [SerializeField]
    private string _exitResultAnimName = "Exit";

    [Header ("Generate Setting")]
    [SerializeField]
    private Transform _titleTransform;

    [SerializeField]
    private GameObject _cubeBoomPrefab;
    [SerializeField]
    private Transform _startBtnTransform;

    [SerializeField]
    private StartButtonTri _startBtnPrefab;

    [Header ("Part")]
    [SerializeField]
    private GameObject _mainMenuPart;

    [SerializeField]
    private GameObject _gameingPart;

    [SerializeField]
    private GameObject _resultMenuPart;
    #endregion

    #region Hide In Inspector
    private Stage _preStage = Stage.NONE;
    private Stage _stage = Stage.MAIN_MENU;
    #endregion

    #region Get Part
    public Stage GetCurrStage () {
        return _stage;
    }
    #endregion

    #region Init Part
    private void Awake () {
        Init ();
    }

    private void Init () {
        InitInstance ();
        HideAllPart ();
        InitStage ();
    }

    private void InitInstance () {
        instance = this;
    }
    #endregion

    #region Update Part

    #endregion

    #region Collision Part

    #endregion

    #region State Part
    private void SwitchStage (Stage stage) {
        if (_stage == stage) {
            return;
        }
        _preStage = _stage;
        switch (_preStage) {
            case Stage.MAIN_MENU:
                ExitMainMenuStage ();
                break;
            case Stage.GAMING:
                ExitGamingStage ();
                break;
            case Stage.RESULT_MENU:
                ExitResultMenuStage ();
                break;
        }
        _stage = stage;
    }

    private void ExitMainMenuStage () {
        _mainMenuAnimator.Play (_exitMainMenuAnimName, 0, 0);
        FakeCube[] cubes = FindObjectsOfType<FakeCube> ();
        for (int i = 0; i < cubes.Length; ++i) {
            cubes[i].HideCube ();
        }
    }

    private void ExitGamingStage () {
        AdManager.instance.ShowAD ();
        _gamingAnimator.Play (_exitGamingAnimName, 0, 0);
    }
    private void ExitOptionMenuStage () { }
    private void ExitRankMenuStage () { }
    private void ExitResultMenuStage () {
        _resultAnimator.Play (_exitResultAnimName, 0, 0);
        LifeProgressManager.instance.HideProgress ();
    }

    public void InitStage () {
        // HidePrePart ();
        switch (_stage) {
            case Stage.MAIN_MENU:
                InitMainMenuStage ();
                break;
            case Stage.GAMING:
                InitGamingStage ();
                break;
            case Stage.RESULT_MENU:
                InitResultMenuStage ();
                break;
        }
    }

    private void InitMainMenuStage () {
        _mainMenuPart.SetActive (true);
        _mainMenuAnimator.Play (_initMainMenuAnimName, 0, 0);
        Instantiate (_cubeBoomPrefab, _titleTransform).transform.localPosition = new Vector3 (0, 0, 0);
        Instantiate (_startBtnPrefab, _startBtnTransform).transform.localPosition = new Vector3 (0, 0, 0);
    }

    private void InitGamingStage () {
        _gameingPart.SetActive (true);
        _gamingAnimator.Play (_initGamingAnimName, 0, 0);
        EffectManager.instance.Reset ();
    }

    private void InitResultMenuStage () {
        _resultMenuPart.SetActive (true);
        _resultAnimator.Play (_initResultAnimName, 0, 0);
    }
    #endregion

    #region Function Part
    public void EnterMainMenu () {
        SwitchStage (Stage.MAIN_MENU);
    }
    public void EnterGaming () {
        SwitchStage (Stage.GAMING);
    }

    public void EndGameEnterResultMenu () {
        ScoreManager.instance.CalProgress ();
        SwitchStage (Stage.RESULT_MENU);
    }

    private void HideAllPart () {
        _mainMenuPart.SetActive (false);
        _gameingPart.SetActive (false);
        _resultMenuPart.SetActive (false);
    }

    public void HidePrePart () {
        switch (_preStage) {
            case Stage.NONE:
                break;
            case Stage.MAIN_MENU:
                _mainMenuPart.SetActive (false);
                break;
            case Stage.GAMING:
                _gameingPart.SetActive (false);
                break;
            case Stage.RESULT_MENU:
                _resultMenuPart.SetActive (false);
                break;
        }
    }

    #endregion
}