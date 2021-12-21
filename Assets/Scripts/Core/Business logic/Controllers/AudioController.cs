using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;
    [field: SerializeField] public AudioMixerGroup AudioMixerGroup { get; set; }

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < SoundDatabase.Instance.Table.Elements.Count; i++)
        {
            GameObject go = new GameObject(SoundDatabase.Instance.Table.Elements[i].Id);
            go.transform.SetParent(transform);
            SoundDatabase.Instance.Table.Elements[i].SetSource(go.AddComponent<AudioSource>());
        }
    }
}
