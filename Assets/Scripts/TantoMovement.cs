using UnityEngine;
using static GameInput;

public class TantoMovement : MonoBehaviour {

    public static TantoMovement Instance;

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private bool movementBasedOnCamera = true;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        GameInput.Instance.OnPlayerMove += Instance_OnPlayerMove;
    }

    private void Instance_OnPlayerMove(object sender, OnPlayerMoveEventArgs e) {
        Vector3 inputVector = e.inputVector;

        Vector3 forward;
        Vector3 right;
        if (movementBasedOnCamera) {
            forward = Camera.main.transform.forward;
            right = Camera.main.transform.right;
        } else {
            forward = this.transform.forward;
            right = this.transform.right;
        }

        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        Vector3 forwardVector = forward * inputVector.z;
        Vector3 rightVector = right * inputVector.x;

        Vector3 moveToVector = forwardVector + rightVector;

        this.transform.Translate(moveToVector * Time.deltaTime * moveSpeed, Space.World);
    }
    public void ChangeCameraBasedMovement() {
        movementBasedOnCamera = !movementBasedOnCamera;
    }

    public bool IsCameraBasedMovement() {
        return movementBasedOnCamera;
    }
}
