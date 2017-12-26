/// <summary>
/// This is a manager script using instance mode.
/// Mainly used for managing audio, which use object pool to generate and destroy audio.
/// Other module only send their base request, how to deal with it depends on this class.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    #region Instance part
    public static AudioManager instance;
    #endregion

    #region Show In Inspector
    [Header ("Generate Setting")]
    [SerializeField]
    private Transform _audioParent;

    [SerializeField]
    private int _initCount;

    [SerializeField]
    private AudioSource _audioSourcePrefab;

    [Header ("Audio Clip Setting")]
    [SerializeField]
    private AudioClip _generateClip;
    [SerializeField]
    private AudioClip _startBtn1;
    [SerializeField]
    private AudioClip _startBtn2;
    [SerializeField]
    private AudioClip _tapSFX;
    [SerializeField]
    private AudioClip _boomSFX;
    [SerializeField]
    private List<AudioClip> _pianoClipList = new List<AudioClip> ();
    #endregion

    #region Hide In Inspector
    private List<AudioSource> _audioPool = new List<AudioSource> ();
    private int _comboCount = -1;
    private int _preFrameCount = -1;
    private int _preBoomFrameCount = -1;
    #endregion

    #region Init Part
    private void Awake () {
        Init ();
    }

    private void Init () {
        InitInstance ();
        InitObjectPool ();
    }

    private void InitInstance () {
        if (!instance) {
            instance = this;
        }
    }

    private void InitObjectPool () {
        if (_initCount > 0) {
            for (int i = 0; i < _initCount; ++i) {
                CreateNewAudioSource ();
            }
        }
    }
    #endregion

    #region Update Part

    #endregion

    #region Collision Part

    #endregion

    #region Function Part
    public void PlayGenerateSFX () {
        if (Time.frameCount == _preFrameCount) {
            return;
        }
        _preFrameCount = Time.frameCount;
        AudioSource source = GetNewAudioSource (_generateClip, true);
        if (!source) {
            return;
        } else {
            source.clip = _generateClip;
            source.Play ();
        }
    }

    public void PlayBtnSFX () {
        AudioSource source = GetNewAudioSource (_startBtn1);
        if (!source) {
            return;
        } else {
            source.clip = _startBtn1;
            source.Play ();
        }
    }

    public void PlayTapSFX () {
        if (Time.frameCount == _preFrameCount) {
            return;
        }
        ++_comboCount;
        _preFrameCount = Time.frameCount;
        if (_comboCount >= _pianoClipList.Count) {
            return;
        }
        AudioSource source = GetNewAudioSource (_pianoClipList[_comboCount]);
        if (!source) {
            return;
        } else {
            source.clip = _pianoClipList[_comboCount];
            source.Play ();
        }
    }

    public void PlayTapSFX (int keyNum) {
        if (Time.frameCount == _preFrameCount) {
            return;
        }
        _preFrameCount = Time.frameCount;
        if (keyNum >= _pianoClipList.Count) {
            return;
        }
        AudioSource source = GetNewAudioSource (_pianoClipList[keyNum]);
        if (!source) {
            return;
        } else {
            source.clip = _pianoClipList[keyNum];
            source.Play ();
        }
    }

    public void PlayBoomSFX () {
        if (Time.frameCount == _preBoomFrameCount) {
            return;
        }
        _preBoomFrameCount = Time.frameCount;
        AudioSource source = GetNewAudioSource (_boomSFX);
        if (!source) {
            return;
        } else {
            source.clip = _boomSFX;
            source.Play ();
        }
    }

    public void ResetPiano () {
        _comboCount = -1;
    }

    private AudioSource GetNewAudioSource (AudioClip clip, bool allowSame = false) {
        AudioSource source = null;
        for (int i = 0; i < _audioPool.Count; ++i) {
            if (!_audioPool[i].isPlaying) {
                source = _audioPool[i];
                continue;
            } else {
                if (!allowSame) {
                    if (_audioPool[i].clip == clip) {
                        return _audioPool[i];
                    }
                }
            }
        }
        if (source) {
            return source;
        } else {
            return CreateNewAudioSource ();
        }
    }

    private AudioSource CreateNewAudioSource () {
        _audioPool.Add (Instantiate (_audioSourcePrefab, _audioParent));
        return _audioPool[_audioPool.Count - 1];
    }
    #endregion
}