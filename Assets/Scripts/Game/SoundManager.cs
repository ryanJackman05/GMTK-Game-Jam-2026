using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager sm;
    public AudioClip[] clips;
    private List<AudioSource> sources;
    // Start is called before the first frame update

    private void Start()
    {
        sm = this;
        foreach (AudioClip clip in clips){
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = clip;
            sm.sources.Add(source);
        }
    }
    
    public void PlayClip(string name)
    {
        foreach (AudioSource source in sources)
        {
            if (source.clip.name == name){
                source.Play();
                return;
            }
        }
    }
}
