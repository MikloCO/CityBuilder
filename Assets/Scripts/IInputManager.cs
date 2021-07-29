using System;
using UnityEngine;

public interface IInputManager
{
    LayerMask mouseInputMask { get; set; }

    /***************************** Add Listeners *****************************/
    void AddListenerOnPointerSecondChangeEvent(Action<Vector3> listener);
    void AddListenerOnPointerUpEvent(Action listener);
    void AddListenerOnPointerChangeEvent(Action<Vector3> listener);
    void AddListenerOnPointerSecondDownEvent(Action<Vector3> listener);
    void AddListenerOnPointerSecondUpEvent(Action listener);

    /***************************** Remove Listeners *****************************/
    void RemoveListenerOnPointerOnChangeEvent(Action<Vector3> listener);
    void RemoveListenerOnpointerSecondDownEvent(Action<Vector3> listener);
    void RemoveListenerOnPointerSecondUpEvent(Action listener);
    void RemoveListenerOnPointerUpEvent(Action listener);
    void RemoveListenerOnPointerChangeEvent(Action<Vector3> listener);
}