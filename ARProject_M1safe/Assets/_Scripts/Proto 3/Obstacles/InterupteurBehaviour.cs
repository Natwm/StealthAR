using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InterupteurBehaviour : ActionableObjects
{
    

    #region Interface
    public virtual void Interaction()
    {
        MoveWall();
        ActionPlatform();
    }

    #endregion
}
