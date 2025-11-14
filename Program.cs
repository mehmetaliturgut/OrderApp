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
    }
}
