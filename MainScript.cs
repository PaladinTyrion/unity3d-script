using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
public class MainScript : MonoBehaviour {

    public Transform thePaiZi;

    public GameObject theInputPar;

	public GameObject theShowPanel;

    Dictionary<string, float> myDictionary;
    
	// Use this for initialization
	IEnumerator Start () {

       myDictionary  = new Dictionary<string, float>();

        string path = Application.dataPath;

        if (Application.isWebPlayer)
        {
            path += @"/std.xml";
        }
        else
        {
            path = "file:///"+path + @"/std.xml";
        }

        WWW data = new WWW(path);
        yield return data;
     
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(data.text);
        XmlNodeList nodelist = xmlDoc.SelectSingleNode("root").ChildNodes;

        foreach (XmlElement xe in nodelist)
        {
            myDictionary.Add(xe.Name, float.Parse(xe.InnerText));

        }
	}

    void IsValidate(UIInput val)
    {
        if (!string.IsNullOrEmpty(val.value))
        {
            print(val.value);

            if (float.Parse(val.value) < myDictionary["s"+val.name.ToLower()])
            {
                GameObject go = thePaiZi.FindChild(val.name).gameObject;
                go.SetActive(true);
                go.transform.FindChild("Fail").gameObject.SetActive(false);
                go.transform.FindChild("Success").gameObject.SetActive(true);
            }
            else
            {
                GameObject go = thePaiZi.FindChild(val.name).gameObject;
                go.SetActive(true);
				go.transform.FindChild("Success").gameObject.SetActive(false);
                GameObject go1 = go.transform.FindChild("Fail").gameObject;
                go1.SetActive(true);
                go1.transform.FindChild("StandInput").GetComponent<TextMesh>().text = "标准值：" + myDictionary["s" + val.name.ToLower()];
                go1.transform.FindChild("UserInput").GetComponent<TextMesh>().text = "输入值：" +val.value;
            }
        }
    }


    void OnOkBtnClicked()
    {
		foreach (Transform ts in thePaiZi.transform)
		{
			ts.gameObject.SetActive(false);
		}

        foreach (UIInput ui in theInputPar.GetComponentsInChildren<UIInput>())
        {
           IsValidate(ui);
        }
        theShowPanel.SetActive(false);
    }


    void OnClearBtnClicked()
    {
        foreach (Transform ts in thePaiZi.transform)
        {
            ts.gameObject.SetActive(false);
        }
		theShowPanel.SetActive(false);
    }
}
