using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonTri : MonoBehaviour {
    #region Instance part
    public static StartButtonTri instance;
    #endregion

    #region Show In Inspector
    [Header ("Component Setting")]
    [SerializeField]
    private Rigidbody2D _rigidbody;

    [SerializeField]
    private Button _finalButton;

    [SerializeField]
    private HingeJoint2D _hingeJoint;

    [SerializeField]
    private Image _pointImage;

    [SerializeField]
    private Image _triangleImage;
    #endregion

    #region Hide In Inspector
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
        } else {
            Destroy (instance.gameObject);
            instance = this;
        }
    }
    #endregion

    #region Update Part

    #endregion

    #region Collision Part

    #endregion

    #region Function Part
    public void FallOff () {
        if (_rigidbody.bodyType != RigidbodyType2D.Dynamic) {

        }
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _finalButton.enabled = true;
        _pointImage.raycastTarget = true;
        _triangleImage.raycastTarget = false;
        AudioManager.instance.PlayBtnSFX ();
    }

    public void FinalFallOff () {
        _hingeJoint.enabled = false;
        StageManager.instance.EnterGaming ();
        AudioManager.instance.PlayBtnSFX ();
    }
    #endregion
}