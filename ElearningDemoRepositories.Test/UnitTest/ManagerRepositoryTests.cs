namespace ElearningDemoRepositories.Test.UnitTest;

public class ManagerRepositoryTests
{
    [Test]
    public async Task CreateManagerAsync_CreateNewManager_ManagerIsAdded()
    {
        // Arrange
        using var context = new ElearningDemoContext(Utilities.TestDbContextOptions());
        var fakeRepository = await CreateRepositoryAsync(context);
        var expectedManager = new Manager
        {
            Name = "unitTest",
            Username = "unitTest",
            Password = "unitTest",
            CreatedAt = DateTime.Now,
            IsValid = true,
            IsDeleted = false,
        };
        var newManagerId = 4;   // because there are 3 seeding managers which Ids are 1, 2, 3

        // Act
        fakeRepository.Create(expectedManager);
        await context.SaveChangesAsync();

        // Assert
        var actualManager = context.Managers.Single(m => m.Id == newManagerId);
        Assert.That(actualManager, Is.EqualTo(expectedManager));
    }

    [Test]
    public async Task UpdateManagerAsync_UpdateManager_ManagerIsUpdated()
    {
        // Arrange
        using var context = new ElearningDemoContext(Utilities.TestDbContextOptions());
        var fakeRepository = await CreateRepositoryAsync(context);
        var targetManagerId = 1;
        var existedManager = await context.Managers.SingleAsync(m => m.Id == targetManagerId);

        // Act
        existedManager.Name = "UpdatedName";
        fakeRepository.Update(existedManager);
        await context.SaveChangesAsync();

        // Assert
        var updatedManager = context.Managers.Single(m => m.Id == targetManagerId);
        Assert.That(updatedManager.Name, Is.EqualTo("UpdatedName"));
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    public async Task SoftDeleteManager_InputManagerId_ManagerIsSoftDeleted(int targetManagerId)
    {
        // Arrange
        using var context = new ElearningDemoContext(Utilities.TestDbContextOptions());
        var fakeRepository = await CreateRepositoryAsync(context);

        // Act
        fakeRepository.SoftDelete(targetManagerId);
        await context.SaveChangesAsync();

        // Assert
        var deletedManager = context.Managers.Single(m => m.Id == targetManagerId);
        Assert.That(deletedManager.IsDeleted, Is.EqualTo(true));
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    public async Task HardDeleteManager_ManagerIsHardDeleted(int targetManagerId)
    {
        // Arrange
        using var context = new ElearningDemoContext(Utilities.TestDbContextOptions());
        var fakeRepository = await CreateRepositoryAsync(context);
        var existedManager = await context.Managers.SingleAsync(m => m.Id == targetManagerId);
        var expectedManagers = context.Managers.Where(m => m.Id != targetManagerId).ToList();

        // Act
        fakeRepository.HardDelete(existedManager);
        await context.SaveChangesAsync();

        // Assert
        var actualManagers = context.Managers.ToList();
        Assert.That(actualManagers.OrderBy(m => m.Id), Is.EqualTo(expectedManagers.OrderBy(m => m.Id)));
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    public async Task GetManagerByIdAsync_GetCorrectManager(int targetManagerId)
    {
        // Arrange
        using var context = new ElearningDemoContext(Utilities.TestDbContextOptions());
        var fakeRepository = await CreateRepositoryAsync(context);

        // Act
        var manager = await fakeRepository.GetOneAsync(m => m.Id == targetManagerId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(manager, Is.Not.Null);
            Assert.That(manager.Id, Is.EqualTo(targetManagerId));
        });
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    public async Task GetManagerById_GetCorrectManager(int targetManagerId)
    {
        // Arrange
        using var context = new ElearningDemoContext(Utilities.TestDbContextOptions());
        var fakeRepository = await CreateRepositoryAsync(context);

        // Act
        var manager = fakeRepository.GetOne(m => m.Id == targetManagerId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(manager, Is.Not.Null);
            Assert.That(manager.Id, Is.EqualTo(targetManagerId));
        });
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
        Assert.Multiple(() =>
        {
            Assert.That(managers, Is.Not.Null);
            Assert.That(managers.Count(), Is.EqualTo(3));
        });
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
        Assert.Multiple(() =>
        {
            Assert.That(managers, Is.Not.Null);
            Assert.That(managers.Count(), Is.EqualTo(3));
        });
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
        Assert.Multiple(() =>
        {
            Assert.That(managers, Is.Not.Null);
            Assert.That(managers.Count(), Is.EqualTo(1));
            Assert.That(managers.Any(m => m.IsValid == false), Is.False);
            Assert.That(managers.Any(m => m.IsDeleted == true), Is.False);
        });
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
        Assert.Multiple(() =>
        {
            Assert.That(managers, Is.Not.Null);
            Assert.That(managers.Count(), Is.EqualTo(2));
            Assert.That(managers.Any(m => m.IsDeleted == true), Is.False);
        });
    }

    private static async Task<ManagerRepository> CreateRepositoryAsync(ElearningDemoContext context)
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

