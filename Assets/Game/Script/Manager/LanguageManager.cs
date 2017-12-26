using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour {
    #region Instance part
    public static LanguageType LANGUAGE_TYPE = LanguageType.CHINESE;
    #endregion

    #region Enum Part
    public enum LanguageType {
        CHINESE,
        ENGLISH
    }
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
        InitStatic ();
    }

    private void InitStatic () {
        if (Application.systemLanguage == SystemLanguage.Chinese ||
            Application.systemLanguage == SystemLanguage.ChineseSimplified ||
            Application.systemLanguage == SystemLanguage.ChineseTraditional) {
            LANGUAGE_TYPE = LanguageType.CHINESE;
        } else {
            LANGUAGE_TYPE = LanguageType.ENGLISH;
        }
    }
    #endregion

    #region Update Part

    #endregion

    #region Collision Part

    #endregion

    #region Function Part

    #endregion
}