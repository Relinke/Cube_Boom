/// <summary>
/// This is a manager script using instance mode.
/// Mainly used for displaying advertisement.
/// When player has used up all triangle, this will ask player if they need one more triangle 
/// before end, if yes, player can have one more triangle, and continue playing, next time player
/// use up all triangle, there will play advertisement, and then return to result menu.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour {
    #region Instance part
    public static AdManager instance;
    #endregion

    #region Show In Inspector
    [Header ("Animation Setting")]
    [SerializeField]
    private Animator _adTipAnimator;
    [SerializeField]
    private string _initWithAdAnimName = "Init";
    [SerializeField]
    private string _disappearAnimName = "Disappear";
    #endregion

    #region Hide In Inspector
    private bool _hasUsedExtraTriangle = false;
    private bool _isShowing = false;
    #endregion

    #region Get Part
    public bool Get_hasUsedExtraTriangle () {
        return _hasUsedExtraTriangle;
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
        if (!instance) {
            instance = this;
        }
    }
    #endregion

    #region Update Part

    #endregion

    #region Collision Part

    #endregion

    #region Function Part
    public void ShowTip () {
        if (_isShowing) {
            return;
        }
        _isShowing = true;
        _adTipAnimator.gameObject.SetActive (true);
        _adTipAnimator.Play (_initWithAdAnimName, 0, 0);
    }

    public void EndGame () {
        _adTipAnimator.Play (_disappearAnimName, 0, 0);
        CubeManager.instance.StartHideAllCube ();
        TriangleManager.instance.StartHideAllTriangle ();
        _isShowing = false;
    }

    public void ContinueGame () {
        if (_hasUsedExtraTriangle) {
            return;
        }
        _adTipAnimator.Play (_disappearAnimName, 0, 0);
        TriangleManager.instance.AddOneTriangle ();
        _hasUsedExtraTriangle = true;
        CubeManager.instance.StartGenerateCube ();
        _isShowing = false;
    }

    public void ShowAD () {
        if (AdManager.instance.Get_hasUsedExtraTriangle ()) {
            ShowOptions options = new ShowOptions ();
            options.resultCallback = HandleShowADResult;
            Advertisement.Show ("video", options);
        }
    }

    private void HandleShowADResult (ShowResult result) {
        _hasUsedExtraTriangle = false;
    }
    #endregion
}