using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class points : MonoBehaviour
{
    public objects_interact OI;
    
    // Start is called before the first frame update

    public void checkpoints()
    {
        OI.CheckBuisness();
    }

}
