using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerAttackController : MonoBehaviour
{
    protected Animator animator;
    public GameObject headbuttLeft;
    public GameObject headbuttRight;

    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        Assert.IsTrue(animator);
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetTrigger("HeadbuttLeft");
            headbuttLeft.SetActive(true);
            StartCoroutine(TurnOffCollider(headbuttLeft));
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("HeadbuttRight");
            headbuttRight.SetActive(true);
            StartCoroutine(TurnOffCollider(headbuttRight));
        }
    }

    protected IEnumerator TurnOffCollider(GameObject colliderObject)
    {
        float timer = .25f;
        while (timer > 0)
        {
            Debug.Log(timer);
            timer -= Time.deltaTime;
            yield return null;
        }
        colliderObject.SetActive(false);
    }
}
