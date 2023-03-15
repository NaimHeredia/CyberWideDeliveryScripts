using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Multipurpose Script that enables functionality on Collision
*/

public class CollideEnable : MonoBehaviour
{

    public AudioClip pickupAudio;

    public enum ScriptType { OTHER, ANIMATION, HUD, SPAWN, COMPONENT_ENABLE };

    [SerializeField]
    private ScriptType type = ScriptType.OTHER;

    // Takes in an animator
    [SerializeField]
    private Animator animator = null;

    // Takes in Text Object to change in the UI
    [SerializeField]
    private Text hudIndicator = null;

    // Takes in Object to Spawn
    [SerializeField]
    private GameObject SpawnGO = null;

    // Destroys collider on colliison exit
    [SerializeField]
    private bool ExitDestroyCollider = false;

    // Takes in Object to Enable a component
    [SerializeField]
    private List<GameObject> ComponentEnableGO = new List<GameObject>();    

    // Takes in a Message
    [SerializeField]    
    [TextArea(15, 20)]
    public string Text = null;


    private void Awake()
    {
        // If the Script Type is to enable a game objects component, first turn them off
        if( type == ScriptType.COMPONENT_ENABLE)
                    {
            for (int i = 0; i < ComponentEnableGO.Count; i++)
            {
                ComponentEnableGO[i].GetComponent<MoveToPoints>().enabled = false;
            }            
        }
        

    }

    private void Start()
    {
        if (type == ScriptType.HUD)
        {
            hudIndicator.text = "";
            hudIndicator.enabled = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        // If collision is whit the player
        if(other.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFX(pickupAudio);
            switch(type)
            {
                case ScriptType.ANIMATION:
                    {
                        // If there is an animator
                        if (animator != null)
                        {
                            // turn animator bool parameter on
                            animator.SetBool("On", true);
                        }
                        break;
                    }                

                case ScriptType.SPAWN:
                    {
                        // Spawning Objects
                        if (SpawnGO != null)
                        {
                            SpawnGO.SetActive(true);
                        }
                        break;
                    }

                case ScriptType.COMPONENT_ENABLE:
                    {
                        // Enable the component of all the Game Objects in the list
                        for (int i = 0; i < ComponentEnableGO.Count; i++) 
                        {
                            ComponentEnableGO[i].GetComponent<MoveToPoints>().enabled = true;
                        }
                        break;
                    }

                case ScriptType.OTHER:
                    {
                        if(ComponentEnableGO[0].name == "Barrell")
                        {
                            ComponentEnableGO[0].GetComponent<Shoot3D>().enabled = true;
                        }
                        break;
                    }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If player leaves collision
        if (other.CompareTag("Player"))
        {
            if (hudIndicator != null)
            {
                // Turn off the UI 
                hudIndicator.enabled = false;
            }

            if (ExitDestroyCollider)
            {
                Destroy(gameObject);
            }
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (type == ScriptType.HUD)
            {
                // If there is a Text UI
                if (Text != "")
                {
                    // Change the UI to our Text
                    hudIndicator.text = Text;
                    hudIndicator.enabled = true;
                }
            }
        }
    }
}


