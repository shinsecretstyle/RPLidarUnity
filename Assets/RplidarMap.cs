using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class RplidarMap : MonoBehaviour
{

    private static readonly int s_MaxLength = 8192;

    public string m_COM = "COM3";
    public int m_Baudrate = 256000;
    public MeshFilter m_Filter;

    bool m_onScan = false;
    LidarData[] m_Data;
    Mesh m_Mesh;
    List<Vector3> m_Vert = new List<Vector3>();
    List<int> m_Index = new List<int>();
    Thread m_ThreadReceive;
    bool m_DataChanged = false;
    int m_DataCount = 0;

    public static bool updatingData = true;
    // Start is called before the first frame update
    void Start()
    {
        m_Data = new LidarData[s_MaxLength];
        for(int i = 0; i < 360; i++)
        {
            m_Index.Add(i);
        }
        m_Mesh = new Mesh();
        m_Mesh.MarkDynamic();
        RplidarBinding.OnConnect(m_COM, m_Baudrate);
        RplidarBinding.StartMotor();
        m_onScan = RplidarBinding.StartScan();

        if (m_onScan)
        {
            m_ThreadReceive = new Thread(RPLidarAX)
            {
                IsBackground = true
            };
            m_ThreadReceive.Start();
        }   
    }
    private void Update()
    {
        UpdateData();
    }

    void OnDestory()
    {
        if(m_ThreadReceive != null)
        {
            m_ThreadReceive.Abort();
            m_ThreadReceive = null;
            Debug.Log("close thread");
        }

        RplidarBinding.EndScan();
        RplidarBinding.EndMotor();
        RplidarBinding.onDisconnect();
        RplidarBinding.ReleaseDrive();

        m_onScan = false;
    }
    

    private void RPLidarAX()
    {
        while(true)
        {
            m_DataCount = RplidarBinding.GetData(ref m_Data);

            if(m_DataCount == 0)
            {
                Thread.Sleep(20);
            }else m_DataChanged = true;
            
        }
    }

    public void UpdateData()
    {
        if (updatingData)
        {
            if (m_DataChanged)
            {
                m_Vert.Clear();

                for (int i = 0; i < m_DataCount; i++)
                {

                    m_Vert.Add(Quaternion.Euler(0, 0, m_Data[i].theta) * Vector3.right * m_Data[i].distant * 0.001f);
                }

                m_Mesh.SetVertices(m_Vert);
                m_Mesh.SetIndices(m_Index.ToArray(), MeshTopology.Points, 0);

                m_Mesh.UploadMeshData(false);
                m_Filter.mesh = m_Mesh;
                m_DataChanged = false;
            }
        }
    }
}
