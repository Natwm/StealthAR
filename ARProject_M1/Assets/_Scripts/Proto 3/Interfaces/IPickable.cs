using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickable<T>  
{
    void ValidationSpawn();
    void Damage(T damageTake);
    void Kill();
}


