using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }
    [field: SerializeField] public AudioMixerGroup AudioMixerGroup { get; set; }

    /// <summary>
    /// First method that will be called when a script is enabled. Only called once.
    /// </summary>
    void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time. Only called once.
    /// </summary>
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
