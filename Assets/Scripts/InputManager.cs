using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class InputManager : MonoBehaviour, IInputManager
{
    private Action<Vector3> OnPointerSecondChangeHandler;
    private Action OnPointerSecondUpHandler;
    private Action<Vector3> OnPointerDownHandler;

    private Action OnPointerUpHandler;
    private Action<Vector3> OnPointerChangeHandler;

    private LayerMask mouseInputMask;

    LayerMask IInputManager.mouseInputMask { get => mouseInputMask; set => mouseInputMask = value; }

    void Update()
    {
        GetCursorInput();
        GetPanningCursor();
    }

    private void GetCursorInput()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            CallActionOnPointer((position) => OnPointerDownHandler?.Invoke(position));
        }
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            CallActionOnPointer((position) => OnPointerChangeHandler?.Invoke(position));
        }
        if(Input.GetMouseButtonUp(0))
        {
            OnPointerUpHandler?.Invoke();
        }
    }

    private void CallActionOnPointer(Action<Vector3> action)
    {
        Vector3? position = GetMousePosition();

        if (position.HasValue)
        {
            action(position.Value);
            position = null;
        }
    }


    private Vector3? GetMousePosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        Vector3? position = null;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, mouseInputMask))
        {
            position = hit.point - transform.position;
        }

        return position;
    }

    private void GetPanningCursor()
    {
       if (Input.GetMouseButton(1))
        {
            var position = Input.mousePosition;
            OnPointerSecondChangeHandler?.Invoke(position);
        }
        if (Input.GetMouseButtonUp(1))
        {
            OnPointerSecondUpHandler?.Invoke(); 
        }
    }

    public void AddListenerOnPointerSecondChangeEvent(Action<Vector3> listener)
    {
        OnPointerDownHandler += listener;
    }

    public void RemoveListenerOnPointerOnChangeEvent(Action<Vector3> listener)
    {
        OnPointerDownHandler -= listener;
    }

    public void AddListenerOnPointerSecondDownEvent(Action<Vector3> listener)
    {
        OnPointerSecondChangeHandler += listener;
    }

    public void RemoveListenerOnpointerSecondDownEvent(Action<Vector3> listener)
    {
        OnPointerSecondChangeHandler -= listener;
    }

    public void AddListenerOnPointerSecondUpEvent(Action listener)
    {
        OnPointerSecondUpHandler += listener;
    }

    public void RemoveListenerOnPointerSecondUpEvent(Action listener)
    {
        OnPointerSecondUpHandler -= listener;
    }
    public void AddListenerOnPointerUpEvent(Action listener)
    {
        OnPointerUpHandler += listener;
    }

    public void RemoveListenerOnPointerUpEvent(Action listener)
    {
        OnPointerUpHandler -= listener;
    }
    public void AddListenerOnPointerChangeEvent(Action<Vector3> listener)
    {
        OnPointerChangeHandler += listener;
    }

    public void RemoveListenerOnPointerChangeEvent(Action<Vector3> listener)
    {
        OnPointerChangeHandler -= listener;
    }

}

