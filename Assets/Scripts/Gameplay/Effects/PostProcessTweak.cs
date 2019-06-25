using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PostProcessTweak : MonoBehaviour {

    public PostProcessingProfile prof;


    public void bloom(float _bl)
    {
        BloomModel.Settings cache = prof.bloom.settings;
        cache.bloom.intensity = _bl;
        prof.bloom.settings = cache;
    }


   
}
