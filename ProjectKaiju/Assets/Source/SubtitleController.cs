using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleController : MonoBehaviour {

    public static SubtitleController singleton;

    public Image face;
    public Text words;
    public Image background;
    bool displaying = false;
    public List<Sprite> faceQ = new List<Sprite>();
    public List<string> speechQ = new List<string>();
    public List<AudioSource> sourceQ = new List<AudioSource>();

    void Awake()
    {
        if(!singleton)
        {
            singleton = this;
        }
    }

    public void StartSubtitles(Sprite speaker, string speech , AudioSource playing)
    {
        faceQ.Add(speaker);
        speechQ.Add(speech);
        sourceQ.Add(playing);
        if(!displaying)
        {
            displaying = true;
            StartCoroutine(ShowSubtitles());
        }
     
    }

    IEnumerator ShowSubtitles()
    {
        do
        {
            yield return new WaitForSeconds(0.5f);
            face.sprite = faceQ[0];
            words.text = speechQ[0];
            sourceQ[0].Play();
            face.enabled = true;
            words.enabled = true;
            background.enabled = true;
            yield return new YieldForAudio(sourceQ[0]);
            faceQ.RemoveAt(0);
            speechQ.RemoveAt(0);
            sourceQ.RemoveAt(0);
            yield return null;
        } while (faceQ.Count > 0);
        
        face.enabled = false;
        words.enabled = false;
        background.enabled = false;
        displaying = false;
        yield return null;
    }

}
