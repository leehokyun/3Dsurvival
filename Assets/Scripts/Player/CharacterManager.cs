using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _instance;
    public static CharacterManager Instance //�ܺο��� ������ �� �ְ��ϴ� ����
    {
        get //��ȯ���ִ� Getter�Լ��� �Ἥ �ҹ��� _instance�� ��ȯ���ش�.
        {
            if(_instance == null) //������� ����� ����ڵ�
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
            DontDestroyOnLoad(gameObject); //���� �̵��ϴ��� �� ������ ������� �ʵ�����
        }
        else
        {
            if (_instance == this) //�ν��Ͻ��� �ִ� �Ͱ� �� �ڽ��� �ٸ��ٸ�, ���� ���� �ı��ض�.
            {
                Destroy(this);
            }
        }
    }
}
