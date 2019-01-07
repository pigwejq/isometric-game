using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(ThirdPersonCharacter))]

public class PlayerMovement : MonoBehaviour
{
    bool isInDirectMode = false;

    [SerializeField] float walkMoveStopRadius = 0.25f;
    [SerializeField] float attackMeleeRadius = 0.7f;
    [SerializeField] float attackDistanceRadius = 5f;

    float actualRadius = 0.15f;

    ThirdPersonCharacter thirdPersonCharacter;
    CameraRaycaster cameraRaycaster;
    Vector3 currentDestination, clickPoint;

    // Start is called before the first frame update
    void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    private void ProcessDirectMovement() //support for gamepad
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");

        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movenemt = v * cameraForward + h * Camera.main.transform.right;

        thirdPersonCharacter.Move(movenemt, false, false);
    }

    //private void ProcessMouseMovement()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        clickPoint = cameraRaycaster.hit.point;
    //        switch (cameraRaycaster.currentLayerHit)
    //        {
    //            case Layer.Walkable:
    //                currentDestination = ShortDestination(clickPoint, walkMoveStopRadius);
    //                break;
    //            case Layer.Enemy:
    //                Attack();
    //                break;
    //            case Layer.NPC:
    //                currentDestination = ShortDestination(clickPoint, walkMoveStopRadius);
    //                break;
    //        }
    //    }
    //    WalkToDestination();
    //}

    private void WalkToDestination()
    {
        var playerToClickPoint = currentDestination - transform.position;
        if (playerToClickPoint.magnitude >= walkMoveStopRadius)
        {
            thirdPersonCharacter.Move(playerToClickPoint, false, false);
        }
        else
        {
            thirdPersonCharacter.Move(Vector3.zero, false, false);
        }
    }

    Vector3 ShortDestination(Vector3 destination, float shortening)
    {
        Vector3 reductionVector;
        reductionVector = (destination - transform.position).normalized * shortening;
        return destination - reductionVector;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, clickPoint);
        Gizmos.DrawSphere(clickPoint, 0.07f);
        Gizmos.DrawSphere(currentDestination, 0.15f);
    }
}
