using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBomb : MonoBehaviour
{

    public float speed;
    private float waitTime;
    public float startWaitTime;

    public Transform[] movePoints;
    private int randomPoint;

    private Vector3 dir;

    //Animation
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        randomPoint = Random.Range(0, movePoints.Length);
        anim = GetComponent<Animator>();
        anim.SetBool("IsWalking", false);
    }

    // Update is called once per frame
    void Update()
    {
        FaceThePoint();

        transform.position = Vector3.MoveTowards(transform.position, movePoints[randomPoint].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoints[randomPoint].position)<0.2f)
        {

            if (waitTime <= 0)
            {
                //anim.SetFloat("Speed", 1);
                anim.SetBool("IsWalking", true);
                randomPoint = Random.Range(0, movePoints.Length);
                waitTime = startWaitTime;
               
            }
            else
            {
                //anim.SetFloat("Speed", 0);
                anim.SetBool("IsWalking", false);
                waitTime -= Time.deltaTime;
            }

        }
    }
    void FaceThePoint()
    {
        dir = (movePoints[randomPoint].position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
       
    }
}
