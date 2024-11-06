using UnityEngine;

public class Bomb_Fire : PoolObje
{
    [SerializeField] private float fireTime = 1;
    private float newFireTime = 1;
    private int power;
    private Transform boardFireParent;

    private void Start()
    {
        boardFireParent = Utils.MakeChieldForGameElement("Board_Fire");
        transform.SetParent(boardFireParent);
    }
    public void SetFire(int power)
    {
        this.power = power;
        newFireTime = fireTime;
        transform.SetParent(boardFireParent);
    }
    private void Update()
    {
        newFireTime -= Time.deltaTime;
        if (newFireTime < 0)
        {
            EnterHavuz();
            newFireTime = fireTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamegable IDamegable))
        {
            IDamegable.TakeDamage(power);
        }
        else if (Map_Holder.Instance.GameBoard[Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z)].board_Game.boardType == BoardType.Wall)
        {
            int posX = Mathf.RoundToInt(transform.position.x);
            int posY = Mathf.RoundToInt(transform.position.z);

            Map_Holder.Instance.GameBoard[posX, posY].board_Object.GetComponent<PoolObje>().EnterHavuz();
            Map_Holder.Instance.GameBoard[posX, posY] = new GameBoard();
        }
    }
}