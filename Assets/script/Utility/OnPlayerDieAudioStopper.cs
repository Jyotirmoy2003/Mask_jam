using System.Collections.Generic;
using UnityEngine;

public class OnPlayerDieAudioStopper : MonoBehaviour
{
    [SerializeField] List<AudioSource> audioSources = new List<AudioSource>();
    public void ListenTOPlayerDies(Component sender,object data)
    {
        if((bool)data)
        {
            foreach (var item in audioSources)
            {
                item.Stop();
            }
        }
    }
}
