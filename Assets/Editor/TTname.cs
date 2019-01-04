using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class TTname : MonoBehaviour {

    [MenuItem("Assets/ReMName")]

    public static void ToRename()
    {
        Object[] m_objects = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);//选择的所以对象
        int index = 0;//序号

        foreach (Object item in m_objects)
        {

            string m_name = item.name;
            if (Path.GetExtension(AssetDatabase.GetAssetPath(item)) != "")//判断路径是否为空
            {
                string path = AssetDatabase.GetAssetPath(item);
                AssetDatabase.RenameAsset(path, index.ToString());
                index++;
            }

        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
