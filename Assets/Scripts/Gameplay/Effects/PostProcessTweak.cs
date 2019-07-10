using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PostProcessTweak : MonoBehaviour {

    public PostProcessingProfile prof;
    public float bloomTo = 0f;
    private float currentBloom = 0f;

    public void bloom(float _bl)
    {
        BloomModel.Settings cache = prof.bloom.settings;
        cache.bloom.intensity = _bl;
        prof.bloom.settings = cache;
    }

    public void Start()
    {
        bloomTo = 0f;
    }

    public void Update()
    {
        if(currentBloom >= bloomTo) currentBloom -= 0.1f;
        if(currentBloom <= bloomTo) currentBloom += 0.1f;
        bloom(currentBloom);
    }

}
