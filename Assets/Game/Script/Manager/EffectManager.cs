/// <summary>
/// This is a manager script using instance mode.
/// Mainly manager ui effect of game, like add score effect, add triangle effect, etc.
/// Use dynamic object pool to manager effect
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour {
    #region Instance part
    public static EffectManager instance;
    #endregion

    #region Show In Inspector
    [Header ("Generate Setting")]
    [SerializeField]
    private Transform _addScoreEffOrigin;
    [SerializeField]
    private Transform _addTriEffOrigin;
    [SerializeField]
    private Transform _comboEffOrigin;

    [Header ("Anim Setting")]
    [SerializeField]
    private string _effectAnimName = "Effect";

    [Header ("Prefab Setting")]
    [SerializeField]
    private Animator _addScoreEffPrefab;
    [SerializeField]
    private Animator _addTriEffPrefab;
    [SerializeField]
    private Animator _addComboEffPrefab;
    #endregion

    #region Hide In Inspector
    private List<Request> _requestList = new List<Request> ();
    private List<Animator> _addScoreEffList = new List<Animator> ();
    private List<Animator> _addTriEffList = new List<Animator> ();
    private List<Animator> _comboEffList = new List<Animator> ();
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
        DealWithRequest ();
    }
    #endregion

    #region Collision Part

    #endregion

    #region Function Part
    public void AddScoreEffectRequest (float amount) {
        _requestList.Add (new AddScoreRequest (amount));
    }

    public void AddTriangleEffectRequest () {
        _requestList.Add (new AddTriangleRequest ());
    }

    public void ComboEffectRequest (int amount) {
        _requestList.Add (new ComboRequest (amount));
    }

    private void MergeRequest () {
        for (int i = _requestList.Count - 1; i >= 1; --i) {
            for (int j = i - 1; j >= 0; --j) {
                if (_requestList[i].IsSameType (_requestList[j])) {
                    _requestList[j].MergeRequest (_requestList[i]);
                    _requestList.RemoveAt (i);
                    break;
                }
            }
        }
    }

    private void DealWithRequest () {
        if (_requestList.Count <= 0) {
            return;
        }
        MergeRequest ();
        for (int i = 0; i < _requestList.Count; ++i) {
            _requestList[i].Apply ();
        }
        _requestList.Clear ();
    }

    public void AddScoreEffect (float amount) {
        Animator animator = GetDisactiveAddScoreEff ();
        animator.Play (_effectAnimName, 0, 0);
        animator.transform.GetChild (0).GetComponent<Text> ().text = "+" + amount.ToString ();
    }

    private Animator GetDisactiveAddScoreEff () {
        if (_addScoreEffList.Count > 0) {
            for (int i = 0; i < _addScoreEffList.Count; ++i) {
                if (!_addScoreEffList[i].GetCurrentAnimatorStateInfo (0).IsName (_effectAnimName)) {
                    return _addScoreEffList[i];
                }
            }
        }
        _addScoreEffList.Add (Instantiate (_addScoreEffPrefab, _addScoreEffOrigin));
        _addScoreEffList[_addScoreEffList.Count - 1].transform.localPosition = new Vector3 ();
        return _addScoreEffList[_addScoreEffList.Count - 1];
    }

    public void AddTriangleEffect () {
        GetDisactiveAddTriEff ().Play (_effectAnimName, 0, 0);
    }

    private Animator GetDisactiveAddTriEff () {
        if (_addTriEffList.Count > 0) {
            for (int i = 0; i < _addTriEffList.Count; ++i) {
                if (!_addTriEffList[i].GetCurrentAnimatorStateInfo (0).IsName (_effectAnimName)) {
                    return _addTriEffList[i];
                }
            }
        }
        _addTriEffList.Add (Instantiate (_addTriEffPrefab, _addTriEffOrigin));
        _addTriEffList[_addTriEffList.Count - 1].transform.localPosition = new Vector3 ();
        return _addTriEffList[_addTriEffList.Count - 1];
    }

    public void ComboEffect (int amount) {
        Animator animator = GetDisactiveComboEff ();
        animator.Play (_effectAnimName, 0, 0);
        animator.transform.GetChild (0).GetComponent<Text> ().text = "X" + amount.ToString () + "!";
    }

    private Animator GetDisactiveComboEff () {
        if (_comboEffList.Count > 0) {
            return _comboEffList[0];
            for (int i = 0; i < _comboEffList.Count; ++i) {
                if (!_comboEffList[i].GetCurrentAnimatorStateInfo (0).IsName (_effectAnimName)) {
                    return _comboEffList[i];
                }
            }
        }
        _comboEffList.Add (Instantiate (_addComboEffPrefab, _comboEffOrigin));
        _comboEffList[_comboEffList.Count - 1].transform.localPosition = new Vector3 ();
        return _comboEffList[_comboEffList.Count - 1];
    }

    public void Reset () {
        for (int i = _addScoreEffList.Count - 1; i >= 0; --i) {
            Destroy (_addScoreEffList[i]);
        }
        _addScoreEffList.Clear ();

        for (int i = _addTriEffList.Count - 1; i >= 0; --i) {
            Destroy (_addTriEffList[i]);
        }
        _addTriEffList.Clear ();

        for (int i = _comboEffList.Count - 1; i >= 0; --i) {
            Destroy (_comboEffList[i]);
        }
        _comboEffList.Clear ();
    }
    #endregion
}