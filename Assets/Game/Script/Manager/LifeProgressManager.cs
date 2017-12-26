/// <summary>
/// This is a manager script using instance mode.
/// Mainly used for loging and displaying progress, unlocking word.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class LifeProgressManager : MonoBehaviour {
    #region Instance part
    public static LifeProgressManager instance;
    #endregion

    #region Enum Part
    private enum ProgressState {
        NONE,
        SHOW_PREVIOUS_PROGRESS,
        PREPARE_NEW_PROGRESS,
        SHOW_NEW_PROGRESS,
        SHOW_WORD
    }
    #endregion

    #region Show In Inspector
    [Header ("Word Setting")]
    [SerializeField]
    private string _xmlChinesePath = "Word/Word_Chinese";
    [SerializeField]
    private string _xmlEnglishPath = "Word/Word_English";

    [SerializeField]
    private Font _chineseFont;
    [SerializeField]
    private Font _englishFont;

    [Header ("Prefs Setting")]
    [SerializeField]
    private string _progressPrefName = "Life_Progress";

    [Header ("UI Setting")]
    [SerializeField]
    private Text _wordText;

    [SerializeField]
    private Slider _slider;

    [SerializeField]
    private Image _2ndBG;
    #endregion

    #region Hide In Inspector
    private float _currProgress = 0f;
    private float _newProgress = 0f;
    private float _needTime = 0f;
    private float _timer = 0f;
    private ProgressState _currProgressState = ProgressState.NONE;

    private List<string> _wordFirstList = new List<string> ();
    private List<string> _wordList = new List<string> ();
    #endregion

    #region Init Part
    private void Awake () {
        Init ();
    }

    private void Init () {
        InitInstance ();
        InitProgress ();
    }

    private void InitInstance () {
        if (!instance) {
            instance = this;
        }
    }

    private void InitProgress () {
        if (!PlayerPrefs.HasKey (_progressPrefName)) {
            _currProgress = 0f;
            PlayerPrefs.SetFloat (_progressPrefName, _currProgress);
            PlayerPrefs.Save ();
        } else {
            _currProgress = PlayerPrefs.GetFloat (_progressPrefName);
        }
        _newProgress = _currProgress;

        _slider.value = 0;
        _2ndBG.fillAmount = 0;
    }

    private void InitWordList () {
        LoadWordFromXML ();
    }

    private void Start () {
        InitWordList ();
    }
    #endregion

    #region Update Part
    private void Update () {
        switch (_currProgressState) {
            case ProgressState.NONE:
                UpdateNoneState ();
                break;
            case ProgressState.SHOW_PREVIOUS_PROGRESS:
                UpdateShowPreviousProgressState ();
                break;
            case ProgressState.PREPARE_NEW_PROGRESS:
                UpdatePrepareNewProgressState ();
                break;
            case ProgressState.SHOW_NEW_PROGRESS:
                UpdateShowNewProgressState ();
                break;
            case ProgressState.SHOW_WORD:
                UpdateShowWordState ();
                break;
        }
    }

    private void UpdateNoneState () {
        if (_slider.value != 0) {
            _timer += Time.deltaTime;
            _slider.value = Mathf.Lerp (_slider.value, 0, _timer / _needTime);
            _2ndBG.fillAmount = _slider.value;

            Color color = _wordText.color;
            color.a = Mathf.Lerp (color.a, 0f, _timer / _needTime);
            _wordText.color = color;
        } else {
            _wordText.text = "";
        }
    }

    private void UpdateShowPreviousProgressState () {
        if (_slider.value < _currProgress) {
            _timer += Time.deltaTime;
            _slider.value = Mathf.Lerp (_slider.value, _currProgress, _timer / _needTime);
            _2ndBG.fillAmount = _slider.value;
        } else {
            SwitchState (ProgressState.PREPARE_NEW_PROGRESS);
        }
    }

    private void UpdatePrepareNewProgressState () {
        if (_2ndBG.fillAmount < _newProgress) {
            _timer += Time.deltaTime;
            _2ndBG.fillAmount = Mathf.Lerp (_2ndBG.fillAmount, _newProgress, _timer / _needTime);
        } else {
            SwitchState (ProgressState.SHOW_NEW_PROGRESS);
        }
    }

    private void UpdateShowNewProgressState () {
        if (_slider.value < _newProgress) {
            _timer += Time.deltaTime;
            _slider.value = Mathf.Lerp (_slider.value, _newProgress, _timer / _needTime);
        } else {
            SwitchState (ProgressState.SHOW_WORD);
        }
    }

    private void UpdateShowWordState () {
        if (_wordText.color.a < 0.87f) {
            _timer += Time.deltaTime;
            Color color = _wordText.color;
            color.a = Mathf.Lerp (color.a, 0.87f, _timer / _needTime);
            _wordText.color = color;
        }
    }
    #endregion

    #region Collision Part

    #endregion

    #region State Part
    private void SwitchState (ProgressState state) {
        if (_currProgressState == state) {
            return;
        }

        _currProgressState = state;
        _timer = 0f;
        switch (_currProgressState) {
            case ProgressState.NONE:
                InitNoneState ();
                break;
            case ProgressState.SHOW_PREVIOUS_PROGRESS:
                InitShowPreviousProgressState ();
                break;
            case ProgressState.PREPARE_NEW_PROGRESS:
                InitPrepareNewProgressState ();
                break;
            case ProgressState.SHOW_NEW_PROGRESS:
                InitShowNewProgressState ();
                break;
            case ProgressState.SHOW_WORD:
                InitShowWordState ();
                break;
        }
    }

    private void InitNoneState () {
        _needTime = 0.5f;
    }

    private void InitShowPreviousProgressState () {
        _needTime = 1;
    }

    private void InitPrepareNewProgressState () {
        _needTime = 1;
    }

    private void InitShowNewProgressState () {
        _needTime = 1;
    }

    private void InitShowWordState () {
        _needTime = 0.5f;
        if (LanguageManager.LANGUAGE_TYPE == LanguageManager.LanguageType.CHINESE) {
            _wordText.font = _chineseFont;
        } else {
            _wordText.font = _englishFont;
        }
        for (float i = 1; i.CompareTo (10) <= 0; ++i) {
            float n = i * 0.1f;
            if (_newProgress.CompareTo (n) < 0) {
                if (_currProgress.CompareTo (n - 0.1f) < 0) {
                    _wordText.text = _wordFirstList[(int) i - 1];
                    return;
                } else {
                    int ran = Random.Range (0, 4);
                    int num = (((int) i - 1) * 5) + ran;
                    _wordText.text = _wordList[num];
                    return;
                }
            }
        }
        if (_newProgress.CompareTo (1) == 0) {
            if (_currProgress.CompareTo (1) < 0) {
                _wordText.text = _wordFirstList[10];
                return;
            } else {
                int ran = Random.Range (0, 4);
                int num = 50 + ran;
                _wordText.text = _wordList[num];
                return;
            }
        }
    }
    #endregion

    #region XML Part
    private void LoadWordFromXML () {
        string xmlPath =
            (LanguageManager.LANGUAGE_TYPE == LanguageManager.LanguageType.CHINESE ?
                _xmlChinesePath : _xmlEnglishPath);
        if (!Resources.Load (xmlPath)) {
            return;
        }
        string data = Resources.Load (xmlPath).ToString ();
        XmlDocument xmlDoc = new XmlDocument ();
        xmlDoc.LoadXml (data);

        XmlNodeList wordFirstList = xmlDoc.SelectSingleNode ("XML").SelectSingleNode ("Word_First").ChildNodes;
        XmlNodeList wordList = xmlDoc.SelectSingleNode ("XML").SelectSingleNode ("Word").ChildNodes;

        for (int i = 0; i < wordFirstList.Count; ++i) {
            _wordFirstList.Add (wordFirstList[i].InnerText);
        }

        for (int i = 0; i < wordList.Count; ++i) {
            _wordList.Add (wordList[i].InnerText);
        }
    }
    #endregion

    #region Function Part
    public void AddProgress (float score) {
        _newProgress += score;
        PlayerPrefs.SetFloat (_progressPrefName, _newProgress);
        PlayerPrefs.Save ();
    }

    public void ShowProgress () {
        SwitchState (ProgressState.SHOW_PREVIOUS_PROGRESS);
    }

    public void HideProgress () {
        SwitchState (ProgressState.NONE);
    }
    #endregion
}