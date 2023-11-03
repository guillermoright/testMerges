using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Testcontainers.Elasticsearch;
using Xunit;

public sealed class ElasticsearchContainerTest2 : IAsyncLifetime
{
    private readonly ElasticsearchContainer _elasticsearch
        = new ElasticsearchBuilder().Build();

    [Fact]
    public async Task ReadFromElasticsearch()
    {
        var settings = new ElasticsearchClientSettings(new Uri(_elasticsearch.GetConnectionString()));
        settings.ServerCertificateValidationCallback(CertificateValidations.AllowAll);

        var client = new ElasticsearchClient(settings);

        var stats = await client.PingAsync();
         
        Assert.True(stats.IsValidResponse);
    }

    public Task InitializeAsync()
        => _elasticsearch.StartAsync();

    public Task DisposeAsync()
        => _elasticsearch.DisposeAsync().AsTask();
}