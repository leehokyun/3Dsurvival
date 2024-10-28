using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _instance;
    public static CharacterManager Instance //외부에서 접근할 수 있게하는 변수
    {
        get //반환해주는 Getter함수를 써서 소문자 _instance를 반환해준다.
        {
            if(_instance == null) //비어있을 경우의 방어코드
            {
                _instance = new GameObject("CharacterManager").AddComponent<CharacterManager>();
            }
            return _instance;
        }
    }

    public Player _player;
    public Player Player
    {
        get { return _player; }
        set { _player = value; }
    }
    private void Awake()
    {
        if (_instance != null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); //씬을 이동하더라도 이 정보가 사라지지 않도록함
        }
        else
        {
            if (_instance == this) //인스턴스에 있는 것과 내 자신이 다르다면, 현재 것을 파괴해라.
            {
                Destroy(this);
            }
        }
    }
}
