
using UnityEngine;
using System.Collections;

public class ControlScript: MonoBehaviour {

	// Use this for initialization
	private bool showStage = false;
	private bool showAirLight = false;
	private bool showLearlight = false;
	private bool showRearlight = false;
	private bool showAudience = false;

	public GameObject airLight;
	public string stageData,airLightData,leftEarlightData,rightEarlightData,audienceData;
    public string tips;

//	public GameObject earLLight;
//  public GameObject earRLight;
	public GameObject LearlightPerf;
	public GameObject RearlightPerf;

	private bool boolLEar = false;
	private bool boolREar = false;

	private GameObject activePanel;
	private Vector3 pos;
	float f = 0.002980626f;
	private bool isUp = true;

//	private float virtualWidth = 960.0f;
//	private float virtualHeight = 800.0f;
//	private Matrix4x4 matrix;
	
	void Start () {
        stageData = "舞台长度: 70M"+"\n"+"舞台深度: 35M"+"\n"+"舞台高度: 20M";
        airLightData = "吊杆类型: 电动吊杆"+"\n"+"吊杆承载数量: 15"+"\n"+"吊杆最大承重: 10kN "+"\n"+"最大移动速度: 1.5M/s";
		leftEarlightData = "吊灯类型: 左耳灯"+"\n"+"吊灯数量: 30";
		rightEarlightData = "吊灯类型: 右耳灯"+"\n"+"吊灯数量: 30";
		audienceData = "观众席"+"\n"+"最大容纳人数: 5000";
		tips = "B键上/下移动吊杆"+"\n"+"Q键以漫游者为中心打开/关闭左排耳灯"+"\n"+"E键以漫游者为中心打开/关闭右排耳灯";

//		pos = new Vector3(0, -0.95f, 0);
		pos = new Vector3((Screen.width/2-180)*f, (Screen.height/2-80)*f, 0f);
		activePanel = GameObject.FindGameObjectWithTag("activePanel");
		activePanel.transform.position = pos;
//		matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Screen.width/virtualWidth, Screen.height/virtualHeight, 1.0f));
	}

	// Update is called once per frame
   	 
	void Update () {

		Ray ray = Camera.mainCamera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit = new RaycastHit();
		bool boolHit = Physics.Raycast(ray, out hit);
		if (boolHit)
        {
            if (hit.transform.name == "Stage")
            {       
                showStage = true;
            }
            if (hit.transform.name == "airLight")
            {
                showAirLight = true;
            }
			if (hit.transform.tag == "left_earlight")
			{       
				showLearlight = true;
			}
			if (hit.transform.tag == "right_earlight")
			{
				showRearlight = true;
			}
			if (hit.transform.tag == "Audience")
			{
				showAudience = true;
			}
		}
		else
		{
			showStage = false;
			showAirLight = false;
			showLearlight = false;
			showRearlight = false;
			showAudience = false;
		}

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (isUp)
            {
                airLight.animation.Play("Move");
                isUp = false;
            }else{
				airLight.animation.Play("Up");
				isUp = true;
			}
        }

		//Q for Left-earlight
        if (Input.GetKeyDown(KeyCode.Q))
        {
			if(!boolLEar)
			{
				for(int i=244; i<=273; i++)
				{
					string cyl = "Cylinder";
					cyl += i;
					GameObject objtmp = GameObject.Find(cyl);
					GameObject offer = Instantiate(LearlightPerf) as GameObject;
					offer.transform.position = objtmp.transform.position;
					offer.transform.parent = objtmp.transform;
					offer.transform.LookAt(GameObject.FindGameObjectWithTag("MainCamera").transform.position);

					objtmp.transform.renderer.materials[1].color = Color.green;
				}
				boolLEar = !boolLEar;
			}
			else
			{
				for(int i=244; i<=273; i++)
				{
					string cyl = "Cylinder";
					cyl += i;
					GameObject objtmp = GameObject.Find(cyl);
					Transform offer = objtmp.transform.FindChild("LearlightPerf(Clone)");
					offer.parent = null;
					Destroy(offer.gameObject);
					objtmp.transform.renderer.materials[1].color = Color.gray;
				}
				boolLEar = !boolLEar;
			}
        }

		//E for Right-earlight
		if (Input.GetKeyDown(KeyCode.E))
		{
			if(!boolREar)
			{
				for(int i=214; i<=243; i++)
				{
					string cyl = "Cylinder";
					cyl += i;
					GameObject objtmp = GameObject.Find(cyl);
					GameObject offer = Instantiate(RearlightPerf) as GameObject;
					offer.transform.position = objtmp.transform.position;
					offer.transform.parent = objtmp.transform;
					offer.transform.LookAt(GameObject.FindGameObjectWithTag("MainCamera").transform.position);
					
					objtmp.transform.renderer.materials[1].color = Color.blue;
				}
				boolREar = !boolREar;
			}
			else
			{
				for(int i=214; i<=243; i++)
				{
					string cyl = "Cylinder";
					cyl += i;
					GameObject objtmp = GameObject.Find(cyl);
					Transform offer = objtmp.transform.FindChild("RearlightPerf(Clone)");
					offer.parent = null;
					Destroy(offer.gameObject);
					objtmp.transform.renderer.materials[1].color = Color.gray;
				}
				boolREar = !boolREar;
			}
		}


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

	}

    void OnGUI()
    {
//		GUI.matrix = matrix;
		pos = new Vector3((Screen.width/2-180)*f, (Screen.height/2-80)*f, 0f);
		activePanel = GameObject.FindGameObjectWithTag("activePanel");
		activePanel.transform.position = pos;

		GUIStyle style = new GUIStyle();
		style.fontSize = 18;
		style.normal.textColor = Color.white;

		GUIStyle styleField = new GUIStyle();
		styleField.fontSize = 22;
		styleField.normal.textColor = Color.white;

		if (showStage)
		{
			GUI.TextField(new Rect(100, 100, 200, 200), stageData, styleField);
		}
		if (showAirLight)
		{
			GUI.TextField(new Rect(300, 100, 200, 200), airLightData, styleField);
		}
		if (showLearlight)
		{
			GUI.TextField(new Rect(100, 300, 200, 200), leftEarlightData, styleField);
		}
		if (showRearlight)
		{
			GUI.TextField(new Rect(300, 300, 200, 200), rightEarlightData, styleField);
		}
		if (showAudience)
		{
			GUI.TextField(new Rect(500, 100, 200, 200), audienceData, styleField);
		}
     
		GUI.Label(new Rect(10, 10, 100, 100), tips, style); 
    }

}