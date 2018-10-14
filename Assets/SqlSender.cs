using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SqlSender : MonoBehaviour {

    public string m_strServerAddr = "127.0.0.1";
    public string m_strDataBase = "test";

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 100), "Send"))
        {
            string strQuery = "SELECT * FROM ncsrpg";
            eResultMode mode = eResultMode.SELECT;
            StartCoroutine(GetSqlResult(mode, strQuery));
        }
    }

    public enum eResultMode { CHECK, SELECT};

    public string GetSelectQuery(string table, string where)
    {
        return string.Format("SELECT * FROM {0} WHERE {1}", table, where);
    }

    public string GetInsertQuery(string table, string ids, string values)
    {
        return string.Format("INSERT INTO {0} ({1}) VALUES ({2})", table, ids, values);
    }

    IEnumerator GetSqlResult(eResultMode mode, string sql)
    {
        WWWForm form = new WWWForm();

        form.AddField("servername", m_strServerAddr);
        form.AddField("database", m_strDataBase);
        form.AddField("mode", string.Format("{0}", (int)mode));
        form.AddField("sql", sql);
        Debug.Log(string.Format("servername:{0} database:{1} sql:{2}", m_strServerAddr, m_strDataBase, sql));

        WWW webRequest = new WWW(string.Format("http://{0}/sql_query_result.php"
            , m_strServerAddr), form);
        yield return webRequest;

        if(webRequest.isDone)
        {
            Debug.Log("Find:"+webRequest.text);
        }
        else
        {
            Debug.LogError("Filed : " + webRequest.error);
        }
        
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
