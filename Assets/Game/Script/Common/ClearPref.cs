using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPref : MonoBehaviour {
    #region Instance part
    #endregion

    #region Show In Inspector
    #endregion

    #region Hide In Inspector
    #endregion

    #region Init Part
    private void Awake () {
        Init ();
    }

    private void Init () {
        PlayerPrefs.DeleteAll ();
    }
    #endregion

    #region Update Part

    #endregion

    #region Collision Part

    #endregion

    #region Function Part

    #endregion
}