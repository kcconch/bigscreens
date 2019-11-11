// using UnityEngine;
// // using Random = UnityEngine.Random;

// public class MeshDeformerInput : MonoBehaviour {

//     //public ClientData data;
//     public float force = 1000f;
// 	public float forceOffset = 0.1f;

// 	public static MeshDeformerInput Create(){

// 		// Debug.Log(data);

// 		return true;
// 	}
	
//     void Update () {
//         HandleInput();
//     //    if (Input.GetMouseButton(0)) {
// 	// 		HandleInput();
// 	// 	}
// 	}

// 	void HandleInput () {

// 		// Debug.Log(data != null);

// 		// if(data != null){

// 		// 	Debug.Log(data.input.y);
// 		// 	var x = 0.5f;
// 		// 	var y = -data.Input.y;
			
// 		// 	Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
// 		// 	RaycastHit hit;
			
// 		// 	if (Physics.Raycast(inputRay, out hit)) {
// 		// 		MeshDeformer deformer = hit.collider.GetComponent<MeshDeformer>();
// 		// 			if (deformer) {
// 		// 				Vector3 point = hit.point;
// 		// 				Vector3 xy = new Vector3(x, y, 1f);
// 		// 				point += hit.normal * forceOffset;
// 		// 				// xy += hit.normal * forceOffset;
// 		// 				deformer.AddDeformingForce(xy, force);
// 		// 			}
// 		// 	}
// 		// }
//         // var x = data.Input.x;
// 		//var pos = Camera.main.ViewportToWorldPoint(new Vector3(Random.value, 0, 0));
		
// 	}
// }