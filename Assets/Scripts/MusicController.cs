using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicController : MonoBehaviour
{
	public List<AudioClip> music;

	public int bands;
	public float amplification;

	private int currentTrack = 0;

	private float[] spectrum;

	private void Start ()
	{
	}

	private void Update ()
	{
		if (!GetComponent<AudioSource> ().isPlaying)
		{
			GetComponent<AudioSource> ().PlayOneShot (music[currentTrack]);
			currentTrack++;

			if (currentTrack >= music.Count)
			{
				currentTrack = 0;
			}
		}

		spectrum = new float[bands];

		GetComponent<AudioSource> ().GetSpectrumData (spectrum, 0, FFTWindow.BlackmanHarris);

		/*
		for (int i = 1; i < spectrum.Length - 1; i++)
		{
			Debug.DrawLine(new Vector3(i - 1, spectrum[i]*amplification + 10, 0), new Vector3(i, spectrum[i+1]*amplification + 10, 0), Color.red);
			Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i-1]*amplification) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]*amplification) + 10, 2), Color.cyan);
			Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i-1]*amplification - 10, 1), new Vector3(Mathf.Log(i), spectrum[i]*amplification - 10, 1), Color.green);
			Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i-1]*amplification), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]*amplification), 3), Color.blue);
		}
		*/
	}

	public float GetBandData ( int band )
	{
		return spectrum[band] * amplification;
	}
}