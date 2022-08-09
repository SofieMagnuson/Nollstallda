using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class player_animation : MonoBehaviour
{
    public AnimatorController animController;
    // Start is called before the first frame update
    public void animWalking()
    {
        Debug.Log("Walking");
        //animController.SetBool("Walking", true);
    }
    public void animIdle()
    {
        Debug.Log("Still");
        //animController.SetBool("Walking", false);
    }
}
