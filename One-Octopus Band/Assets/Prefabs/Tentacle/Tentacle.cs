using UnityEngine;

public class Tentacle : MonoBehaviour {
    public float pokeStrength;
    public float controlJointDist;
    public float reticuleSpeed;
    /// <summary>
    /// 0: Mouse
    /// 1-4: Corresponding gamepad
    /// </summary>
    public int controller; 

    public Rigidbody controlJoint;
    public Rigidbody tipJoint;
    private Vector2 reticule;

    void Start() {
        if(controller != 0) {
            reticule = new Vector2(0, 0);
        }
    }

    private void FixedUpdate() {
        Vector3 newPos;
        Ray ray;
        if(controller == 0) {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        } else {
            Vector2 moveVector = new Vector2(Input.GetAxis("Horizontal" + controller), Input.GetAxis("Vertical" + controller)).normalized * reticuleSpeed;
            reticule += moveVector;

            ray = Camera.main.ScreenPointToRay(reticule);
        }
        int layerMask = LayerMask.GetMask("ReticulePlane");
        if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask)) {
            newPos = hit.point;
            controlJoint.position = newPos;
        }
        
        if (controller == 0) {
            if (Input.GetMouseButton(0)) {
                tipJoint.AddForce(controlJoint.transform.up.normalized * pokeStrength);
            }
        } else if (Input.GetButton("Stretch" + controller)) {
            tipJoint.AddForce(controlJoint.transform.up.normalized * pokeStrength);
        }
    }
}
