using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

	public ArrayLayout data;
    private void Start()
    {
        print( data.rows[1].row[1]);
    }
}
