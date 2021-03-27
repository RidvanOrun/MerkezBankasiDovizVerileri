# Merkez Bankasi Döviz Verileri
Bu projede; Türkiye Cumhuriyeti Merkez Bankası tarafından açıklanan döviz kurlarını, XML ile çekerek .NetCore/MVC ile geliştirdiğim sayfamda gösterdim ve eş zamanı olarak Db'ye kaydettim. Code First yönetmini kullandım. Basit olarak katmanlı mimarı mantığı kullandım. Ayrıca site çalıştırıldığında 10 snde bir verileri otomatik kaydetmesini sağladım.

1. "MerkezBankasiDovizVerileri" adında Blank Solution Açılır.
2. "MerkezBankasiDovizVerileri.Data" adında Library Class (.Net Core) Açılır.

    NOT: Aşağıdaki paketler yüklenir.
    
         1. Microsoft.EntityFrameworkCore(5.0.4)
         
         2. Microsoft.EntityFrameworkCore.SqlServer(5.0.4)
         
         3. Microsoft.EntityFrameworkCore.Tools(5.0.4)
         
    2.1. Entity klasörü açılır.
    
      2.1.1. Entities.cs classı açılır. CurrencyDate Merkez bankası verilerinde sunulan tarih, CreateDate ise Db'ye kaydettiğim tarih olarak tutulmuştur.
      
      public class Entities
      {
      
        [Key]
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int Id { get; set; }
        
        public string USD { get; set; }
        
        public string AUD { get; set; }
        
        public string EUR { get; set; }
        
        public string GBP { get; set; }
        
        public string CurrencyDate { get; set; }
        
        private DateTime _createDate=DateTime.Now;
        public DateTime CreateDate { get => _createDate; set => value = _createDate; }
      }
    
   2.2. Context Klasörü açılır.
   
      2.2.1. ApplicationDbContext.cs classı açılır.
      
       public class ApplicationDbContext: DbContext
        {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        public DbSet<Entities> Entities { get; set; }
        }
3. "MerkezBankasiDovizVerileri.UI" adında ASP.NetCore Wep App(Model-View-Controller) açılır.

    NOT: "MerkezBankasiDovizVerileri.Data" katmanı referans olarak eklenir.
    
         Aşağıdaki paketler yüklenir.
         
         1. Microsoft.EntityFrameworkCore(5.0.4)
         
         2. Microsoft.EntityFrameworkCore.SqlServer(5.0.4)
         
         3. Microsoft.EntityFrameworkCore.Desing(5.0.4)
         
         4. Microsoft.VisualStudio.Web.CodeGeneration.Design(3.1.5)
         
   3.1. Verilerin çakilmesi ve database'e kaydedilmesi için Home Controller içerisine aşağıdaki kodlar yazılır. 
   
        public IActionResult Index(Entities entities)
        {
            var currencyInf = "http://www.tcmb.gov.tr/kurlar/today.xml";
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(currencyInf);
            entities.CurrencyDate = Convert.ToString(xmlDoc.SelectSingleNode("//Tarih_Date").Attributes["Tarih"].Value);
            entities.USD = xmlDoc.SelectSingleNode("Tarih_Date/Currency [@CrossOrder='0']/BanknoteSelling").InnerText;
            entities.AUD = xmlDoc.SelectSingleNode("Tarih_Date/Currency [@CrossOrder='1']/BanknoteSelling").InnerText;
            entities.EUR = xmlDoc.SelectSingleNode("Tarih_Date/Currency [@CrossOrder='9']/BanknoteSelling").InnerText;
            entities.GBP = xmlDoc.SelectSingleNode("Tarih_Date/Currency [@CrossOrder='10']/BanknoteSelling").InnerText;
            _db.Add(entities);
            _db.SaveChanges();

            return View(entities);
        }
        
    3.2. Verilerin Ön yüzde gösterilmesi için View/Home Klasörü içerisindeki Index.cshtml sayfası düzenlenir.
    
    3.3. Db bağlantısnın gerçekleşmesi için appsettings.json içerisi düzenlenir.
    
    3.4. Migration yapılır.
         
  
    
 
      
