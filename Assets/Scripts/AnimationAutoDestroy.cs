using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAutoDestroy : MonoBehaviour
{
    private float animTime;
    // Start is called before the first frame update
    void Start()
    {
        animTime = this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, animTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
