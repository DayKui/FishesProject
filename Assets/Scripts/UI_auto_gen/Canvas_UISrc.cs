using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
///UISource File Create Data: 2019/9/5 22:28:52
public partial class Canvas_UICtrl : MonoBehaviour {

	public Dictionary<string, GameObject> view = new Dictionary<string, GameObject>();
	void load_all_object(GameObject root, string path) {
		foreach (Transform tf in root.transform) {
			this.view.Add(path + tf.gameObject.name, tf.gameObject);
			load_all_object(tf.gameObject, path + tf.gameObject.name + "/");
		}

	}

	void Awake() {
		this.load_all_object(this.gameObject, "");

	}

}
