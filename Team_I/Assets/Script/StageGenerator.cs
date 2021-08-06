using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{

    const int StageChipSize = 30;

    int currentChipIndex;

    public Transform character;
    public GameObject[] stageChips;
    public int startChipIndex;
    public int preInstantiaste;
    public List<GameObject> generatedStageList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        currentChipIndex = startChipIndex - 1;
        UpdateStage(preInstantiaste);

    }

    // Update is called once per frame
    void Update()
    {
        int charaPositionIndex = (int)(character.position.z / StageChipSize);

        if (charaPositionIndex + preInstantiaste > currentChipIndex)
        {
            UpdateStage(charaPositionIndex + preInstantiaste);
        }
    }

    //指定のIndexまでのステージチップを生成して管理下に置く
    void UpdateStage(int toChipIndex)
    {
        if (toChipIndex <= currentChipIndex) return;
        //指定のステージチップまでを生成       
        for (int i = currentChipIndex + 1; i <= toChipIndex; i++)
        {
            GameObject stageObject = GenerateStage(i);
            //生成したオブジェクトを管理リストに追加
            generatedStageList.Add(stageObject);

        }
        //ステージ保持上限内になるまで古いステージを削除
        while (generatedStageList.Count > preInstantiaste + 2) DestroyOldestStage();

        currentChipIndex = toChipIndex;

    }

    //していのインデックス位置にStageオブジェクトをランダムに生成
    GameObject GenerateStage(int chipIndex)
    {
        int nextStageChip = Random.Range(0, stageChips.Length);

        GameObject stageObject = (GameObject)Instantiate(
            stageChips[nextStageChip],
            new Vector3(0, 0, chipIndex * StageChipSize),
            Quaternion.identity
            );

        return stageObject;
    }

    //一番古いステージを削除
    void DestroyOldestStage()
    {
        GameObject oldStage = generatedStageList[0];
        generatedStageList.RemoveAt(0);
        Destroy(oldStage);
    }

}
