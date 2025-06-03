using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefsSO : ScriptableObject
{
    public AudioClip[] Fail;
    public AudioClip[] Success;
    public AudioClip[] Footstep;
    public AudioClip[] ObjectDrop;
    public AudioClip[] ObjectPickup;

    public AudioClip[] warning;


}

