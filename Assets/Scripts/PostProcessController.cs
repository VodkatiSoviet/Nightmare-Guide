using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessController : MonoBehaviour
{
    private PostProcessLayer layer;
    private PostProcessVolume volume;
    // Start is called before the first frame update
    void Start()
    {
        layer = GetComponent<PostProcessLayer>();
        volume = GetComponent<PostProcessVolume>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Change_ChromaticAberration()
    {
        
    }
}
