using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityStandardAssets.ImageEffects;
using static UnityEngine.GraphicsBuffer;

public class PlayerMainCamera : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] PostProcessingBehaviour postProcessingBehaviour;
    [SerializeField] PostProcessingProfile day_Scene;
    [SerializeField] PostProcessingProfile night_Scene;

    public static PlayerMainCamera camera_single {  get; private set; }
    private Transform death_Camera_Target;
    public float rotationDuration = 1f;

    private Coroutine rotationCoroutine;

    private void Awake()
    {
        if (camera_single == null)
        {
            camera_single = this;
        }
    }
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

    public void DeathCamera()
    {
        death_Camera_Target = Enemy.enemy_single.deathTarget;
        Debug.Log(death_Camera_Target);
    }

    public void RotateTarget()
    {
        StartCoroutine(RotateCamera());
    }

    private IEnumerator RotateCamera()
    {
        DeathCamera();
        Quaternion initialRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(death_Camera_Target.position - transform.position);

        float elapsedTime = 0f;

        while (elapsedTime < rotationDuration)
        {
            float t = elapsedTime / rotationDuration;

            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;

        rotationCoroutine = null;
    }
}
