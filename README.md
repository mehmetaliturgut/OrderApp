# OrderApp

Bu proje, belirli aralıklarla (örneğin her 5 dakikada bir) **REST API üzerinden sipariş listesi çeken bir .NET konsol uygulamasıdır**. Uygulama, **API token yönetimini** otomatik olarak gerçekleştirir ve token süresi dolmadan tekrar istek atmaz.  

## Özellikler

- **Token yönetimi**: Saatlik token isteği sınırı dikkate alınır.  
- **Otomatik token yenileme**: Token süresi dolduğunda veya geçersiz olduğunda otomatik olarak yenilenir.  
- **Periyodik sipariş çekme**: Sipariş listesi her 5 dakikada bir güncellenir.  
- **Hata yönetimi**: Token hataları ve API hataları yakalanır, gerekli durumlarda yeniden deneme yapılır.  

## Kullanım

### 1. Token Alma
- Uygulama ilk çalıştığında API’den token alır.  
- Token, süresi boyunca bellekte saklanır ve tekrar kullanılabilir.  
- Eğer token süresi dolarsa veya geçersiz hale gelirse, otomatik olarak yenilenir.  

### 2. Sipariş Çekme
- Uygulama belirlenen periyotlarda API’ye istek atar.  
- İsteklerde mevcut token kullanılır.  
- API’den gelen siparişler işlenir veya saklanabilir (dosya, veritabanı vb.).  

### 3. Hata ve Retry Mekanizması
- Token hatası (örn. 401 Unauthorized) alındığında token yenilenir ve istek tekrar yapılır.  
- API isteği sırasında başka hata alınırsa loglanır ve bir sonraki periyotta tekrar denenir.  

## Örnek Kod Akışı

```csharp
using System;
using System.Threading;

class Program
{
    static string Token;
    static DateTime TokenExpiry;

    static void Main(string[] args)
    {
        while (true)
        {
            EnsureToken(); // Token geçerliliğini kontrol et
            GetOrders();   // Siparişleri çek
            Thread.Sleep(TimeSpan.FromMinutes(5)); // 5 dakika bekle
        }
    }

    static void EnsureToken()
    {
        if (Token == null || DateTime.Now >= TokenExpiry)
        {
            Token = GetNewToken();
            TokenExpiry = DateTime.Now.AddHours(1); // Örnek token süresi 1 saat
        }
    }

    static string GetNewToken()
    {
        // API'den token al
        return "yeni-token";
    }

    static void GetOrders()
    {
        // Token ile siparişleri çek
        Console.WriteLine("Siparişler çekildi.");
    }
}
