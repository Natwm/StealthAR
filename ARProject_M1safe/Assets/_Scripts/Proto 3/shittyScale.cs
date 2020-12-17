using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class shittyScale : MonoBehaviour
{

    [SerializeField] float timetoscale = 1f;

    [SerializeField] GameObject ExplosionParticule;

    [SerializeField] GameObject[] particule;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 local = transform.localScale;
        transform.localScale = Vector3.zero;

        transform.DOScale(local, timetoscale);
        transform.DOMoveZ(transform.position.z - 20, 5f);
    }

    public IEnumerator attaque(List<TurretBehaviours> cible)
    {

        for (int i = 0; i < cible.Count; i++)
        {
            particule[i].transform.LookAt(cible[i].transform);
            

            yield return new WaitForSeconds(1f);
            particule[i].GetComponent<ParticleSystem>().Play();

            Instantiate(ExplosionParticule, cible[i].transform.position, Quaternion.identity);
            Destroy(cible[i].gameObject);
            GameManager.PlaySoundStatic(Sound.m_SoundName.PlayerDied);
        }
        

    }
}
