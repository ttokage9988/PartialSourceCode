using System.Collections.Generic;
using UnityEngine;

public class TaskPopup : MonoBehaviour
{
    [SerializeField] GameObject taskCube;

    [SerializeField] GameObject charactersParent;

    List<GameObject> characterList = new List<GameObject>();

    void Start()
    {
        //キャッシュ
        CacheCharacters();

        //8キャラにランダム生成
        Create(8);
    }

    //ランダムにポップアップを作成
    private void Create(int count)
    {
        var list = new List<GameObject>(characterList); //抽選用リスト作成

        //抽選
        for (int i = 0; i < count; i++)
        {
            int num = Random.Range(0, list.Count);

            Transform character = list[num].transform;

            Instantiate(original: taskCube,
                position: new Vector3(character.position.x, character.position.y + 4, character.position.z),
                rotation: Quaternion.identity,
                parent: list[num].transform);

            //抽選リストから除外
            list.RemoveAt(num);
        }
    }

    private void CacheCharacters()
    {
        foreach (Transform child in charactersParent.transform)
        {
            characterList.Add(child.gameObject);
        }
    }

}
