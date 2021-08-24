using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionElevator : InteractionObject
{
    public UIController ui;
    public override void interact()
    {
        ui.UIModeOn(UIType.Elevator);
    }
}
