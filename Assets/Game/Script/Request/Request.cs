using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Request {
    #region Instance part
    #endregion

    #region Enum Part
    public enum RequestType {
        NONE,
        ADD_SCORE,
        ADD_TRIANGLE,
        COMBO
    }
    #endregion

    #region Show In Inspector
    #endregion

    #region Hide In Inspector
    protected RequestType _requestType = RequestType.NONE;
    #endregion

    #region Init Part
    protected Request () { }
    #endregion

    #region Update Part

    #endregion

    #region Collision Part

    #endregion

    #region Function Part
    public bool IsSameType (Request request) {
        if (request == this) {
            return true;
        }

        if (request._requestType == _requestType) {
            return true;
        }

        return false;
    }

    public virtual bool MergeRequest (Request request) {
        if (request == this) {
            return false;
        }
        return true;
    }

    public virtual void Apply () {

    }
    #endregion
}