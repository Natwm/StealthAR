﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class shittyScale : MonoBehaviour
{

    [SerializeField] float timetoscale = 1f;

    [SerializeField] GameObject[] particule;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 local = transform.localScale;
        transform.localScale = Vector3.zero;

        transform.DOScale(local, timetoscale);
        transform.DOMoveZ(transform.position.z - 20, 5f);
    }

    public void attaque(List<TurretBehaviours> cible)
    {
        try
        {
            for (int i = 0; i < cible.Count; i++)
            {
                particule[i].transform.LookAt(cible[i].transform);
                particule[i].GetComponent<ParticleSystem>().Play();
            }
        }
        catch (Exception e)
        {
            Debug.Log("Impossible");
        }
        
    }
}
