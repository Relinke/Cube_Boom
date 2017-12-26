/// <summary>
/// This is a manager script using instance mode.
/// The function of this is to calculate and save score, mainly calculate score
/// by the boomNum of cube and combo count, combo means how many cubes boom in one 
/// player tap turn.
/// Using _currShowScore to show the progress of inscreasing score.
/// This also need to manage result menu.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    #region Instance part
    public static ScoreManager instance;
    #endregion

    #region Show In Inspector
    [Header ("Score Setting")]
    [SerializeField]
    private int _singleCubeScore = 1;

    [SerializeField]
    private int _baseComboCoeffcient = 2;

    [SerializeField]
    private int _addComboCoeffcient = 1;

    [SerializeField]
    private float _progressCoe = 0.000001f;

    [Header ("Component Part")]
    [SerializeField]
    private Text _scoreTextInGame;

    [SerializeField]
    private Text _scoreTextInResult;

    #endregion

    #region Hide In Inspector
    private int _score = 0;
    private int _combo = 0;
    private int _currShowScore = 0;
    #endregion

    #region Get Part
    public int Get_score () {
        return _score;
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
    private void FixedUpdate () {
        FixUpdatedShowScore ();
    }

    private void FixUpdatedShowScore () {
        if (_currShowScore != _score) {
            int gap = _score - _currShowScore;
            if (gap <= 10) {
                ++_currShowScore;
            } else if (gap <= 100) {
                _currShowScore += 10;
            } else if (gap <= 1000) {
                _currShowScore += 100;
            } else if (gap <= 10000) {
                _currShowScore += 1000;
            }
            if (_currShowScore > _score) {
                _currShowScore = _score;
            }
            UpdateUI ();
        }
    }
    #endregion

    #region Collision Part

    #endregion

    #region Function Part
    public void Reset () {
        _score = 0;
        _combo = 0;
    }

    public void ResetCombo () {
        _combo = 0;
    }

    public void AddBoomScore (int boomNum) {
        int score = BoomBonus (boomNum);
        if (CubeManager.instance.Get_cubeCount () == 1) {
            score += ClearBonus ();
        }
        _score += score;
        ++_combo;
    }

    private void UpdateUI () {
        _scoreTextInGame.text = _currShowScore.ToString ();
        _scoreTextInResult.text = _currShowScore.ToString ();
    }

    private int BoomBonus (int boomNum) {
        int score = boomNum * _singleCubeScore;
        if (_combo != 0) {
            score *= ((_combo * _addComboCoeffcient) + _baseComboCoeffcient);
            EffectManager.instance.ComboEffectRequest (_combo + 1);
        }
        EffectManager.instance.AddScoreEffectRequest (score);
        return score;
    }

    private int ClearBonus () {
        TriangleManager.instance.AddOneTriangle ();
        EffectManager.instance.AddTriangleEffectRequest ();
        return 0;
    }

    public void CalProgress () {
        //  / 100000f;
        float score = ((float) _score) * _progressCoe;
        LifeProgressManager.instance.AddProgress (score);
    }
    #endregion
}