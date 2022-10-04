using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    // Faces for 6 sides of the cube
    private GameObject[] quads = new GameObject[6];

    // Textures for each quad, should be +X, +Y etc
    // with appropriate colors, red, green, blue, etc
    public Texture[] labels;

    void Start()
    {

        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }

        // make camera solid colour and based at the origin
        GetComponent<Camera>().backgroundColor = new Color(49.0f / 255.0f, 77.0f / 255.0f, 121.0f / 255.0f);
        GetComponent<Camera>().transform.position = new Vector3(0, 0, 0);
        GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;

        
    }

    // make a quad for one side of the cube
    GameObject createQuad(GameObject quad, Vector3 pos, Vector3 rot, string name, Color col, Texture t)
    {
        Quaternion quat = Quaternion.Euler(rot);
        GameObject GO = Instantiate(quad, pos, quat);
        GO.name = name;
        GO.GetComponent<Renderer>().material.color = col;
        GO.GetComponent<Renderer>().material.mainTexture = t;
        GO.transform.localScale += new Vector3(0.25f, 0.25f, 0.25f);
        return GO;
    }

    protected void Update()
    {
        GyroModifyCamera();

    }

    public float movementScale = 1.0f;

    bool CheckUpright()
    {
        Vector3 pos = transform.position;
        //pos.y = Vector3.Dot(Input.gyro.gravity, Vector3.up) * movementScale;
        //transform.position = pos;
        return Vector3.Dot(Input.gyro.gravity, Vector3.up) < 0.1;
    }

    protected void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 40;

        GUILayout.Label("Orientation: " + Screen.orientation);
        GUILayout.Label("input.gyro.attitude: " + Input.gyro.attitude);
        GUILayout.Label("input.gyro.attitude in euler: " + Input.gyro.attitude.eulerAngles);
        GUILayout.Label("iphone width/font: " + Screen.width + " : " + GUI.skin.label.fontSize);

        Vector3 gr = Input.gyro.gravity;
        //Vector3 funnyGravity = new Vector3(

        GUILayout.Label("near upright?: " + Vector3.Dot(gr, Vector3.up));
    }

    /********************************************/

    // The Gyroscope is right-handed.  Unity is left handed.
    // Make the necessary change to the camera.
    void GyroModifyCamera()
    {
        Quaternion cool =   GyroToUnity(Input.gyro.attitude);
        //cool.eulerAngles = new Vector3(cool.eulerAngles.x, 0,cool.eulerAngles.z);
        Quaternion temp = cool;
        cool = Quaternion.identity;
        cool.y = temp.z;
        transform.rotation = cool;
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
