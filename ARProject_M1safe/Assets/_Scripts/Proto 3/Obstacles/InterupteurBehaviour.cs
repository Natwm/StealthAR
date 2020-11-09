using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InterupteurBehaviour : ActionableObjects, IActionableObjects
{
    [SerializeField] private GameObject m_Voyant;

    public virtual void ShowAction()
    {
        Debug.Log("change couleur");
        if(m_Voyant.GetComponent<Renderer>().material.color == Color.green)
        {
            m_Voyant.GetComponent<Renderer>().material.color = Color.red;
        }else
            m_Voyant.GetComponent<Renderer>().material.color = Color.green;
    }

    #region Interface
    public void Interaction()
    {
        MoveWall();
        ActionPlatform();
        ShowAction();
        GameManager.PlaySoundStatic(Sound.m_SoundName.InterupteurAction);
        //Debug.Log("interaction");
    }
    #endregion
}
