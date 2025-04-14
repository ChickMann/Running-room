using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMoment : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private int anim_int;
    private void Awake()
    {
            anim = GetComponent<Animator>();
            anim.SetInteger("Animation_int", anim_int);
        
    }

}
