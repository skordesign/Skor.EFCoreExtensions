Open project to see example using Skor.EFCoreExtensions :)
I write it because I got some error when update database with tracking of EFCore, my extension will make it easier

# USING
## Register Service

```csharp
 public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer().AddDbContext<ExampleDbContext>(options => {
                options.UseSqlServer(connectionString);
            }, ServiceLifetime.Transient);
            services.AddTransientRepository<Author, ExampleDbContext>();
            services.AddTransientRepository<Book, ExampleDbContext>();
        }
```

## Inject

```csharp
private readonly IBaseRepository<Author> _authorRepository;
public ExampleController(IBaseRepository<Author> authorRepository)
{
      _authorRepository = authorRepository;
}
```

## Get data

```csharp
  var authors = await _authorRepository.GetAllAsync();
  authors = await _authorRepository.GetAllAsync(s=>s.Name.Contains("A"));
  authors = _authorRepository.GetAll(s=>s.Name=="Author"); // without async/await
  var addedAuthor = await _authorRepository.AddAsync(new Author{Name="ExampleAuthor"});
  Console.WriteLine(addedAuthor.Name);// Result: ExampleAuthor
```
