using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTriangleRequest : Request {
    #region Instance part
    #endregion

    #region Show In Inspector
    #endregion

    #region Hide In Inspector
    #endregion

    #region Init Part
    public AddTriangleRequest () {
        _requestType = RequestType.ADD_TRIANGLE;
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
        return true;
    }

    public override void Apply () {
        base.Apply ();
        EffectManager.instance.AddTriangleEffect ();
    }
    #endregion
}