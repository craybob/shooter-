using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySound : MonoBehaviour
{
    public AudioClip[] EnemySounds;

    
    public bool hurt = false;

    void EnemysoundManager()
    {
        if (hurt == true)
        {
            mySoundManager.Instance.Play(EnemySounds[0]);
            hurt = false;
        }
    }


}
