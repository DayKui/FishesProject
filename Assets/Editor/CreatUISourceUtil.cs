using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

//BYCW 2018年6月5日10:43:51 博毅创为 版权所有
public class CreatUISourceUtil {

    //创建UISource文件的函数
    public static void CreatUISourceFile(GameObject selectGameObject)
    {
        string gameObjectName = selectGameObject.name;
        string fileName = gameObjectName + "_UISrc";
        string className = gameObjectName + "_UICtrl";
        StreamWriter sw = new StreamWriter(Application.dataPath + "/Scripts/UI_auto_gen/" + fileName + ".cs");
        sw.WriteLine(
            "using UnityEngine;\nusing System.Collections;\nusing UnityEngine.UI;\nusing System.Collections.Generic;");

        sw.WriteLine("///UISource File Create Data: " + System.DateTime.Now.ToString());
        sw.WriteLine("public partial class " + className + " : MonoBehaviour {" + "\n");

       
        sw.WriteLine("\t" + "public Dictionary<string, GameObject> view = new Dictionary<string, GameObject>();");
        sw.WriteLine("\t" + "void load_all_object(GameObject root, string path) {");
        sw.WriteLine("\t\t" + "foreach (Transform tf in root.transform) {");
        sw.WriteLine("\t\t\t" + "if (this.view.ContainsKey(path + tf.gameObject.name)) {");
        sw.WriteLine("\t\t\t\t" + "Debug.LogWarning(\"Warning object is exist:\" + path + tf.gameObject.name + \"!\");");
        sw.WriteLine("\t\t\t\t" + "continue;");
        sw.WriteLine("\t\t\t" + "}");
        sw.WriteLine("\t\t\t" + "this.view.Add(path + tf.gameObject.name, tf.gameObject);");
        sw.WriteLine("\t\t\t" + "load_all_object(tf.gameObject, path + tf.gameObject.name + \"/\");");  
        sw.WriteLine("\t\t" + "}" + "\n");
        sw.WriteLine("\t" + "}" + "\n");

        sw.WriteLine("\t" + "void Awake() {");
        sw.WriteLine("\t\t" + "this.load_all_object(this.gameObject, \"\");" + "\n");
        sw.WriteLine("\t" + "}" + "\n");
        sw.WriteLine("}");
        sw.Flush();
        sw.Close();

        Debug.Log("Gen: " + Application.dataPath + "/Scripts/UI_auto_gen/" + fileName + ".cs");

        if (File.Exists(Application.dataPath + "/Scripts/UI_controllers/" + className + ".cs")) {
            return;
        }

        sw = new StreamWriter(Application.dataPath + "/Scripts/UI_controllers/" + className + ".cs");
        sw.WriteLine(
            "using UnityEngine;\nusing System.Collections;\nusing UnityEngine.UI;\nusing System.Collections.Generic;");

        sw.WriteLine("public partial class " + className + " : MonoBehaviour {" + "\n");


        sw.WriteLine("\t" + "void Start() {");
        sw.WriteLine("\t" + "}" + "\n");
        sw.WriteLine("}");
        sw.Flush();
        sw.Close();

        Debug.Log("Gen: " + Application.dataPath + "/Scripts/UI_controllers/" + className + ".cs");
    }
}
