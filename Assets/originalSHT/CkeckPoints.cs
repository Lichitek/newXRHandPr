using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CkeckPoints : MonoBehaviour
{
    [SerializeField] public int idPoint;
    [SerializeField] public bool pointPassed;
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LeftHand" || other.gameObject.tag == "RightHand")
        {
            pointPassed = true;
            this.GetComponent<Material>().color = Color.green;
        }
    }
}
