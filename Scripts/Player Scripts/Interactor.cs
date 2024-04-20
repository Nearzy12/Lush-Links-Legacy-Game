using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Interactor : MonoBehaviour
{
    public Transform cameraTransform;
    public float interactionDistance;
    public LayerMask interactionMask;

    public bool performingAction;

    public string interaction_key_string;

    //On Screen Text
    public TMP_Text onScreenText;
    public Slider actionSlider;

    public float actionCooldown = 0.5f;
    public float nextActionTime = 0.0f;

    [SerializeField]
    private int playerTool;
    [SerializeField]
    private string HitObjectTag;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        performAction();

    }

    public void performAction()
    {
        // Cast a ray every frame and look for interactable objects.
        ObjectInteractionManager hit_object_script;

        Debug.DrawRay(cameraTransform.position, cameraTransform.forward * interactionDistance, Color.green);

        RaycastHit hit;
        Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, interactionDistance, interactionMask);

        if (hit.collider != null)
        {
            // This means we are close enough and looking at a interactable object
            hit_object_script = hit.collider.gameObject.GetComponent<ObjectInteractionManager>();
            HitObjectTag = hit.collider.gameObject.tag;
            playerTool = gameManager.getPlayerTool();
            string hitReturnString = hit_object_script.getObjectText();
            onScreenText.text = hitReturnString;

            interaction_key_string = hit_object_script.getInteractionButton();

            // only allow the object to get hit if the action time is within the next action time threshold
            if (Input.GetButtonDown(interaction_key_string))
            {   
                if(interaction_key_string == "Hit")
                {
                    if (Time.time >= nextActionTime)
                    {
                        if(HitObjectTag == "Tree")
                        {
                            if(playerTool == 1)
                            {
                                hit_object_script.hitObject(1);
                                nextActionTime = Time.time + actionCooldown;
                                StartCoroutine(ActionSlider());
                            }
                            else
                            {
                                hit_object_script.setInteractionText("You need an Axe to chop down Trees");
                            }
                        }else if( HitObjectTag == "Rock")
                        {
                            if (playerTool == 2)
                            {
                                hit_object_script.hitObject(1);
                                nextActionTime = Time.time + actionCooldown;
                                StartCoroutine(ActionSlider());
                            }
                            else
                            {
                                hit_object_script.setInteractionText("You need a Pickaxe to break up Rocks");
                            }
                        }
                        else if(HitObjectTag == "Stump")
                        {
                            if (playerTool == 3)
                            {
                                hit_object_script.hitObject(1);
                                nextActionTime = Time.time + actionCooldown;
                                StartCoroutine(ActionSlider());
                            }
                            else
                            {
                                hit_object_script.setInteractionText("You need a Shovel to dig out Stumps");
                            }
                        }
                    }
                }
                else
                {
                    hit_object_script.hitObject(1);
                }
            }
        }
        else
        {
            onScreenText.text = "";
        }
    }
    private IEnumerator ActionSlider()
    {
        performingAction = true;
        actionSlider.gameObject.SetActive(true);
        actionSlider.value = 0;
        actionSlider.DOValue(1, actionCooldown).SetEase(Ease.Linear); ;

        yield return new WaitForSeconds(actionCooldown);

        actionSlider.gameObject.SetActive(false);
    }
}
