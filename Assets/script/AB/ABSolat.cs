using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABSolat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //打开加载AB包。注意，不能同时加载同一个ab包，会报错
        AssetBundle ab =AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "model");//读取路径

        //获取里面资源；读取AB包资源
        // ab.LoadAsset("Cube");名字加载，有同名资源会混淆，同名不同类型资源;
        //ab.LoadAsset<GameObject>("Cube");泛型加载，可指定类型，便于区分同名不同资源
        //和泛型一样，区别在于Lua语言没有泛型，但是可以用Type指定类型，建议多用这个
        GameObject obj = ab.LoadAsset("Cube", typeof(GameObject)) as GameObject;//获取单一一个资源

        Object[] objList=ab.LoadAllAssets(typeof(GameObject));//获取所有资源

        Vector3 a = new Vector3(10, 20);
        foreach (GameObject item in objList)
        {  
            a.x++;
            Instantiate(item,transform.position=a,Quaternion.identity);
        }

        //卸载AB包，true是吧包里的加载的内容都同时一起卸载，false只卸载AB包,加载了的资源会继续存在
        ab.Unload(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
