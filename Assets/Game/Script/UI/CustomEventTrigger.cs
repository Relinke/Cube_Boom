using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomEventTrigger : EventTrigger {

    public delegate void VoidDelegate (GameObject obj);
    public VoidDelegate onPointerClick;
    public VoidDelegate onPointerEnter;

    public VoidDelegate onPointerExit;

    public VoidDelegate onPointerUp;

    public VoidDelegate onPointerDown;

    public VoidDelegate onBeginDrag;

    public VoidDelegate onDrag;

    public VoidDelegate onEndDrag;

    public VoidDelegate onDrop;

    public VoidDelegate onScroll;

    public VoidDelegate onUpdateSelected;

    public VoidDelegate onSelect;

    public VoidDelegate onDeselect;

    public VoidDelegate onMove;

    public VoidDelegate onSubmit;

    public VoidDelegate onCancel;

    public static CustomEventTrigger Get (GameObject obj) {
        CustomEventTrigger CET = obj.GetComponent<CustomEventTrigger> ();
        if (!CET) {
            CET = obj.AddComponent<CustomEventTrigger> ();
        }
        return CET;
    }

    public override void OnPointerClick (PointerEventData eventData) {
        base.OnPointerClick (eventData);
        if (onPointerClick != null) {
            onPointerClick (gameObject);
        }
    }

    public override void OnPointerEnter (PointerEventData eventData) {
        base.OnPointerEnter (eventData);
        if (onPointerEnter != null) {
            onPointerEnter (gameObject);
        }
    }

    public override void OnPointerExit (PointerEventData eventData) {
        base.OnPointerExit (eventData);
        if (onPointerExit != null) {
            onPointerExit (gameObject);
        }
    }

    public override void OnPointerDown (PointerEventData eventData) {
        base.OnPointerDown (eventData);
        if (onPointerDown != null) {
            onPointerDown (gameObject);
        }
    }

    public override void OnPointerUp (PointerEventData eventData) {
        base.OnPointerUp (eventData);
        if (onPointerUp != null) {
            onPointerUp (gameObject);
        }
    }

    public override void OnBeginDrag (PointerEventData eventData) {
        base.OnBeginDrag (eventData);
        if (onBeginDrag != null) {
            onBeginDrag (gameObject);
        }
    }

    public override void OnDrag (PointerEventData eventData) {
        base.OnDrag (eventData);
        if (onDrag != null) {
            onDrag (gameObject);
        }
    }

    public override void OnEndDrag (PointerEventData eventData) {
        base.OnEndDrag (eventData);
        if (onEndDrag != null) {
            onEndDrag (gameObject);
        }
    }

    public override void OnDrop (PointerEventData eventData) {
        base.OnDrop (eventData);
        if (onDrop != null) {
            onDrop (gameObject);
        }
    }

    public override void OnScroll (PointerEventData eventData) {
        base.OnScroll (eventData);
        if (onScroll != null) {
            onScroll (gameObject);
        }
    }

    public override void OnUpdateSelected (BaseEventData eventData) {
        base.OnUpdateSelected (eventData);
        if (onUpdateSelected != null) {
            onUpdateSelected (gameObject);
        }
    }

    public override void OnSelect (BaseEventData eventData) {
        base.OnSelect (eventData);
        if (onSelect != null) {
            onSelect (gameObject);
        }
    }

    public override void OnDeselect (BaseEventData eventData) {
        base.OnDeselect (eventData);
        if (onDeselect != null) {
            onDeselect (gameObject);
        }
    }

    public override void OnMove (AxisEventData eventData) {
        base.OnMove (eventData);
        if (onMove != null) {
            onMove (gameObject);
        }
    }

    public override void OnSubmit (BaseEventData eventData) {
        base.OnSubmit (eventData);
        if (onSubmit != null) {
            onSubmit (gameObject);
        }
    }

    public override void OnCancel (BaseEventData eventData) {
        base.OnCancel (eventData);
        if (onCancel != null) {
            onCancel (gameObject);
        }
    }
}