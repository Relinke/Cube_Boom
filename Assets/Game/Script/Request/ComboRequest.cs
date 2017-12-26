using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboRequest : Request {
    #region Instance part
    #endregion

    #region Show In Inspector
    #endregion

    #region Hide In Inspector
    private int _amount;
    #endregion

    #region Init Part
    public ComboRequest (int amount) {
        _requestType = RequestType.COMBO;
        _amount = amount;
    }
    #endregion

    #region Update Part

    #endregion

    #region Collision Part

    #endregion

    #region Function Part
    public override bool MergeRequest (Request request) {
        if (!base.MergeRequest (request)) {
            return false;
        }
        if (_amount < ((ComboRequest) request)._amount) {
            _amount = ((ComboRequest) request)._amount;
        }
        return true;
    }

    public override void Apply () {
        base.Apply ();
        EffectManager.instance.ComboEffect (_amount);
    }
    #endregion
}