using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PieMenu : MonoBehaviour
{
    [SerializeField] ItemMenu item1, item2, item3, item4, item5, item6;

    [SerializeField] float threshold;

    Gamepad gamepad = Gamepad.current;

    void Update()
    {
        Vector2 stickL = gamepad.leftStick.ReadValue();

        float angle = Mathf.Atan2(stickL.y, stickL.x) * Mathf.Rad2Deg;

        if (stickL.x > threshold || stickL.y > threshold || stickL.x < -threshold || stickL.y < -threshold)
        {
            //item 1
            if (angle > 60 && angle < 120)
            {
                item1.SelectItem();
                item2.DeselectItem();
                item3.DeselectItem();
                item4.DeselectItem();
                item5.DeselectItem();
                item6.DeselectItem();
            }
            //item 2
            if (angle > 0 && angle < 60)
            {
                item1.DeselectItem();
                item2.SelectItem();
                item3.DeselectItem();
                item4.DeselectItem();
                item5.DeselectItem();
                item6.DeselectItem();
            }
            //item 3
            if (angle > -60 && angle < 0)
            {
                item1.DeselectItem();
                item2.DeselectItem();
                item3.SelectItem();
                item4.DeselectItem();
                item5.DeselectItem();
                item6.DeselectItem();
            }
            //item 4
            if (angle > -120 && angle < -60)
            {
                item1.DeselectItem();
                item2.DeselectItem();
                item3.DeselectItem();
                item4.SelectItem();
                item5.DeselectItem();
                item6.DeselectItem();
            }
            //item 5
            if (angle > -180 && angle < -120)
            {
                item1.DeselectItem();
                item2.DeselectItem();
                item3.DeselectItem();
                item4.DeselectItem();
                item5.SelectItem();
                item6.DeselectItem();
            }
            //item 6
            if (angle > 120 && angle < 180)
            {
                item1.DeselectItem();
                item2.DeselectItem();
                item3.DeselectItem();
                item4.DeselectItem();
                item5.DeselectItem();
                item6.SelectItem();
            }
        }
        else
        {
            item1.DeselectItem();
            item2.DeselectItem();
            item3.DeselectItem();
            item4.DeselectItem();
            item5.DeselectItem();
            item6.DeselectItem();
        }
    }
}
