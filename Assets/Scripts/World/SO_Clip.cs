using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio", fileName = "Clip")]
public class SO_Clip : ScriptableObject{
    [SerializeField] AudioClip clip;
    [SerializeField] float volume = 1;

    public AudioClip Clip => clip;
    public float Volume => volume;
}    
