using UnityEngine;
using System.Collections;
using System;
public class BeatDetector : MonoBehaviour {

    public float detectionTime = 0.01f;
    public float detectionTimer = 0.0f;

    private AudioClip music;
    private AudioSource audioPlayer;
    private SpriteRenderer marker;

    public float[] historyBuffer = new float[43];
    private float[] audioLeft = new float[1024];
    private float[] audioRight = new float[1024];


	// Use this for initialization
	void Start () {
        audioPlayer = Camera.main.GetComponent<AudioSource>();
        marker = GameObject.Find("BeatMarker").GetComponent<SpriteRenderer>();

        marker.color = Color.blue;

	}
	
	// Update is called once per frame
	void Update () {

        detectionTimer += Time.deltaTime;
            /*
        if (detectionTimer > detectionTime || historyBuffer[historyBuffer.Length-1] == 0)
        {
             * */
            detectionTimer = 0.0f;

            audioPlayer.GetSpectrumData(audioLeft, 1,FFTWindow.Hamming);
            audioPlayer.GetSpectrumData(audioRight, 2, FFTWindow.Hamming);

            float stereo_e = sumStereo(audioLeft, audioRight);

            float local_e = sumLocalEnergy() / historyBuffer.Length;

            float sumV = 0;

            for (int i = 0; i < historyBuffer.Length; i++)
            {
                sumV += (historyBuffer[i] - local_e) * (historyBuffer[i] - local_e);
            }

            float V = sumV / historyBuffer.Length;
            float constant = (float)((-0.0025714 * V) + 1.5142857);

            float[] shiftingHistoryBuffer = new float[historyBuffer.Length];

            Array.Copy(historyBuffer, 0, shiftingHistoryBuffer, 1, historyBuffer.Length -1 );
            shiftingHistoryBuffer[0] = stereo_e;

            Array.Copy(shiftingHistoryBuffer, historyBuffer, historyBuffer.Length );



            if (stereo_e > (constant * local_e))
            { // now we check if we have a beat
                marker.color = Color.red;
                Debug.Log("High");
            }
            else
            {
                marker.color = Color.blue;
                Debug.Log("Low");
            }

            //Debug.Log(V);
        /*
        }
         * */
	}

    private float sumLocalEnergy()
    {
        float e = 0;

        for (int i = 0; i < historyBuffer.Length; i++)
        {
            e += historyBuffer[i] * historyBuffer[i];
        }

        return e;
    }

    private float sumStereo(float[] audioLeft, float[] audioRight)
    {
        float e = 0;
        for(int i = 0 ; i < audioLeft.Length; i ++)
        {
            e += ((audioLeft[i] * audioLeft[i]) + (audioRight[i] * audioRight[i]));
        }

        return e;
    }

    string historybuffer()
    {
        string s = "";
        for (int i = 0; i < historyBuffer.Length; i++)
        {
            s += (historyBuffer[i] + ",");
        }
        return s;
    }
}
