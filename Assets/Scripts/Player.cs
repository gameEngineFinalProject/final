using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Player : MonoBehaviour,IKitchenObjectParents
{
    //private static Player instance;
    //public  static Player Instance
    //{
    //    get
    //    {
    //        return instance;
    //    }
    //    set
    //    {
    //        instance = value;
    //    }

    //}
    public static Player Instance { get; private set; }
    public event EventHandler <OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs: EventArgs
    {
        public BaseCounter selectedCounter;
    }
    [SerializeField ]private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    float interactDistance = 2f;
    private KitchenObject kitchenObject;
    // Start is called before the first frame update
    void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one player exists");
        }
        Instance = this;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }

     
       
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleInteractions();

    }
    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }


        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance,counterLayerMask))
        {
            //Debug.Log(raycastHit.transform);
            if(raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                //Has ClearCounter
               //clearCounter.Interact();
               if( baseCounter != selectedCounter)
                {
                    
                    SetSelectedCounter(baseCounter);
                }
               
            }
            else
            {
                
                SetSelectedCounter(null);
            }
        }
        else
        {
           
            SetSelectedCounter(null);
        }
     
    }

    public bool IsWalking()
    {
        return isWalking;
    }
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.8f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            // Cannot move towards moveDir
            // Attempt only X movements
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDir.x!=0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                //can only move on X
                moveDir = moveDirX;
            }
            else
            {
                // cannot only move on x
                // attempt only z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.z !=0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    // Can only on the Z
                    moveDir = moveDirZ;
                }
                else
                {
                    //Cannot move in any direction
                }
            }
        }
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }


        isWalking = (moveDir != Vector3.zero);

        float ratateSpeed = 10f;
        //transform.forward = Vector3.Slerp(transform.forward , moveDir,Time.deltaTime*ratateSpeed);
        if (moveDir != Vector3.zero)
        {
            //float ratateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * ratateSpeed);
        }


    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKichenObject()
    {
        kitchenObject = null;

    }
    public bool HasKitchenObject()
    {
        return (kitchenObject != null);
    }

}

