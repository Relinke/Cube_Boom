using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScoreRequest : Request {
    #region Instance part
    #endregion

    #region Show In Inspector
    #endregion

    #region Hide In Inspector
    private float _amount;
    #endregion

    #region Init Part
    public AddScoreRequest (float amount) {
        _requestType = RequestType.ADD_SCORE;
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
        _amount += ((AddScoreRequest) request)._amount;
        return true;
    }

    public override void Apply () {
        base.Apply ();
        EffectManager.instance.AddScoreEffect (_amount);
    }
    #endregion
}