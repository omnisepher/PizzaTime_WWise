using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField]
    private AK.Wwise.Event footstepsEvent;

    [SerializeField]
    private AK.Wwise.Switch footstepsGravel;
    [SerializeField]
    private AK.Wwise.Switch footstepsGrass;
    public void PlayFootstepSound()
    {
        GroundSwitch();
        footstepsEvent.Post(gameObject);
    }
    private void GroundSwitch()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, -Vector3.up);
        Material surfaceMaterial;

        if (Physics.Raycast(ray, out hit, 1.0f, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            Renderer surfaceRenderer = hit.collider.GetComponentInChildren<Renderer>();
            if (surfaceRenderer)
            {
                Debug.Log(surfaceRenderer.material.name);
                if (surfaceRenderer.material.name.Contains("grass"))
                {
                    //AkSoundEngine.SetSwitch("Footsteps", "Grass", gameObject);
                    footstepsGrass.SetValue(gameObject);
                }
                else
                {
                    //AkSoundEngine.SetSwitch("Footsteps", "Gravel", gameObject);
                    footstepsGravel.SetValue(gameObject);
                }
            }
        }
    }
}
