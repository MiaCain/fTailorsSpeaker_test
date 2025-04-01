using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource Source;
    [SerializeField] private SoundSource Voice;
    [SerializeField] private float Volume;
    [SerializeField] private float Pitch;


    public void Initialize(SoundSource newVoice, string soundId, float newVolume, float newPitch)
    {
        //Gets called from the spawning script. Sets us up with all the data we need to play the correct sound at the correct volume and pitch
        Source = this.gameObject.GetComponent<AudioSource>();
        Voice = newVoice;
        Volume = newVolume;
        Pitch = newPitch;

        Source.volume = Volume;
        Source.pitch = Pitch;

        Source.clip = SoundGetter(soundId);

        Source.enabled = true;
        if(Source!= null) Source.Play();
        StartCoroutine(WaitThenDestroy(Source.clip.length * 1.25f));
    }

    private IEnumerator WaitThenDestroy(float time)
    {
        //Makes sure the sound has been played, then destroys the whole gameobject. This prevents a memory leak
        yield return new WaitForSeconds(time*2);
        Destroy(this.gameObject);
    }
    
    private AudioClip SoundGetter(string soundID)
    {
        //matches the "soundID" string we've been passed with the matching clip in the Voice scriptableObject
        
        switch (soundID)
        {
            default: return Voice.nullClip;
            
            case "a": return Voice.aClip;
            case "b": return Voice.bClip;
            case "c": return Voice.cClip;
            case "d": return Voice.dClip;
            case "e": return Voice.eClip;
            case "f": return Voice.fClip;
            case "g": return Voice.gClip;
            case "h": return Voice.hClip;
            case "i": return Voice.iClip;
            case "j": return Voice.jClip;
            case "k": return Voice.kclip;
            case "l": return Voice.lClip;
            case "m": return Voice.mClip;
            case "n": return Voice.nClip;
            case "o": return Voice.oClip;
            case "p": return Voice.pClip;
            case "q": return Voice.qClip;
            case "r": return Voice.rClip;
            case "s": return Voice.sClip;
            case "t": return Voice.tClip;
            case "u": return Voice.uClip;
            case "v": return Voice.vClip;
            case "w": return Voice.wClip;
            case "x": return Voice.xClip;
            case "y": return Voice.yClip;
            case "z": return Voice.zClip;
            case "au": return Voice.auClip;
            case "ou": return Voice.ouClip;
            case "th": return Voice.thClip;
            case "ch": return Voice.chClip;
            case "ph": return Voice.phClip;
            case "ng": return Voice.ngClip;
            case "sh": return Voice.shClip;
        }
    }
}
