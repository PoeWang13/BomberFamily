﻿using UnityEngine;

public class Fikirler : MonoBehaviour
{
    /// +Günlük giriş
    /// +Kaçırılan günlük giriş
    /// +Levele başlama desteği
    /// +2 Kat ödül
    /// Özel level oynama
    /// Özel giysiler
    /// Özel büyü
    /// Levellerden bazen vakitle açılan sandık düşer, bu sandıkları upgrade etmek isteriz.

    /// Craft System
    /// Her bomba 5 çeşit alet kullanılarak yapılır. Bu aletlerde hammadde kullanılarak yapılır.
    /// Her alet için sabit 3 eşya gerekli
    /// - Çerçeve : Demir + Plastik + Tahta
    /// - Barut : Güherçile + Kükürt + Kömür tozu
    /// - Fitil : Pamuk + Makara + Parmağa takılan yüzük
    /// 1-Clock Bomb
    /// - Uzaktan kumanda : Elektronik kart + Plastik kap + pil
    /// - Alıcı : Alıcı radar iğnesi + Bobin + 
    /// 2-Area Bomb
    /// - Sıvı solüsyon : Sıvı X + Sıvı Y + Sıvı Z
    /// - Cam beher : Kum + Ocak + Beher için kalıp
    /// 3-Anti Bomb
    /// - Bilye : Demir + Çelik + Bakır
    /// - Muşamba : Petrol + Damıtma aleti + Betondan kalıp -> Bilyeleri sarmak için 
    /// 4-Nucleer Bomb
    /// - Atom kabı : Uranyum + Özel alışım kap + Kurşun
    /// - Basınçla açılan kap : Dişli + Demir + Barometre
    /// 5-Searcher
    /// - Radar : Cam ekran + Lamba + Hassasiyet ölçer
    /// - Batarya : Lityum + Su + Bakır
    /// 6-Elektro -> Öldürmez ama düşmanları felç eder
    /// - Cam küre : Kum + Plastik + Tahta
    /// - Elektrik kablosu : Plastik + Bakır + Beton kalıp
    /// 7-Lav Bombası -> 10 birim ilerleyene kadar tüm boş yollarda ilerler, kutu veya düşman bulursa onu ortadan kaldırır ve biter.
    /// - 1 toprak delici : Matkap + Kol + Gözlük
    /// - 1 lav küresi : Lav cam küresi + akışkan lav + Taş Z
    /// 8-Buz Bombası -> 10 birim ilerleyene kadar tüm boş yollarda ilerler, düşman bulursa onu dondurur ve biter.
    ///                 -> Donma süresi 30 saniyedir, birisi deyerse yok olurlar.
    /// - 1 Torba X : Buz taşı + Taş I + Taş B
    /// - 1 buz küresi : Buz cam küresi + Sıvı L + Taş M
    /// 9-Sis Bombası -> 20 birim çevreyi görmeyi engelleyen bir sis oluşturur.
    /// - 1 hava küresi : Sis cam küresi + hava pompası + hava vanası
    /// - 1 toz küresi : Taş K + Taş L + Taş M
    /// 10-Zehir bombası -> 20 birim çevredeki tüm canlıları zehirler.
    /// - 1 zehir tozu : Kırma kovası + Kırma taşı + Zehir taşı
    /// - 1 zehir küresi : Sıvı Z + Zehir cam küresi + Zehir kumaşı
    ///

    //public List<Transform> transforms = new List<Transform>();
    //[ContextMenu("Deneme")]
    //public void Deneme()
    //{
    //    new GameObject("Deneme").transform.SetParent(transforms[0]);

    //    //for (int i = 0; i < transforms.Count; i++)
    //    //{
    //    //    for (int y = transforms[i].childCount - 1; y >= 0; y--)
    //    //    {
    //    //        if (!transforms[i].GetChild(y).gameObject.activeSelf)
    //    //        {
    //    //            transforms[i].GetChild(y).SetParent(transform);
    //    //        }
    //    //    }
    //    //}
    //}
    //DOTween.To(value => { }, startValue: 0, endValue: 1, duration: 0.25f).SetEase(Ease.Linear).
    //        OnComplete(() =>
    //        {
    //        });

    /// -Anahtarları düşmanlarada verebiliriz
    /// -Üstünde durunca tetiklenen çıkınca bırakılan tuzaklar yapabiliriz.
    /// -üstünden geçemediğimiz blocklar olabilir
    /// -Ateşle tetiklenen bloklar olabilir.
    /// -Bütün haritayı etkileyen tuzaklar olabilir.
    /// -Üstündekileri boşluk veya üstünden geçilebilecek alan varsa gösterdiği istikamete gönderen tuzaklar
    /// 
    /// 
    ///


    /* Düşanlar yön bulma türleri
     * 1+Rastgele dolaşan
     * 2+Playera yaklaşan
     * 3+Playerı tuzağa düşüren
     * 4+Rastgele ışınlanan
     * 5+Playerı görünce korkup kaçan
     * 6+Yolun sonuna kadar giden
     * 7-Kutuların içinden geçip giden
     * 8-Duvarların içinden geçip giden
     * 9-Şekil değiştiren
     * 10-Darbe alınca kendi türündekileri çağıran
     * 11-Darbe alınca kendi türündekileri çağıran
     * 12-Yapışkan düşman : Bize yapışır ve click ile üstümüzden atana kadar bizi yavaşlatır.
     */

    /* Düşanlar Hareket Çeşitleri
     * 1-Yürüyen
     * 2-Uçan
     * 3-Zıplayan
     * 
     */

    /* Tuzaklar
     * Diken -> Zamanlı - Triggerli
     * Yavaşlatan -> İçinde durdukça - Zamanla geçen
     * Zehirleyen -> İçinde durdukça - Zamanla geçen
     * Lazer -> Zamanla hepsi - Zamanla diğer taraftaki lazere geçen - Trigger ile hepsi - Trigger ile diğer taraftaki lazere geçen
     * Ateşli -> Zamanla hepsi - Zamanla diğer taraftaki ateşe geçen - Trigger ile hepsi - Trigger ile diğer taraftaki ateşe geçen
     * Kırbaç -> Zamanla hepsi - Zamanla diğer taraftaki ateşe geçen - Trigger ile hepsi - Trigger ile diğer taraftaki ateşe geçen
     * Testere -> Zamanla hepsi - Zamanla diğer taraftaki ateşe geçen - Trigger ile hepsi - Trigger ile diğer taraftaki ateşe geçen
     * Dönen Testere -> Zamanlı - Triggerli
     * Teleporter -> Tuzağa geleni rastgele bir yere ışınlayacak ve belli bir süre kapanıp görünmez olacak ama başka yere gidecek.
     */

    /* Boardlar
     * Kapı
     * Çoğalan Box
     * Arada bir açılıp etkisiz olan ve sonra kapanıp etkili olan
     * Ok atan duvar
     * Ateş atan duvar
     * Lazer atan duvar
     * Yakalayıp öldüren
     * Yakalayıp tutan ve bırakan
     */

    /* Karakterler
     * Hızlanan
     * Daha çok vuran
     * Daha çok bomba atan
     * Zamanlı bomba alan
     * Fırlatılan bomba alan
     * 
     * Player_Simple        : Normal başlangıç karakteri
     * Player_Natural       : Daha çok can toplayabilen
     * Player_Stronger      : Daha çok vuran
     * Player_Bomber        : Daha çok bomba atan
     * Player_Flash         : Daha çok hızlanabilen
     * Player_Thrower       : Fırlatılan bomba atabilen
     * Player_Ghost         : 1 Kutunun içinden geçebilen
     * Player_Broker        : 1 Kutunun ötesinede ateş bırakabilen
     * Player_Arsonist      : Daha çok ateş bırabilen
     * Player_Timer         : Sadece clock bombası bırakan
     * Player_Radioactiv    : Sadece nucleer bombası bırakan
     * Player_Area          : Sadece area bombası bırakan
     * Player_Anti_Wall     : Sadece anti wall bombası bırakan
     * Player_Searcher      : Sadece searcher bombası bırakan
     * 
     */

    /* Mini oyunlar
     * Gelen düşmana kendi türlerinden bomba atarak düşmanların çizgiyi geçmesini engellemek
     * Belli bir alan içinde sürekli gelen düşmana bomba bırakarak öldürmek
     * Bombalarla dolu bir alanda ilerlemek
     * Takip eden bombadan kaçmak
     * 
     */
}