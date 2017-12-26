/// <summary>
/// This is a manager script using instance mode.
/// The function of this is managing cube in game, like generating, etc.
/// This also need calculating boomNum of new cube by current score.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class CubeManager : MonoBehaviour {
    #region Instance part
    public static CubeManager instance;
    #endregion

    #region Enum Part
    public enum ManagerState {
        NONE,
        GENERATING,
        SETTLEMENT,
        HIDING
    }
    #endregion

    #region Show In Inspector
    [Header ("Generate Setting")]
    [SerializeField]
    private Transform _generateParent;
    [SerializeField]
    private GameObject _originPointObj;

    [SerializeField]
    private Vector2 _cubeOffset = new Vector2 (176, 176);

    [SerializeField]
    private float _generateDelay = 0.25f;

    [SerializeField]
    private float _hidingDelay = 0.1f;

    [Header ("Random Setting")]
    [SerializeField]
    private List<float> _baseProbablity;

    [Header ("Prefab")]
    [SerializeField]
    private Cube _cubePrefab;
    #endregion

    #region Hide In Inspector
    private const int _ROW_COUNT = 5;
    private const int _COLUMN_COUNT = 5;
    private Cube[, ] _cubeArray = new Cube[_ROW_COUNT, _COLUMN_COUNT];

    private float _timer = 0;
    private int _cubeCount = 0;
    private int _destroyCount = 0;

    private ManagerState _generatingState = ManagerState.NONE;
    #endregion

    #region Get Part
    public ManagerState Get_generatingState () {
        return _generatingState;
    }

    public int Get_cubeCount () {
        return _cubeCount;
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
    private void Update () {
        switch (_generatingState) {
            case ManagerState.NONE:
                UpdatedNone ();
                break;
            case ManagerState.GENERATING:
                UpdatedGenerating ();
                break;
            case ManagerState.SETTLEMENT:
                UpdatedSettlement ();
                break;
            case ManagerState.HIDING:
                UpdatedHiding ();
                break;
        }
    }

    private void UpdatedNone () {

    }

    private void UpdatedGenerating () {
        if (_cubeCount >= _ROW_COUNT * _COLUMN_COUNT) {
            SwitchManagerState (ManagerState.NONE);
            return;
        } else {
            if (_timer >= _generateDelay) {
                // int r = _cubeCount / _ROW_COUNT;
                // int c = _cubeCount % _ROW_COUNT;
                int r = Random.Range (0, _ROW_COUNT);
                int c = Random.Range (0, _COLUMN_COUNT);
                while (_cubeArray[r, c]) {
                    r = Random.Range (0, _ROW_COUNT);
                    c = Random.Range (0, _COLUMN_COUNT);
                }
                GenerateCube (r, c);
                _timer = 0f;
            } else {
                _timer += Time.deltaTime;
            }
        }
    }

    private void UpdatedSettlement () {
        for (int i = 0; i < _ROW_COUNT; ++i) {
            for (int j = 0; j < _COLUMN_COUNT; ++j) {
                if (!_cubeArray[i, j]) {
                    continue;
                }
                if (_cubeArray[i, j].Get_cubeState () != Cube.CubeState.NONE) {
                    return;
                }
            }
        }
        if (TriangleManager.instance.GetEnabledTriangleCount () <= 0) {
            if (AdManager.instance.Get_hasUsedExtraTriangle () ||
                Application.internetReachability == NetworkReachability.NotReachable) {
                StartHideAllCube ();
                TriangleManager.instance.StartHideAllTriangle ();
            } else {
                AdManager.instance.ShowTip ();
            }
        } else {
            StartGenerateCube ();
        }
    }

    private void UpdatedHiding () {
        if (_cubeCount <= 0) {
            if (!FindObjectOfType<Cube> ()) {
                SwitchManagerState (ManagerState.NONE);
            }
            return;
        } else {
            if (_timer >= _generateDelay) {
                // int r = _cubeCount / _ROW_COUNT;
                // int c = _cubeCount % _ROW_COUNT;
                int r = Random.Range (0, _ROW_COUNT);
                int c = Random.Range (0, _COLUMN_COUNT);
                while (!_cubeArray[r, c]) {
                    r = Random.Range (0, _ROW_COUNT);
                    c = Random.Range (0, _COLUMN_COUNT);
                }
                HideCube (r, c);
                _timer = 0f;

            } else {
                _timer += Time.deltaTime;
            }
        }
    }
    #endregion

    #region Collision Part

    #endregion

    #region State Part
    private void SwitchManagerState (ManagerState state) {
        if (_generatingState == state) {
            return;
        }
        switch (_generatingState) {
            case ManagerState.NONE:
                ExitNoneState ();
                break;
            case ManagerState.GENERATING:
                ExitGeneratingState ();
                break;
            case ManagerState.SETTLEMENT:
                ExitSettlementState ();
                break;
            case ManagerState.HIDING:
                ExitHidingState ();
                break;
        }
        _generatingState = state;
        switch (_generatingState) {
            case ManagerState.NONE:
                InitNoneState ();
                break;
            case ManagerState.GENERATING:
                InitGeneratingState ();
                break;
            case ManagerState.SETTLEMENT:
                InitSettlementState ();
                break;
            case ManagerState.HIDING:
                InitHidingState ();
                break;
        }
    }

    private void ExitNoneState () {

    }

    private void ExitGeneratingState () {

    }

    private void ExitSettlementState () {
        AudioManager.instance.ResetPiano ();
    }

    private void ExitHidingState () {
        StageManager.instance.EndGameEnterResultMenu ();
    }

    private void InitNoneState () {
        ScoreManager.instance.ResetCombo ();
        if (_cubeCount > 0 && TriangleManager.instance.GetEnabledTriangleCount () <= 0) {
            if (AdManager.instance.Get_hasUsedExtraTriangle () ||
                Application.internetReachability == NetworkReachability.NotReachable) {
                StartHideAllCube ();
                TriangleManager.instance.StartHideAllTriangle ();
            } else {
                AdManager.instance.ShowTip ();
            }
        }
    }

    private void InitGeneratingState () {
        _timer = _generateDelay;
        if (_cubeCount >= _ROW_COUNT * _COLUMN_COUNT) {
            SwitchManagerState (ManagerState.NONE);
            return;
        }
    }

    private void InitSettlementState () {

    }

    private void InitHidingState () {
        _timer = _hidingDelay;
    }

    public void SettleTap () {
        SwitchManagerState (ManagerState.SETTLEMENT);
    }
    #endregion

    #region Function Part

    private void GenerateCube (int r, int c) {
        ++_cubeCount;
        Cube cube = Instantiate (_cubePrefab, _generateParent);
        int boomNum = GetRandomBoomNum ();
        cube.Init (boomNum, r, c);
        cube.transform.localPosition = _originPointObj.transform.localPosition +
            new Vector3 (c * _cubeOffset.y, r * _cubeOffset.x, 0);
        _cubeArray[r, c] = cube;
    }

    private void HideCube (int r, int c) {
        if (_cubeArray[r, c]) {
            _cubeArray[r, c].HideCube ();
            _cubeArray[r, c] = null;
            --_cubeCount;
        }
    }

    private int GetRandomBoomNum () {
        int score = ScoreManager.instance.Get_score ();
        float max = 0;
        for (int i = 0; i < _baseProbablity.Count; ++i) {
            max += _baseProbablity[i];
        }
        float r = Random.Range (0f, max);
        float baseProbablity = 0;
        for (int i = 0; i < _baseProbablity.Count; ++i) {
            baseProbablity += _baseProbablity[i];
            if (r <= baseProbablity) {
                return i + 1;
            }
        }
        return 1;
    }

    public void BoomAt (int r, int c) {
        if (r >= _ROW_COUNT || r < 0 || c >= _COLUMN_COUNT || c < 0) {
            return;
        }
        ScoreManager.instance.AddBoomScore (_cubeArray[r, c].Get_boomNum ());
        _cubeArray[r, c] = null;
        --_cubeCount;
        for (int i = r - 1; i <= r + 1; ++i) {
            for (int j = c - 1; j <= c + 1; ++j) {
                if (i >= _ROW_COUNT || i < 0 ||
                    j >= _COLUMN_COUNT || j < 0 ||
                    (i == r && j == c)) {
                    continue;
                }
                if (_cubeArray[i, j]) {
                    _cubeArray[i, j].InsideTap ();
                }
            }
        }
    }

    public void StartHideAllCube () {
        SwitchManagerState (ManagerState.HIDING);
    }

    public void StartGenerateCube () {
        SwitchManagerState (ManagerState.GENERATING);
    }
    #endregion
}