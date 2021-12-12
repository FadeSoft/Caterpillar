using UnityEngine;
using System.Collections;

public class TouchSc : MonoBehaviour
{
    public Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    public float speed;
    public Animator catAnim;
    public GameObject handImg;
    public PlayerManager movementSc;
    public DistanceE distanceSc;
    public GameObject arrow;

    //Bu scripti açýklayamayacaðým bende zor yazdým :)))
    private void Start()
    {
        firstPressPos = new Vector2(0, 0);
        catAnim.enabled = false;
        handImg.SetActive(false);

    }
    private void LateUpdate()
    {
        if (Input.touchCount > 0)
        {
            catAnim.enabled = true;
            movementSc.enabled = true;

            arrow.SetActive(false);
            handImg.SetActive(true);

            Touch t = Input.GetTouch(0);
            handImg.transform.position = t.position;

            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
                handImg.SetActive(true);
            }
            if (t.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(transform.position.x + t.deltaPosition.x * speed, transform.position.y, transform.position.z);
                currentSwipe = t.position;
                secondPressPos = new Vector2(t.position.x, 0);
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                currentSwipe.Normalize();

                // left
                if (currentSwipe.x < 0)
                {
                    catAnim.SetBool("left", true);
                    catAnim.SetBool("right", false);
                }
                // right
                if (currentSwipe.x > 0)
                {
                    catAnim.SetBool("right", true);
                    catAnim.SetBool("left", false);
                }
                if (currentSwipe.x == 0)
                {            
                    catAnim.SetBool("x", false);
                }
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }

            if (t.phase == TouchPhase.Ended)
            {
                catAnim.SetBool("left", false);
                catAnim.SetBool("right", false);
                catAnim.SetBool("x", false);
                handImg.SetActive(false);          

                currentSwipe = t.position;
                firstPressPos = new Vector2(t.position.x, t.position.y);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(51.32f, -1f, 0f), 5f);
            }
            if (t.phase == TouchPhase.Stationary)
            {
                catAnim.SetBool("x", false);
                catAnim.SetBool("left", false);
                catAnim.SetBool("right", false);

                currentSwipe = t.position;
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
        }
    }
}
