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
        BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x, 0, MyCoor.y)).GetComponent<Bomb_Fire>().SetFire(MyOwner.CharacterStat.myBombPower);
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
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
                    BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x + x, 0, MyCoor.y + y)).GetComponent<Bomb_Fire>().SetFire(MyOwner.CharacterStat.myBombPower);
                }
                // Bulunan obje deactif mi
                else if (!Map_Holder.Instance.GameBoard[MyCoor.x + x, MyCoor.y + y].board_Object.activeSelf)
                {
                    // Bomb Fire bırak
                    BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x + x, 0, MyCoor.y + y)).GetComponent<Bomb_Fire>().SetFire(MyOwner.CharacterStat.myBombPower);
                }
                // Bulunan obje kutu mu
                else if (Map_Holder.Instance.GameBoard[MyCoor.x + x, MyCoor.y + y].board_Game.boardType == BoardType.Box)
                {
                    // Bomb Fire bırak
                    BombFirePool.HavuzdanObjeIste(new Vector3Int(MyCoor.x + x, 0, MyCoor.y + y)).GetComponent<Bomb_Fire>().SetFire(MyOwner.CharacterStat.myBombPower);
                }
                else if (Map_Holder.Instance.GameBoard[MyCoor.x + x, MyCoor.y + y].board_Object.TryGetComponent(out Bomb_Base bomb_Base))
                {
                    // Bomb patlat
                    bomb_Base.Bombed();
                }
            }
        }
        view.eulerAngles = Vector3.zero;
        transform.localPosition = Vector3.zero;
        EnterHavuz();
    }
}