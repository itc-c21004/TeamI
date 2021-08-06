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

    //�w���Index�܂ł̃X�e�[�W�`�b�v�𐶐����ĊǗ����ɒu��
    void UpdateStage(int toChipIndex)
    {
        if (toChipIndex <= currentChipIndex) return;
        //�w��̃X�e�[�W�`�b�v�܂ł𐶐�       
        for (int i = currentChipIndex + 1; i <= toChipIndex; i++)
        {
            GameObject stageObject = GenerateStage(i);
            //���������I�u�W�F�N�g���Ǘ����X�g�ɒǉ�
            generatedStageList.Add(stageObject);

        }
        //�X�e�[�W�ێ�������ɂȂ�܂ŌÂ��X�e�[�W���폜
        while (generatedStageList.Count > preInstantiaste + 2) DestroyOldestStage();

        currentChipIndex = toChipIndex;

    }

    //���Ă��̃C���f�b�N�X�ʒu��Stage�I�u�W�F�N�g�������_���ɐ���
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

    //��ԌÂ��X�e�[�W���폜
    void DestroyOldestStage()
    {
        GameObject oldStage = generatedStageList[0];
        generatedStageList.RemoveAt(0);
        Destroy(oldStage);
    }

}
