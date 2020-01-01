using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour {

    public Transform Root;

    Vector3 _Vector;
    
    public PlayerMover()
    {
        _Vector = new Vector3();
    }
    
	
	// Update is called once per frame
	void Update () {
        var delta = UnityEngine.Time.deltaTime;
        var x  = Root.position.x + _Vector.x * delta;
        var z  = Root.position.z + _Vector.z * delta;

        Vector3 p = new Vector3();
        p.x = x;
        p.z = z;
        Root.SetPositionAndRotation(p, Quaternion.identity);
    }

    public void Move(Tennis1.Common.Move move)
    {

        _Vector.x = move.Vector.X;
        _Vector.z = move.Vector.Y;

        Vector3 p = new Vector3();
        p.Set(move.Start.X, 0, move.Start.Y); 
        Root.SetPositionAndRotation(p , Quaternion.identity);        
    }
}
