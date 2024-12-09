using DG.Tweening;
using UnityEngine;

public class Bomb_Nukleer : Bomb_Base
{
    private Transform view;
    private ParticleSystem bombTrail;


    private void Awake()
    {
        view = transform.Find("BombView");
        bombTrail = view.Find("Bomb_Nucleer_Trail").GetComponent<ParticleSystem>();
    }
    public override void Bombed()
    {
        if (IsExploded)
        {
            return;
        }
        bombTrail.Play();
        SetExploded();
        transform.DOLocalMoveY(10, 1).OnComplete(() =>
        {
            view.eulerAngles = Vector3.right * 180;
            transform.DOLocalMoveY(0, 1).OnComplete(() =>
            {
                CreateFire();
            });
        });
    }
    private void CreateFire()
    {
        bombTrail.Stop();
        BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x, 0, MyCoor.y)).GetComponent<Bomb_Fire>().SetFire(BombPower, BombFireTime);
        for (int x = -BombLimit; x < BombLimit + 1; x++)
        {
            for (int y = -BombLimit; y < BombLimit + 1; y++)
            {
                // X sınırlar içinde mi
                if (MyCoor.x + x < 0 || MyCoor.x + x >= Map_Holder.Instance.GameBoard.GetLength(0))
                {
                    continue;
                }
                // Y sınırlar içinde mi
                if (MyCoor.y + y < 0 || MyCoor.y + y >= Map_Holder.Instance.GameBoard.GetLength(1))
                {
                    continue;
                }
                // Bir obje var mı
                if (Map_Holder.Instance.GameBoard[MyCoor.x + x, MyCoor.y + y].board_Object is null)
                {
                    // Bomb Fire bırak
                    BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x + x, 0, MyCoor.y + y)).GetComponent<Bomb_Fire>().SetFire(BombPower, BombFireTime);
                }
                // Bulunan obje deactif mi
                else if (!Map_Holder.Instance.GameBoard[MyCoor.x + x, MyCoor.y + y].board_Object.activeSelf)
                {
                    // Bomb Fire bırak
                    BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x + x, 0, MyCoor.y + y)).GetComponent<Bomb_Fire>().SetFire(BombPower, BombFireTime);
                }
                // Bulunan obje kutu mu
                else if (Map_Holder.Instance.GameBoard[MyCoor.x + x, MyCoor.y + y].board_Game.boardType == BoardType.NonUseable)
                {
                    // Bomb Fire bırak
                    BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x + x, 0, MyCoor.y + y)).GetComponent<Bomb_Fire>().SetFire(BombPower, BombFireTime);
                }
                // Bulunan obje kutu mu
                else if (Map_Holder.Instance.GameBoard[MyCoor.x + x, MyCoor.y + y].board_Game.boardType == BoardType.Empty)
                {
                    // Bomb Fire bırak
                    BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x + x, 0, MyCoor.y + y)).GetComponent<Bomb_Fire>().SetFire(BombPower, BombFireTime);
                }
                // Bulunan obje kutu mu
                else if (Map_Holder.Instance.GameBoard[MyCoor.x + x, MyCoor.y + y].board_Game.boardType == BoardType.Box)
                {
                    // Bomb Fire bırak
                    BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x + x, 0, MyCoor.y + y)).GetComponent<Bomb_Fire>().SetFire(BombPower, BombFireTime);
                }
                else if (Map_Holder.Instance.GameBoard[MyCoor.x + x, MyCoor.y + y].board_Object.CompareTag("Bomb"))
                {
                    // Bomb patlat
                    Map_Holder.Instance.GameBoard[MyCoor.x + x, MyCoor.y + y].board_Object.GetComponent<Bomb_Base>().Bombed();
                }
            }
        }
        view.eulerAngles = Vector3.zero;
        transform.localPosition = Vector3.zero;
        EnterHavuz();
    }
}