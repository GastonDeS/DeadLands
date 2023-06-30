using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Utilities;

public class LoadGameAsyncManager : MonoBehaviour
{
    [SerializeField] private Image           _progressBar;
    [SerializeField] private TextMeshProUGUI _progressValue;
    [SerializeField] private string          _targetScene = UnityScenes.SampleScene.DisplayName();

    private void Start()
    {
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(_targetScene);
        float progress = 0;

        operation.allowSceneActivation = false;

        while(!operation.isDone)
        {
            progress = operation.progress;
            _progressBar.fillAmount = progress;
            _progressValue.text = $"{progress * 100}%";

            if (progress >= .9f)
            {
                _progressValue.text = $"Press SPACE to continue";
                if (Input.GetKeyDown(KeyCode.Space)) operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
