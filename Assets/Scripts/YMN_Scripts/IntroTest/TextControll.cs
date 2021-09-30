using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using DG.Tweening;

public class TextControll : MonoBehaviour
{
    [SerializeField] Text _text;
    [SerializeField] GameObject icon;
    private Queue<string> sentences;
    // private int count;

    public float TEXT_SPEED = 0.1f;

    void Awake()
    {
        sentences = new Queue<string>();
        var textAsset = Resources.Load ("sample") as TextAsset;
        StringReader stringReader = new StringReader(textAsset.text);

        while (stringReader.Peek() != -1){
            sentences.Enqueue(stringReader.ReadLine());
        }

        // count = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(icon.transform.position.y);
        float _endY = icon.transform.position.y + 1f;
        icon.transform.DOMoveY(_endY, 0.2f).SetLoops(-1, LoopType.Yoyo);
        Debug.Log(_endY);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            _text.text = "";
            if(sentences.Count!=0){
                string message = sentences.Dequeue();
                _text.DOText(message,message.Length*TEXT_SPEED).SetEase(Ease.Linear);
            }else{
                icon.SetActive(false);
            }
        }
    }
}
