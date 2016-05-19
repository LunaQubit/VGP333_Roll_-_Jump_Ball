using UnityEngine;
using System.Collections;

public class SpinTransform : MonoBehaviour
{
    public Vector3 spin = Vector3.zero;

    void Update()
    {
        if( spin == Vector3.zero )
            return;

        this.transform.Rotate( spin * Time.deltaTime );
    }
}
