using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityStandardAssets.ImageEffects;

public class ChangeDayCamera : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] PostProcessingBehaviour postProcessingBehaviour;
    [SerializeField] PostProcessingProfile day_Scene;
    [SerializeField] PostProcessingProfile night_Scene;
    private void Update()
    {
        if (Input.GetKeyDown("o")) // ≥∑¿∏∑Œ ∫Ø∞Ê
        {
            Change_Day();
        }
        if (Input.GetKeyDown("u")) // π„¿∏∑Œ ∫Ø∞Ê
        {
            Change_Night();
        }
    }
    void Change_Day() // π„ø°º≠ ≥∑¿∏∑Œ ∫Ø∞Ê
    {
        mainCamera.renderingPath = RenderingPath.UsePlayerSettings;
        mainCamera.allowHDR = false;
        mainCamera.allowMSAA = true;
        postProcessingBehaviour.profile = day_Scene;
    }
    void Change_Night() // ≥∑ø°º≠ π„¿∏∑Œ ∫Ø∞Ê
    {
        mainCamera.renderingPath = RenderingPath.DeferredShading;
        mainCamera.allowHDR = true;
        mainCamera.allowMSAA = false;
        postProcessingBehaviour.profile = night_Scene;
    }
}
