using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparaHandler : MonoBehaviour {

	public void GoReady(Tennis1.Common.IPreparable preparable)
    {
        preparable.Ready();
    }
}
