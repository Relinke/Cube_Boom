using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageAnimEvent : MonoBehaviour {
    #region Instance part
    #endregion

    #region Show In Inspector
    #endregion

    #region Hide In Inspector
    #endregion

    #region Init Part
    #endregion

    #region Update Part

    #endregion

    #region Collision Part

    #endregion

    #region Function Part
    private void InitStageAnimEvent () {
        StageManager.instance.InitStage ();
    }

    private void GenerateCubeEvent () {
        ScoreManager.instance.Reset ();
        TriangleManager.instance.InitTriangle ();
        CubeManager.instance.StartGenerateCube ();
    }

    private void HidePrePart () {
        StageManager.instance.HidePrePart ();
    }

    private void ShowProgress () {
        LifeProgressManager.instance.ShowProgress ();
    }

    private void ShowAllBtnInMM () {
        UIManager.instance.ShowAllBtnInMM ();
    }

    private void HideAllBtnInMM () {
        UIManager.instance.HideAllBtnInMM ();
    }

    private void ShowAllBtnInRM () {
        UIManager.instance.ShowAllBtnInRM ();
    }

    private void HideAllBtnInRM () {
        UIManager.instance.HideAllBtnInRM ();
    }
    #endregion
}