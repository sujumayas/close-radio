using UnityEngine;
using System.Collections;

public class MouseActions : MonoBehaviour {

    Vector3 mPos;

    public GameObject holdingObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if (Input.GetMouseButtonDown(0)) {
            SetMousePosition ();
            RaycastHit2D[] hits = Physics2D.LinecastAll (mPos, mPos);
            if (holdingObject != null) {
                if (hits.Length != 0) {
                    foreach (RaycastHit2D hit in hits) {
                        if (hit.collider.CompareTag ("Land")) {
                            holdingObject.transform.position = hit.transform.position;
                            Transform[] children = holdingObject.transform.GetComponentsInChildren<Transform> ();
                            foreach (Transform child in children) {
                                RaycastHit2D childHit = Physics2D.Linecast (child.position, child.position);
                                if (childHit && childHit.transform.CompareTag("Land")) {
                                    childHit.transform.GetComponent<SpriteRenderer> ().color = holdingObject.GetComponent<SpriteRenderer> ().color;
                                }
                            }
                            holdingObject = null;
                            break;
                        }
                    }
                }
            } else {
                if (hits.Length != 0) {
                    foreach (RaycastHit2D hit in hits) {
                        if (hit.collider.CompareTag ("Tower")) {
                            holdingObject = hit.collider.gameObject;
                            holdingObject.GetComponent<Collider2D> ().enabled = false;
                            break;
                        }
                    }
                }
            }
        }

        if (holdingObject != null) {
            Vector3 targetPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            holdingObject.transform.position = new Vector3 (targetPos.x, targetPos.y, 0);
        }
	}

    void SetMousePosition () {
        mPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        mPos = new Vector3 (mPos.x, mPos.y, 0);
    }

    void TryUseTower () {

    }
}
