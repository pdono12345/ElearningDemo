namespace ElearningDemoRepositories.Test.UnitTest;

public class ManagerRepositoryTests
{
    [Test]
    public async Task CreateManagerAsync_ManagerIsAdded()
    {
        // Arrange
        using var context = new ElearningDemoContext(Utilities.TestDbContextOptions());
        var fakeRepository = await CreateRepositoryAsync(context);
        var recordId = 123;
        var expectedManager = new Manager
        {
            Id = recordId,
            Name = "unitTest",
            Username = "unitTest",
            Password = "unitTest",
            CreatedAt = DateTime.Now,
            IsValid = true,
            IsDeleted = false,
        };

        // Act
        fakeRepository.Create(expectedManager);
        await context.SaveChangesAsync();

        // Assert
        var actualManager = context.Managers.Single(m => m.Id == recordId);
        Assert.That(actualManager, Is.EqualTo(expectedManager));
    }

    [Test]
    public async Task UpdateManagerAsync_ManagerIsUpdated()
    {
        // Arrange
        using var context = new ElearningDemoContext(Utilities.TestDbContextOptions());
        var fakeRepository = await CreateRepositoryAsync(context);
        var existedManager = await context.Managers.SingleAsync(m => m.Id == 1);

        // Act
        existedManager.Name = "UpdatedName";
        fakeRepository.Update(existedManager);
        await context.SaveChangesAsync();

        // Assert
        var updatedManager = context.Managers.Single(m => m.Id == 1);
        Assert.That(updatedManager.Name, Is.EqualTo("UpdatedName"));
    }

    [Test]
    public async Task SoftDeleteManager_ManagerIsSoftDeleted()
    {
        // Arrange
        using var context = new ElearningDemoContext(Utilities.TestDbContextOptions());
        var fakeRepository = await CreateRepositoryAsync(context);

        // Act
        fakeRepository.SoftDelete(1);
        await context.SaveChangesAsync();

        // Assert
        var deletedManager = context.Managers.Single(m => m.Id == 1);
        Assert.That(deletedManager.IsDeleted, Is.EqualTo(true));
    }

    [Test]
    public async Task HardDeleteManager_ManagerIsHardDeleted()
    {
        // Arrange
        using var context = new ElearningDemoContext(Utilities.TestDbContextOptions());
        var fakeRepository = await CreateRepositoryAsync(context);
        var recordId = 1;
        var existedManager = await context.Managers.SingleAsync(m => m.Id == recordId);
        var expectedManagers = context.Managers.Where(m => m.Id != recordId).ToList();

        // Act
        fakeRepository.HardDelete(existedManager);
        await context.SaveChangesAsync();

        // Assert
        var actualManagers = context.Managers.ToList();
        Assert.That(actualManagers.OrderBy(m => m.Id), Is.EqualTo(expectedManagers.OrderBy(m => m.Id)));
    }

    [Test]
    public async Task GetManagerByIdAsync_GetCorrectManager()
    {
        // Arrange
        using var context = new ElearningDemoContext(Utilities.TestDbContextOptions());
        var fakeRepository = await CreateRepositoryAsync(context);

        // Act
        var manager = await fakeRepository.GetOneAsync(m => m.Id == 1);

        // Assert
        Assert.That(manager, Is.Not.Null);
        Assert.That(manager.Id, Is.EqualTo(1));
    }

    [Test]
    public async Task GetManagerById_GetCorrectManager()
    {
        // Arrange
        using var context = new ElearningDemoContext(Utilities.TestDbContextOptions());
        var fakeRepository = await CreateRepositoryAsync(context);

        // Act
        var manager = fakeRepository.GetOne(m => m.Id == 1);

        // Assert
        Assert.That(manager, Is.Not.Null);
        Assert.That(manager.Id, Is.EqualTo(1));
    }

    [Test]
    public async Task GetAllManagersAsync_GetAllManagers()
    {
        // Arrange
        using var context = new ElearningDemoContext(Utilities.TestDbContextOptions());
        var fakeRepository = await CreateRepositoryAsync(context);

        // Act
        var managers = await fakeRepository.GetAllAsync();

        // Assert
        Assert.That(managers, Is.Not.Null);
        Assert.That(managers.Count(), Is.EqualTo(3));
    }

    [Test]
    public async Task GetAllManagers_GetAllManagers()
    {
        // Arrange
        using var context = new ElearningDemoContext(Utilities.TestDbContextOptions());
        var fakeRepository = await CreateRepositoryAsync(context);

        // Act
        var managers = fakeRepository.GetAll();

        // Assert
        Assert.That(managers, Is.Not.Null);
        Assert.That(managers.Count(), Is.EqualTo(3));   
    }

    [Test]
    public async Task GetAllValidManagersAsync_GetAllValidManagers()
    {
        // Arrange
        using var context = new ElearningDemoContext(Utilities.TestDbContextOptions());
        var fakeRepository = await CreateRepositoryAsync(context);

        // Act
        var managers = await fakeRepository.GetAllValidAsync();

        // Assert
        Assert.That(managers, Is.Not.Null);
        Assert.That(managers.Count(), Is.EqualTo(1));
        Assert.That(managers.Any(m => m.IsValid == false), Is.False);
        Assert.That(managers.Any(m => m.IsDeleted == true), Is.False);
    }

    [Test]
    public async Task GetAllAliveManagersAsync_GetAllAliveManagers()
    {
        // Arrange
        using var context = new ElearningDemoContext(Utilities.TestDbContextOptions());
        var fakeRepository = await CreateRepositoryAsync(context);

        // Act
        var managers = await fakeRepository.GetAllAliveAsync();

        // Assert
        Assert.That(managers, Is.Not.Null);
        Assert.That(managers.Count(), Is.EqualTo(2));
        Assert.That(managers.Any(m => m.IsDeleted == true), Is.False);
    }

    private async Task<ManagerRepository> CreateRepositoryAsync(ElearningDemoContext context)
    {
        await GetSeedingManagers(context);
        return new ManagerRepository(context);
    }

    private static async Task GetSeedingManagers(ElearningDemoContext context)
    {
        var managers = new List<Manager>()
        {
            new Manager {Name = "Developer_1", Username = "admin_1", Password = "0000", CreatedAt = DateTime.Now, IsValid = true, IsDeleted = false },
            new Manager {Name = "Developer_2", Username = "admin_2", Password = "0000", CreatedAt = DateTime.Now, IsValid = false, IsDeleted = false },
            new Manager {Name = "Developer_3", Username = "admin_3", Password = "0000", CreatedAt = DateTime.Now, IsValid = true, IsDeleted = true }
        };
        context.Managers.AddRange(managers);
        await context.SaveChangesAsync();
    }
}

