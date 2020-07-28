using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class BmsLoader : MonoBehaviour
{
    public static string bmsFileName;
    public bool isFinishLoad = false;
    public Bms bms;
    public List<GameObject> objectList;
    public List<GameObject> objectList1;
    public List<GameObject> objectList2;
    public List<GameObject> objectList3;
    public List<GameObject> objectList4;
    public List<GameObject> objectList5;
    public float W = 40.0f;
    public GameObject Note135;
    public GameObject Note24;
    public GameObject node_top;
    private AudioManager theAudio;
    private float noteSpeed;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        initialize();
        setVariables();
    }
        
    void initialize()
    {
        isFinishLoad = false;
    }

    void setVariables()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        if (node_top == null)
        {
            node_top = GameObject.Find("node_top");
        }

        if (Note135 == null)
        {
            Note135 = node_top;
        }

        if (Note24 == null)
        {
            Note24 = node_top;
        }

        if (objectList == null)
        {
            objectList = new List<GameObject>();
        }

        if (bms == null)
        {
            bms = gameObject.AddComponent<Bms>();
        }

        if (theAudio == null)
        {
            theAudio = AudioManager.instance;
        }
    }

    public void Restart(string musicName)
    {
        initialize();
        BmsLoad(musicName);
    }

    public void DestroyAllObject()
    {
        foreach(GameObject gameObject in objectList)
        {
            if(gameObject != null)
            {
                Destroy(gameObject);
            }
        }

        objectList = new List<GameObject>();
    }

    public void BmsLoad(string musicName)
    {
        setVariables();

        bmsFileName = musicName + ".bms";
        //string[] lineData = File.ReadAllLines(pathForFiles("axion.bms"));


        W = 40;
        bms = gameObject.AddComponent<Bms>();

        TextAsset dddata = (TextAsset)Resources.Load("BMS/"+ musicName, typeof(TextAsset));

        StringReader sr = new StringReader(dddata.text);
        string line = sr.ReadLine();

        while (line != null)
        {
            //Debug.Log(line);
            if (line.StartsWith("#"))
            {
                string[] data = line.Split(' ');

                // exception
                if (data[0].IndexOf(":") == -1 && data.Length == 1)
                {
                    continue;
                }

                // header field
                if (data[0].Equals("#TITLE"))
                {
                    bms.setTitle(data[1]);
                }
                else if (data[0].Equals("#ARTIST"))
                {
                    bms.setArtist(data[1]);
                }
                else if (data[0].Equals("#BPM"))
                {
                    bms.setBpm(double.Parse(data[1]));
                    noteSpeed = W / (4.0f / (float)bms.getBpm() * 60);
                }
                else if (data[0].Equals("#PLAYER"))
                {
                }
                else if (data[0].Equals("#GENRE"))
                {
                }
                else if (data[0].Equals("#PLAYLEVEL"))
                {
                }
                else if (data[0].Equals("#RANK"))
                {
                }
                else if (data[0].Equals("#TOTAL"))
                {
                }
                else if (data[0].Equals("#VOLWAV"))
                {
                }
                else if (data[0].Equals("#MIDIFILE"))
                {
                }
                else if (data[0].Equals("#WAV01"))
                {

                }//
                else if (data[0].Equals("#WAV02"))
                {
                }//
                else if (data[0].Equals("#BMP01"))
                {
                }//
                else if (data[0].Equals("#STAGEFILE"))
                {
                }
                else if (data[0].Equals("#VIDEOFILE"))
                {
                }
                else if (data[0].Equals("#BGA"))
                {
                }
                else if (data[0].Equals("#STOP"))
                {
                }
                else if (data[0].Equals("#LNTYPE"))
                {
                    bms.setLnType(int.Parse(data[1]));
                }
                else if (data[0].Equals("#LNOBJ"))
                {
                }
                else if (data[0].Contains("#WAV"))
                {
                }
                else
                {
                    // data field
                    int bar = 0; // node key
                    Int32.TryParse(data[0].Substring(1, 3), out bar);

                    int channel = 0; // node channel
                    Int32.TryParse(data[0].Substring(4, 2), out channel);

                    string noteStr = data[0].Substring(7);

                    List<int> noteData = getNoteDataOfStr(noteStr);

                    Note note = gameObject.AddComponent<Note>();
                    note.setBar(bar);
                    note.setChannel(channel);
                    note.setNoteData(noteData);
                    //note.debug();

                    bms.addNote(note);
                    isFinishLoad = true;
                }
            }

            line = line = sr.ReadLine();
        }

        

        //string[] lineData = File.ReadAllLines(pathForFiles(bmsFileName));

        //foreach (string line in lineData)
        //{
        //    //Debug.Log(line);
        //    if (line.StartsWith("#"))
        //    {
        //        string[] data = line.Split(' ');

        //        // exception
        //        if (data[0].IndexOf(":") == -1 && data.Length == 1)
        //        {
        //            continue;
        //        }

        //        // header field
        //        if (data[0].Equals("#TITLE"))
        //        {
        //            bms.setTitle(data[1]);
        //        }
        //        else if (data[0].Equals("#ARTIST"))
        //        {
        //            bms.setArtist(data[1]);
        //        }
        //        else if (data[0].Equals("#BPM"))
        //        {
        //            bms.setBpm(double.Parse(data[1]));
        //            noteSpeed = W / (4.0f / (float)bms.getBpm() * 60);
        //        }
        //        else if (data[0].Equals("#PLAYER"))
        //        {
        //        }
        //        else if (data[0].Equals("#GENRE"))
        //        {
        //        }
        //        else if (data[0].Equals("#PLAYLEVEL"))
        //        {
        //        }
        //        else if (data[0].Equals("#RANK"))
        //        {
        //        }
        //        else if (data[0].Equals("#TOTAL"))
        //        {
        //        }
        //        else if (data[0].Equals("#VOLWAV"))
        //        {
        //        }
        //        else if (data[0].Equals("#MIDIFILE"))
        //        {
        //        }
        //        else if (data[0].Equals("#WAV01"))
        //        {

        //        }//
        //        else if (data[0].Equals("#WAV02"))
        //        {
        //        }//
        //        else if (data[0].Equals("#BMP01"))
        //        {
        //        }//
        //        else if (data[0].Equals("#STAGEFILE"))
        //        {
        //        }
        //        else if (data[0].Equals("#VIDEOFILE"))
        //        {
        //        }
        //        else if (data[0].Equals("#BGA"))
        //        {
        //        }
        //        else if (data[0].Equals("#STOP"))
        //        {
        //        }
        //        else if (data[0].Equals("#LNTYPE"))
        //        {
        //            bms.setLnType(int.Parse(data[1]));
        //        }
        //        else if (data[0].Equals("#LNOBJ"))
        //        {
        //        }
        //        else if (data[0].Contains("#WAV"))
        //        {
        //        }
        //        else
        //        {
        //            // data field
        //            int bar = 0; // node key
        //            Int32.TryParse(data[0].Substring(1, 3), out bar);

        //            int channel = 0; // node channel
        //            Int32.TryParse(data[0].Substring(4, 2), out channel);

        //            string noteStr = data[0].Substring(7);
                    
        //            List<int> noteData = getNoteDataOfStr(noteStr);

        //            Note note = gameObject.AddComponent<Note>();
        //            note.setBar(bar);
        //            note.setChannel(channel);
        //            note.setNoteData(noteData);
        //            //note.debug();

        //            bms.addNote(note);
        //            isFinishLoad = true;
        //        }
        //    }
        //}
        if (isFinishLoad)
        {
            float musicStartTimeBias = 0;
            float time_delay = 3.0f;

            if(gameManager == null)
            {
                gameManager = FindObjectOfType<GameManager>();
            }

            float bias = gameManager.getMusicBias(musicName);
            float biasByTimeDelay = time_delay * noteSpeed;

            foreach (Note note in bms.getNoteList())
            {
                int noteNum = note.noteData.Count;
                int cnt = 0;

                foreach (int i in note.noteData)
                {
                    if (i == 1)
                    {
                        switch (note.channel)
                        {
                            case 11:
                                GameObject r1 = Instantiate(Note135);
                                r1.transform.localPosition = new Vector3(-10, -15, biasByTimeDelay + bias + (note.bar * W) + (W / noteNum * cnt));
                                objectList.Add(r1);
                                break;
                            case 12:
                                GameObject r2 = Instantiate(Note24);
                                r2.transform.localPosition = new Vector3(-5, -15, biasByTimeDelay + bias + (note.bar * W) + (W / noteNum * cnt));
                                objectList.Add(r2);
                                break;
                            case 13:
                                GameObject r3 = Instantiate(Note135);
                                r3.transform.localPosition = new Vector3(0, -15, biasByTimeDelay + bias + (note.bar * W) + (W / noteNum * cnt));
                                objectList.Add(r3);
                                break;
                            case 14:
                                GameObject r4 = Instantiate(Note24);
                                r4.transform.localPosition = new Vector3(5, -15, biasByTimeDelay + bias + (note.bar * W) + (W / noteNum * cnt));
                                objectList.Add(r4);
                                break;
                            case 15:
                                GameObject r5 = Instantiate(Note135);
                                r5.transform.localPosition = new Vector3(10, -15, biasByTimeDelay + bias + (note.bar * W) + (W / noteNum * cnt));
                                objectList.Add(r5);
                                break;
                        }
                    }
                    else if(i == 2)
                    {
                        float musicStartBias = (note.bar * W) + (W / noteNum * cnt);
                        musicStartTimeBias = musicStartBias / noteSpeed;
                    }

                    cnt++;
                }

            }
 
            theAudio.playBGMWithDelay(musicName, musicStartTimeBias  + time_delay);
        }
    }
    private List<int> getNoteDataOfStr(string str)
    {
        string tempStr = str;
        List<int> noteList = new List<int>();

        while (true)
        {
            if (tempStr.Length > 2)
            {
                int data = 0;
                Int32.TryParse(tempStr.Substring(0, 2), out data);

                noteList.Add(data);
                tempStr = tempStr.Substring(2);
            }
            else
            {
                int data = 0;
                Int32.TryParse(tempStr, out data);
                noteList.Add(data);
                break;
            }

        }

        // 총노트수 증가
        foreach (int note in noteList)
        {
            if (note != 0)
            {
                bms.sumTotalNoteCount();
            }
        }

        return noteList;
    }

    void Update()
    {
        Double bpm = bms.getBpm();
        //  Debug.Log(HP_Manager.isGameEnd.ToString());
        foreach (GameObject o in objectList)
        {
            if (o != null) o.transform.position += new Vector3(0f, 0f, -Time.deltaTime * noteSpeed);
          /*  if (HP_Manager.isGameEnd)
            {
                Debug.Log("Hp 0 game end");
                Destroy(this);
            }*/
        }
    }
    public string pathForFiles(string filename)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            string path = Application.persistentDataPath;
            return Path.Combine(path, filename);
        }
        else
        {
           string path = Application.dataPath;

            return Path.Combine(path, filename);
        }
    }

    public void destroyNote(GameObject note)
    {
        Destroy(note);
    }
}
