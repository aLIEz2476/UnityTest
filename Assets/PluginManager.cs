using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PluginManager : MonoBehaviour {
    public int n_Time;
    public string str_Msg;
#if UNITY_ANDROID
    private AndroidJavaObject m_AndroidJavaObject=null;
    private AndroidJavaObject m_ActivityInstance=null;
#elif UNITY_IOS
#endif
    int nNativeData = 0;
    // Use this for initialization
    void Awake () {
#if UNITY_ANDROID
        using (AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            m_ActivityInstance = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
        }

        m_AndroidJavaObject = new AndroidJavaObject("com.sbsgame.android.unityandroidplugin.Plugin");
        Debug.LogWarning("PluginManager.AndroidJavaObject : " + m_AndroidJavaObject);
#endif
    }
    private void Start()
    {
        
    }
    int GetInt()
    {
        int nResult = -1;
#if UNITY_ANDROID
        nResult = m_AndroidJavaObject.Call<int>("GetInt");
#endif
        return nResult;
    }

    void ToastTest()
    {
        m_AndroidJavaObject.Call("SetActivity", m_ActivityInstance);
        m_AndroidJavaObject.Call("ToastTest", str_Msg, n_Time);
        
    }

    private void OnGUI()
    {
        
        if(GUI.Button(new Rect(300, 300, 1000, 200), "TestButton"))
        {
            ToastTest();
        }
       
        nNativeData = GetInt();
        if (m_AndroidJavaObject != null)
        {
            GUI.Box(new Rect(0, 0, 200, 20), "GetInt : " + nNativeData);
        }
        else
            GUI.Box(new Rect(0, 0, 200, 20), "Plugin Load Failed!");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
