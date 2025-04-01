using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker : MonoBehaviour
{
    public float volume;
    public float pitch;
    public float speed;

    public string fullString;
    public SoundSource voice;
    public GameObject soundPrefab;

    [SerializeField] private float baseSpeed;
    [SerializeField] private int stringPosition;

    [SerializeField] private char[] charArray;

    //This script is sort of the primary manager for the entire system. It receives the pitch, volume and speed settings, the typed string,
    //derives a list of characters from the typed string, and advances through it while spawning the associated sounds.
    
    public void RegisterFullString(string newString)
    {
        ResetString();
        fullString = newString;
        stringPosition = 0;
    }
    
    public void ResetString()
    {
        //Emergency Cancel; used eg. when the player clicks on the input field again. In a real game, this would come in handy when the text window is closed early.
        StopAllCoroutines();
        stringPosition = 0;
    }

    public void StartPlaying()
    {
        stringPosition = 0;
        charArray = fullString.ToLower().ToCharArray();
        if(stringPosition >= charArray.Length) return;
        SpawnSound(GetSoundID(charArray[stringPosition]));
        PlayNextCharacter();
    }
    
    private void PlayNextCharacter()
    {
        //turns the typed string into lower case, then into an array (char[]) we can read as a long list of letters
        charArray = fullString.ToLower().ToCharArray();
        if(stringPosition >= charArray.Length) return;
        StartCoroutine(WaitThenPlayCharacter(GetSoundID(charArray[stringPosition])));
    }

    private IEnumerator WaitThenPlayCharacter(string soundId)
    {
        //Waits for a set moment before spawning the sound we want, then spawns it, then triggers the next character
        yield return new WaitForSeconds(baseSpeed / speed);
        SpawnSound(soundId);
        PlayNextCharacter();
    }

    public void SpawnSound(string soundId)
    {
        //Creates the game object that actually plays the sound
        GameObject playerObj = Instantiate(soundPrefab);
        SoundPlayer player = playerObj.GetComponent<SoundPlayer>();
        
        //Call the function in SoundPlayer that sets it up and triggers it
        player.Initialize(voice, soundId, volume, pitch);
    }

    private string GetSoundID(char curChar)
    {
        //Return a string depending on the char we got out of the list. If we suspect a special sound or dipthong might be needed, check for that here.
        //Dipthongs are obviously different in german; I would recommend building the system for whichever language you're targeting, not making a complicated switcher.
        //The big german ones are ei, eu, ee, st, dt, ch, and sch. with sch, implement a special checker that goes one letter further
        //if you want to make english sound a little bit fancier, maybe implement a checker that changes the 'er' sound to an 'ah' if its followed by a non-letter character
        
        //Notice that each special character, when triggered, advances stringPosition by 1. This is so "au" doesnt play the au sound and then the u sound after it.
        
        //Advance by 1 in the list:
        stringPosition += 1;

        //Set up secondary char in case we need it:
        char sChar;
        
        switch (curChar)
        {
            default: return "null";
            case 'a':
                if (stringPosition < charArray.Length)
                {
                    sChar = charArray[stringPosition];
                    if (sChar == 'u')
                    {
                        stringPosition += 1; 
                        return "au";
                    }
                }
                return "a";
            case 'b': return "b";
            case 'c':
                if (stringPosition < charArray.Length)
                {
                    sChar = charArray[stringPosition];
                    if (sChar == 'h')
                    {
                        stringPosition += 1; 
                        return "ch";
                    }

                    if (sChar == 'k')
                    {
                        stringPosition += 1;
                        return "k";
                    }
                }
                return "c";
            case 'd': return "d";
            case 'e': return "e";
            case 'f': return "f";
            case 'g': return "g";
            case 'h': return "h";
            case 'i': return "i";
            case 'j': return "j";
            case 'k': return "k";
            case 'l': return "l";
            case 'm': return "m";
            case 'n':
                if (stringPosition < charArray.Length)
                {
                    sChar = charArray[stringPosition];
                    if (sChar == 'g')
                    {
                        stringPosition += 1; 
                        return "ng";
                    }
                }
                return "n";
            case 'o':
                if (stringPosition < charArray.Length)
                {
                    sChar = charArray[stringPosition];
                    //Weird case where ou usually makes an "au" sound
                    if (sChar == 'u')
                    {
                        stringPosition += 1; 
                        return "au";
                    }
                }
                return "o";
            case 'p':
                if (stringPosition < charArray.Length)
                {
                    sChar = charArray[stringPosition];
                    //ph makes an f sound
                    if (sChar == 'h')
                    {
                        stringPosition += 1; 
                        return "f";
                    }
                }
                return "p";
            case 'q':
                if (stringPosition < charArray.Length)
                {
                    sChar = charArray[stringPosition];
                    //Special case: 'qu' should make the q sound, not q and u separately
                    if (sChar == 'u')
                    {
                        stringPosition += 1; 
                        return "q";
                    }
                }
                return "q";
            case 'r': return "r";
            case 's':
                if (stringPosition < charArray.Length)
                {
                    sChar = charArray[stringPosition];
                    if (sChar == 'h')
                    {
                        stringPosition += 1; 
                        return "sh";
                    }
                }
                return "s";
            case 't':
                if (stringPosition < charArray.Length)
                {
                    sChar = charArray[stringPosition];
                    if (sChar == 'h')
                    {
                        stringPosition += 1; 
                        return "th";
                    }
                }
                return "t";
            case 'u': return "u";
            case 'v': return "v";
            case 'w': return "w";
            case 'x': return "x";
            case 'y': return "y";
            case 'z': return "z";
        }
    }
}
