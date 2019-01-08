using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(ThirdPersonCharacter))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AICharacterControl))]

public class PlayerMovement : MonoBehaviour
{
    bool isInDirectMode = false;

    [SerializeField] float attackMeleeRadius = 0.7f;
    [SerializeField] float attackDistanceRadius = 5f;

    float actualRadius = 0.15f;

    ThirdPersonCharacter thirdPersonCharacter = null;
    CameraRaycaster cameraRaycaster = null;
    Vector3 currentDestination, clickPoint;
    AICharacterControl ai = null;
    GameObject walkTarget = null;

    [SerializeField] const int walkableLayerNumber = 9;
    [SerializeField] const int enemyLayerNumber = 10;
    [SerializeField] const int npcLayerNumber = 11;

    // Start is called before the first frame update
    void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
        ai = GetComponent<AICharacterControl>();

        walkTarget = new GameObject("walkTarget");
        cameraRaycaster.NotifyMouseClickObservers += ProcessMouseClick;
    }

    void ProcessMouseClick(RaycastHit raycastHit, int layerHit)
    {
        switch (layerHit) { 
            case enemyLayerNumber:
                ai.SetTarget(raycastHit.collider.gameObject.transform);
                break;
            case npcLayerNumber:
                ai.SetTarget(raycastHit.collider.gameObject.transform.transform);
                break;
            case walkableLayerNumber:
                walkTarget.transform.position = raycastHit.point;
                ai.SetTarget(walkTarget.transform);
                break;
            default:
                Debug.LogWarning("Don't know hot to handle mouse click for player movement");
                break;
        }
    }

    //TODO make this called again
    private void ProcessDirectMovement() //support for gamepad
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");

        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movenemt = v * cameraForward + h * Camera.main.transform.right;

        thirdPersonCharacter.Move(movenemt, false, false);
    }
}
