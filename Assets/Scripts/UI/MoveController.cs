using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float value = 0;
    public float min = -1, max = 1;
    public float gravity = 10;
    private bool pressed = false;

    private RectTransform rt;
    public RectTransform pin;

    private void Awake()
    {
        rt = this.GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector2 touch;
        if (pressed && TryGetTouch(out touch))
        {
            value = Mathf.InverseLerp(rt.rect.x, rt.rect.xMax, touch.x);
            value = Mathf.Lerp(min, max, value);
        }
        else
        {
            value = Mathf.MoveTowards(value, 0, gravity * Time.deltaTime);
        }

        pin.localPosition = new Vector3(Mathf.InverseLerp(min, max, value) * rt.rect.width, pin.localPosition.y, pin.localPosition.z);
    }

    public void SetPressed(bool pressed)
    {
        this.pressed = pressed;
    }

    private bool TryGetTouch(out Vector2 touch)
    {
        
        for (int i = 0; i < Input.touchCount; i++)
        {
            touch = rt.InverseTransformPoint(Input.GetTouch(i).position);
            if (rt.rect.Contains(touch))
            {
                return true;
            }
        }
        // mouse case
        if(Input.mousePresent)
        {
            touch = rt.InverseTransformPoint(Input.mousePosition);
            if (rt.rect.Contains(touch))
            {
                return true;
            }
        }

        // fail case
        touch = Vector2.zero;
        return false;
    }
}
