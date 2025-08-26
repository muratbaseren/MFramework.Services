# MFramework.Services

![License](https://img.shields.io/github/license/muratbaseren/MFramework.Services)
![Version](https://img.shields.io/badge/version-v3.8.0-blue)

**MFramework.Services**, .NET tabanlı modern uygulamalar için kapsamlı bir mikroservis altyapı framework'üdür. MongoDB, Entity Framework ve Entity Framework Core desteği sunar ve enterprise düzeyinde yazılım geliştirme için gerekli tüm temel bileşenleri içerir.

## 🎯 Proje Tanımı

Bu framework, mikroservis mimarileri ve modern .NET uygulamaları için tasarlanmış, Repository Pattern, Unit of Work Pattern ve Dependency Injection gibi modern yazılım geliştirme pratiklerini destekleyen kapsamlı bir altyapı çözümüdür.

## ✨ Özellikler

- **🗄️ Çoklu Veritabanı Desteği**: MongoDB, Entity Framework ve Entity Framework Core
- **🏗️ Repository Pattern**: Veri erişim katmanı için standart repository deseni
- **💼 Manager Pattern**: İş mantığı katmanı için manager deseni
- **🔄 Unit of Work Pattern**: Transaction yönetimi ve veri tutarlılığı
- **🗺️ AutoMapper Entegrasyonu**: Nesne eşleme işlemleri
- **⚡ Async/Await Desteği**: Modern asenkron programlama
- **🧩 Generic Base Classes**: Tekrar kullanılabilir temel sınıflar
- **🔧 Extension Methods**: Kullanışlı genişletme metodları
- **📧 Email Servisi**: SMTP tabanlı email gönderimi
- **🌐 Web Common**: Web uygulamaları için ortak bileşenler
- **🧪 Unit Test Desteği**: xUnit ile test altyapısı

## 📦 Paketler

Framework aşağıdaki ana bileşenlerden oluşur:

| Paket | Açıklama | Versiyon |
|-------|----------|----------|
| `MFramework.Services.Common` | Ortak araçlar ve extension'lar | ![NuGet](https://img.shields.io/nuget/v/MFramework.Services.Common) |
| `MFramework.Services.Entities` | Temel entity sınıfları | ![NuGet](https://img.shields.io/nuget/v/MFramework.Services.Entities) |
| `MFramework.Services.DataAccess` | Veri erişim soyutlamaları | ![NuGet](https://img.shields.io/nuget/v/MFramework.Services.DataAccess) |
| `MFramework.Services.DataAccess.Mongo` | MongoDB implementasyonu | ![NuGet](https://img.shields.io/nuget/v/MFramework.Services.DataAccess.Mongo) |
| `MFramework.Services.DataAccess.EntityFramework` | Entity Framework implementasyonu | ![NuGet](https://img.shields.io/nuget/v/MFramework.Services.DataAccess.EntityFramework) |
| `MFramework.Services.DataAccess.EntityFrameworkCore` | Entity Framework Core implementasyonu | ![NuGet](https://img.shields.io/nuget/v/MFramework.Services.DataAccess.EntityFrameworkCore) |
| `MFramework.Services.Business` | İş mantığı soyutlamaları | ![NuGet](https://img.shields.io/nuget/v/MFramework.Services.Business) |
| `MFramework.Services.Business.Mongo` | MongoDB iş mantığı implementasyonu | ![NuGet](https://img.shields.io/nuget/v/MFramework.Services.Business.Mongo) |
| `MFramework.Services.Business.EntityFramework` | Entity Framework iş mantığı implementasyonu | ![NuGet](https://img.shields.io/nuget/v/MFramework.Services.Business.EntityFramework) |
| `MFramework.Services.WebCommon` | Web uygulamaları için ortak bileşenler | ![NuGet](https://img.shields.io/nuget/v/MFramework.Services.WebCommon) |

## 🛠️ Kurulum

### MongoDB için Kurulum

```bash
# Package Manager Console
Install-Package MFramework.Services.Business.Mongo
Install-Package MFramework.Services.DataAccess.Mongo
Install-Package MFramework.Services.Common
Install-Package MFramework.Services.Entities
Install-Package AutoMapper
Install-Package MongoDB.Driver

# .NET CLI
dotnet add package MFramework.Services.Business.Mongo
dotnet add package MFramework.Services.DataAccess.Mongo
dotnet add package MFramework.Services.Common
dotnet add package MFramework.Services.Entities
dotnet add package AutoMapper
dotnet add package MongoDB.Driver
```

### Entity Framework için Kurulum

```bash
# Package Manager Console
Install-Package MFramework.Services.Business.EntityFramework
Install-Package MFramework.Services.DataAccess.EntityFramework
Install-Package MFramework.Services.Common
Install-Package MFramework.Services.Entities
Install-Package AutoMapper
Install-Package EntityFramework

# .NET CLI
dotnet add package MFramework.Services.Business.EntityFramework
dotnet add package MFramework.Services.DataAccess.EntityFramework
dotnet add package MFramework.Services.Common
dotnet add package MFramework.Services.Entities
dotnet add package AutoMapper
dotnet add package EntityFramework
```

### Entity Framework Core için Kurulum

```bash
# Package Manager Console
Install-Package MFramework.Services.Business.EntityFrameworkCore
Install-Package MFramework.Services.DataAccess.EntityFrameworkCore
Install-Package MFramework.Services.Common
Install-Package MFramework.Services.Entities
Install-Package AutoMapper
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.SqlServer

# .NET CLI
dotnet add package MFramework.Services.Business.EntityFrameworkCore
dotnet add package MFramework.Services.DataAccess.EntityFrameworkCore
dotnet add package MFramework.Services.Common
dotnet add package MFramework.Services.Entities
dotnet add package AutoMapper
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

## 💻 Kullanım

### MongoDB ile CRUD İşlemleri

#### 1. Entity Tanımlama

```csharp
using MFramework.Services.Entities.Abstract;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Album : EntityBase<ObjectId>
{
    public string Name { get; set; }

    [BsonRepresentation(BsonType.Int32)]
    public int Year { get; set; }
    
    public bool IsSales { get; set; }

    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; set; }
}
```

#### 2. Context Tanımlama

```csharp
using MFramework.Services.DataAccess.Mongo.Context;

public class MyMongoContext : MongoDBContextBase
{
    public MyMongoContext(IConfiguration configuration) 
        : base(null, configuration.GetConnectionString("MongoDB"), "albumdb")
    {
    }
}
```

#### 3. Repository Tanımlama

```csharp
using MFramework.Services.DataAccess.Mongo.Attributes;
using MFramework.Services.DataAccess.Mongo.Repository.Abstract;

[Collection("albums")]
public class AlbumRepository : MongoRepository<Album, ObjectId>
{
    public AlbumRepository(MyMongoContext context) : base(context)
    {
    }
}
```

#### 4. Manager Tanımlama

```csharp
using MFramework.Services.Business.Mongo;
using AutoMapper;
using System.Linq.Expressions;

public class AlbumManager : MongoManager<Album, ObjectId, AlbumRepository>
{
    public AlbumManager(AlbumRepository repository, IMapper mapper) 
        : base(repository, mapper)
    {
    }

    public Album FindByName(string name)
    {
        return repository.Find(x => x.Name == name);
    }

    public async Task<IEnumerable<Album>> GetAlbumsByYearAsync(int year)
    {
        return await repository.FindAllAsync(x => x.Year == year);
    }
}
```

### Entity Framework Core ile CRUD İşlemleri

#### 1. Entity Tanımlama

```csharp
using MFramework.Services.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

public class Book : EntityBase<int>
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Author { get; set; }
    
    public DateTime PublishedDate { get; set; }
    
    public decimal Price { get; set; }
}
```

#### 2. DbContext Tanımlama

```csharp
using Microsoft.EntityFrameworkCore;

public class BookContext : DbContext
{
    public BookContext(DbContextOptions<BookContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Author).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
        });
    }
}
```

#### 3. Repository Tanımlama

```csharp
using MFramework.Services.DataAccess.EntityFrameworkCore;

public class BookRepository : EFRepository<Book, int, BookContext>
{
    public BookRepository(BookContext context) : base(context)
    {
    }
}
```

#### 4. Manager Tanımlama

```csharp
using MFramework.Services.Business.EntityFramework;
using AutoMapper;

public class BookManager : EFManager<Book, int, BookRepository>
{
    public BookManager(BookRepository repository, IMapper mapper) 
        : base(repository, mapper)
    {
    }

    public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(string author)
    {
        return await repository.FindAllAsync(x => x.Author == author);
    }
}
```

### Unit of Work Pattern Kullanımı

```csharp
using MFramework.Services.DataAccess.UnitOfWork;
using MFramework.Services.DataAccess.EntityFrameworkCore;

public interface IMyUnitOfWork : IUnitOfWork
{
    IRepository<Book, int> BookRepository { get; }
    IRepository<Album, ObjectId> AlbumRepository { get; }
}

public class MyUnitOfWork : EFUnitOfWork<BookContext>, IMyUnitOfWork
{
    public MyUnitOfWork(BookContext context) : base(context)
    {
    }

    private IRepository<Book, int> _bookRepository;
    public IRepository<Book, int> BookRepository
    {
        get
        {
            return _bookRepository ??= new BookRepository(_context);
        }
    }
}
```

### Web API Controller Örneği

```csharp
[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BookManager _bookManager;
    private readonly IMyUnitOfWork _unitOfWork;

    public BooksController(BookManager bookManager, IMyUnitOfWork unitOfWork)
    {
        _bookManager = bookManager;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        var books = await _bookManager.GetAllAsync();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        var book = await _bookManager.GetByIdAsync(id);
        if (book == null)
            return NotFound();
        
        return Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult<Book>> CreateBook(Book book)
    {
        var createdBook = await _bookManager.CreateAsync(book);
        await _unitOfWork.CommitAsync();
        
        return CreatedAtAction(nameof(GetBook), new { id = createdBook.Id }, createdBook);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, Book book)
    {
        if (id != book.Id)
            return BadRequest();

        await _bookManager.UpdateAsync(book);
        await _unitOfWork.CommitAsync();
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        await _bookManager.DeleteAsync(id);
        await _unitOfWork.CommitAsync();
        
        return NoContent();
    }
}
```

## ⚙️ AppSettings.json Konfigürasyonu

### MongoDB için

```json
{
  "ConnectionStrings": {
    "MongoDB": "mongodb://localhost:27017"
  },
  "EmailSettings": {
    "MailHost": "smtp.gmail.com",
    "MailPort": 587,
    "MailUsername": "your-email@gmail.com",
    "MailPassword": "your-app-password",
    "MailDisplayName": "My Application",
    "MailEnableSsl": true,
    "MailIsBodyHtml": true,
    "MailUseDefaultCredentials": false
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### SQL Server (Entity Framework Core) için

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MyAppDb;Trusted_Connection=true;MultipleActiveResultSets=true",
    "MongoDB": "mongodb://localhost:27017"
  },
  "EmailSettings": {
    "MailHost": "smtp.gmail.com",
    "MailPort": 587,
    "MailUsername": "your-email@gmail.com",
    "MailPassword": "your-app-password",
    "MailDisplayName": "My Application",
    "MailEnableSsl": true,
    "MailIsBodyHtml": true,
    "MailUseDefaultCredentials": false
  },
  "FileSettings": {
    "UploadPath": "uploads",
    "MaxFileSize": 10485760,
    "AllowedExtensions": [".jpg", ".jpeg", ".png", ".pdf", ".docx"]
  }
}
```

## 🔧 Dependency Injection Ayarları

### Program.cs (.NET 6+)

```csharp
using MFramework.Services.Common;
using MFramework.Services.WebCommon;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// DbContext konfigürasyonu (Entity Framework Core)
builder.Services.AddDbContext<BookContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// MongoDB Context konfigürasyonu
builder.Services.AddScoped<MyMongoContext>();

// AutoMapper konfigürasyonu
builder.Services.AddAutoMapper(typeof(Program));

// Repository'ler
builder.Services.AddScoped<BookRepository>();
builder.Services.AddScoped<AlbumRepository>();

// Manager'lar
builder.Services.AddScoped<BookManager>();
builder.Services.AddScoped<AlbumManager>();

// Unit of Work
builder.Services.AddScoped<IMyUnitOfWork, MyUnitOfWork>();

// Email servisi
builder.Services.Configure<EMailSenderSettings>(
    builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IEMailSender, EMailSender>();

// File manager
builder.Services.AddScoped<IFileFolderManager, FileFolderManager>();

// Web API servisleri
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Pipeline konfigürasyonu
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

### AutoMapper Profil Örneği

```csharp
using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Book mappings
        CreateMap<Book, BookDto>().ReverseMap();
        CreateMap<Book, CreateBookDto>().ReverseMap();
        CreateMap<Book, UpdateBookDto>().ReverseMap();
        
        // Album mappings
        CreateMap<Album, AlbumDto>().ReverseMap();
        CreateMap<Album, CreateAlbumDto>().ReverseMap();
        CreateMap<Album, UpdateAlbumDto>().ReverseMap();
    }
}
```

## 📋 Gereksinimler

- **.NET Standard 2.1** veya üzeri
- **MongoDB 4.0+** (MongoDB kullanımı için)
- **SQL Server 2016+** (Entity Framework kullanımı için)
- **Entity Framework Core 7.0+** (EF Core kullanımı için)
- **AutoMapper 12.0+**
- **C# 8.0+** özellikleri

### Desteklenen .NET Versiyonları

- .NET Core 3.1+
- .NET 5.0+
- .NET 6.0+
- .NET 7.0+
- .NET 8.0+

## 📄 Lisans

Bu proje **Apache License 2.0** altında lisanslanmıştır. Detaylar için [LICENSE](https://github.com/muratbaseren/MFramework.Services/blob/master/LICENSE) dosyasına bakınız.

## 🤝 Katkıda Bulunma

Katkılarınızı memnuniyetle karşılıyoruz! Katkıda bulunmak için:

1. Bu repository'yi fork edin
2. Feature branch'i oluşturun (`git checkout -b feature/amazing-feature`)
3. Değişikliklerinizi commit edin (`git commit -m 'feat: Add amazing feature'`)
4. Branch'inizi push edin (`git push origin feature/amazing-feature`)
5. Pull Request oluşturun

### Katkı Kuralları

- Kod standartlarına uyun
- Unit testler yazın
- Commit mesajlarında [Conventional Commits](https://www.conventionalcommits.org/) formatını kullanın
- Değişikliklerinizi dokümante edin

## 📞 İletişim

- **GitHub**: [@muratbaseren](https://github.com/muratbaseren)
- **Proje**: [MFramework.Services](https://github.com/muratbaseren/MFramework.Services)
- **Issues**: [GitHub Issues](https://github.com/muratbaseren/MFramework.Services/issues)

## 🎯 Yol Haritası

- [ ] .NET 8 minimal API desteği
- [ ] GraphQL entegrasyonu
- [ ] Redis cache desteği
- [ ] Message queue entegrasyonu
- [ ] Docker container desteği

---

**⭐ Bu projeyi beğendiyseniz yıldız vermeyi unutmayın!**
