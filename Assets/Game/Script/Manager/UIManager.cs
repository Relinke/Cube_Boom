/// <summary>
/// This is a manager script using instance mode.
/// Mainly manager UI in game, like button.
/// Because button's animation has delay, so function must start after animation end.
/// I add _btnDelay to do this job.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class UIManager : MonoBehaviour {
    #region Instance part
    public static UIManager instance;
    #endregion

    #region Enum Part
    private enum UI_State {
        NONE,
        SHOW_BTN_IN_MM,
        HIDE_BTN_IN_MM,
        SHOW_BTN_IN_RM,
        HIDE_BTN_IN_RM
    }
    #endregion

    #region Show In Inspector
    [Header ("Button Setting")]
    [SerializeField]
    private float _btnTransitionDur = 0.5f;

    [Header ("Alpha Setting")]
    [SerializeField]
    private float _btnAlpha = 1;
    [SerializeField]
    private float _textAlpha = 0.83f;
    [SerializeField]
    private float _shadowAlpha = 0.53f;

    [Header ("Main Menu Button")]
    [SerializeField]
    private Button _startBtnInMM;
    [SerializeField]
    private Button _rankBtnInMM;
    [SerializeField]
    private Button _optionBtnInMM;
    [SerializeField]
    private Button _quitBtnInMM;

    [Header ("Advertisement Tip")]
    [SerializeField]
    private Button _endGameBtnInAdTip;
    [SerializeField]
    private Button _watchAdBtnInAdTip;

    [Header ("Result Menu Button")]
    [SerializeField]
    private Button _restartBtnInRM;
    [SerializeField]
    private Button _returnBtnInRM;

    #endregion

    #region Hide In Inspector
    private UI_State _uiState = UI_State.NONE;
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
        switch (_uiState) {
            case UI_State.SHOW_BTN_IN_MM:
                UpdateShowBtnInMM ();
                break;
            case UI_State.HIDE_BTN_IN_MM:
                UpdateHideBtnInMM ();
                break;
            case UI_State.SHOW_BTN_IN_RM:
                UpdateShowBtnInRM ();
                break;
            case UI_State.HIDE_BTN_IN_RM:
                UpdateHideBtnInRM ();
                break;
        }
    }

    private void UpdateShowBtnInMM () {
        float btnAlpha = GetBtnAlpha (_startBtnInMM);
        if (btnAlpha < _btnAlpha) {
            SetBtnAlpha (_startBtnInMM, true);
            SetBtnAlpha (_rankBtnInMM, true);
            SetBtnAlpha (_optionBtnInMM, true);
            SetBtnAlpha (_quitBtnInMM, true);
        } else {
            float shadowAlpha = GetBtnShadowAlpha (_startBtnInMM);
            if (shadowAlpha < _shadowAlpha) {
                shadowAlpha += (Time.deltaTime / _btnTransitionDur);
                SetBtnShadowAlpha (_startBtnInMM, true);
                SetBtnShadowAlpha (_rankBtnInMM, true);
                SetBtnShadowAlpha (_optionBtnInMM, true);
                SetBtnShadowAlpha (_quitBtnInMM, true);
            }
        }
    }
    private void UpdateHideBtnInMM () {

        float shadowAlpha = GetBtnShadowAlpha (_startBtnInMM);
        if (shadowAlpha > 0) {
            shadowAlpha -= (Time.deltaTime / _btnTransitionDur);
            SetBtnShadowAlpha (_startBtnInMM, false);
            SetBtnShadowAlpha (_rankBtnInMM, false);
            SetBtnShadowAlpha (_optionBtnInMM, false);
            SetBtnShadowAlpha (_quitBtnInMM, false);
        } else {
            float btnAlpha = GetBtnAlpha (_startBtnInMM);
            if (btnAlpha > 0) {
                btnAlpha -= (Time.deltaTime / _btnTransitionDur);
                SetBtnAlpha (_startBtnInMM, false);
                SetBtnAlpha (_rankBtnInMM, false);
                SetBtnAlpha (_optionBtnInMM, false);
                SetBtnAlpha (_quitBtnInMM, false);
            }
        }
    }
    private void UpdateShowBtnInRM () {

    }
    private void UpdateHideBtnInRM () {

    }
    #endregion

    #region Collision Part

    #endregion

    #region State Part
    private void SwitchState (UI_State state) {
        if (_uiState == state) {
            return;
        }
        _uiState = state;
        switch (_uiState) {
            case UI_State.SHOW_BTN_IN_MM:
                InitShowBtnInMM ();
                break;
            case UI_State.HIDE_BTN_IN_MM:
                InitHideBtnInMM ();
                break;
            case UI_State.SHOW_BTN_IN_RM:
                InitShowBtnInRM ();
                break;
            case UI_State.HIDE_BTN_IN_RM:
                InitHideBtnInRM ();
                break;
        }
    }

    private void InitShowBtnInMM () {

    }

    private void InitHideBtnInMM () {

    }

    private void InitShowBtnInRM () {

    }

    private void InitHideBtnInRM () {

    }
    #endregion

    #region Function Part
    private void SetBtnAlpha (Button button, bool isIncrease) {
        Image btnImage = button.GetComponent<Image> ();
        if (!btnImage) {
            return;
        }
        float sign = 0;
        Color btnC = btnImage.color;
        if (isIncrease) {
            sign = 1;
            if (btnC.a >= _btnAlpha) {
                return;
            }
        } else {
            sign = -1;
            if (btnC.a <= 0) {
                return;
            }
        }
        btnC.a += sign * Time.deltaTime * _btnAlpha / _btnTransitionDur;
        btnImage.color = btnC;
        SetTextAlpha (button, isIncrease);
    }

    private void SetBtnShadowAlpha (Button button, bool isIncrease) {
        Transform shadow = button.transform.parent.Find ("Btn Shadow");
        if (!shadow) {
            return;
        }
        Image shadowImage = shadow.GetComponent<Image> ();
        float sign = 0;
        Color colorS = shadowImage.color;
        if (isIncrease) {
            sign = 1;
            if (colorS.a >= _shadowAlpha) {
                return;
            }
        } else {
            sign = -1;
            if (colorS.a <= 0) {
                return;
            }
        }
        colorS.a += sign * Time.deltaTime * _shadowAlpha / _btnTransitionDur;
        shadowImage.color = colorS;
    }

    private void SetTextAlpha (Button button, bool isIncrease) {
        Transform textT = button.transform.Find ("Text");
        if (!textT) {
            return;
        }
        Text text = textT.GetComponent<Text> ();
        float sign = 0;
        Color textC = text.color;
        if (isIncrease) {
            sign = 1;
            if (textC.a >= _textAlpha) {
                return;
            }
        } else {
            sign = -1;
            if (textC.a <= 0) {
                return;
            }
        }
        textC.a += sign * Time.deltaTime * _textAlpha / _btnTransitionDur;
        text.color = textC;
    }

    private float GetBtnAlpha (Button button) {
        Image btnImage = button.GetComponent<Image> ();
        if (!btnImage) {
            return -10f;
        }
        return btnImage.color.a;
    }

    private float GetBtnShadowAlpha (Button button) {
        Transform shadow = button.transform.parent.Find ("Btn Shadow");
        if (!shadow) {
            return -10f;
        }
        return shadow.GetComponent<Image> ().color.a;
    }
    #endregion

    #region Main Menu UI
    public void OnMainMenuStartBtn () {
        StageManager.instance.EnterGaming ();
    }

    public void OnMainMenuOptionBtn () {
    }

    public void OnMainMenuRankBtn () {
    }

    public void OnMainMenuQuitBtn () {
    }

    public void ShowAllBtnInMM () {
        SwitchState (UI_State.SHOW_BTN_IN_MM);
    }

    public void HideAllBtnInMM () {
        SwitchState (UI_State.HIDE_BTN_IN_MM);
    }
    #endregion

    #region Advertisement Tip Part
    public void OnAdTipEndGameBtn () {
        AudioManager.instance.PlayBtnSFX ();
        AdManager.instance.EndGame ();
    }

    public void OnAdTipWatchAdBtn () {
        AudioManager.instance.PlayBtnSFX ();
        AdManager.instance.ContinueGame ();
    }
    #endregion

    #region Result UI
    public void OnResultRestartBtn () {
        StageManager.instance.EnterGaming ();
        AudioManager.instance.PlayBtnSFX ();
    }

    public void OnResultReturnBtn () {
        StageManager.instance.EnterMainMenu ();
        AudioManager.instance.PlayBtnSFX ();
    }

    public void ShowAllBtnInRM () {
        SwitchState (UI_State.SHOW_BTN_IN_RM);
    }

    public void HideAllBtnInRM () {
        SwitchState (UI_State.HIDE_BTN_IN_RM);
    }
    #endregion
}