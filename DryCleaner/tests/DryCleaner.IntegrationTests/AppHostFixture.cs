using Aspire.Hosting;
using Aspire.Hosting.Testing;

namespace DryCleaner.IntegrationTests;

/// <summary>
/// Фикстура для запуска распределённого приложения при выполнении интеграционных тестов
/// </summary>
public class AppHostFixture : IAsyncLifetime
{
    private IDistributedApplicationTestingBuilder? _appHost;

    /// <summary>
    /// Экземпляр распределённого приложения
    /// </summary>
    public DistributedApplication App { get; private set; } = null!;

    /// <inheritdoc />
    public async ValueTask InitializeAsync()
    {
        using var timeout = new CancellationTokenSource(
            TimeSpan.FromMinutes(3));

        _appHost = await DistributedApplicationTestingBuilder
            .CreateAsync<Projects.DryCleaner_AppHost>(
                [
                    "--IntegrationTesting=true",
                    $"--Parameters:MySqlPassword={Guid.NewGuid():N}"
                ],
                timeout.Token);

        App = await _appHost.BuildAsync(timeout.Token);

        await App.StartAsync(timeout.Token);

        await App.ResourceNotifications.WaitForResourceHealthyAsync(
            "drycleanerapi",
            timeout.Token);
    }

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        if (App is not null)
        {
            await App.DisposeAsync();
        }

        if (_appHost is not null)
        {
            await _appHost.DisposeAsync();
        }
    }
}