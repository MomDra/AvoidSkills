using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Command
{
    public void cmd(Transform player, PlayerStatus status);
}

public abstract class SkillCommand : MonoBehaviour, Command
{
    [SerializeField]
    protected SkillInfo skillInfo;
    public SkillInfo SkillInfo { get => skillInfo; }
    public int currUsableCount{ get; set; }

    private void Awake() {
        currUsableCount = skillInfo.usableCount;
    }

    public void cmd(Transform player, PlayerStatus status){
        StartCoroutine(PreviewUpdateCoroutine(player, status));
    }  

    private IEnumerator PreviewUpdateCoroutine(Transform player, PlayerStatus status){
        GeneratePreview(player);
        while(true){
            if(Input.GetMouseButtonDown(0)){
                DestroyPreview();
                run(player, status);
                break;
            }else if(Input.GetKeyDown(KeyCode.Escape)){
                DestroyPreview();
                break;
            }
            previewUpdate(player);
            yield return null;
        }
    }

    public abstract void GeneratePreview(Transform player);
    public abstract void previewUpdate(Transform player);
    public abstract void DestroyPreview();
    public abstract void run(Transform player, PlayerStatus status);
}