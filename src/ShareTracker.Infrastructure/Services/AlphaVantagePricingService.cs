using System.Text.Json;
using Microsoft.Extensions.Configuration;
using ShareTracker.Application.DTOs;
using ShareTracker.Application.Interfaces;

namespace ShareTracker.Infrastructure.Services;

public class AlphaVantagePricingService : IPricingService
{
    private static readonly Dictionary<string, string> _currencyByExchange = new()
    {
        { "AMS", "EUR" },
        { "NYSE", "USD" },
        { "NASDAQ", "USD" },
        { "LSE", "GBP" },
    };

    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public AlphaVantagePricingService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<PriceResult?> GetCurrentPriceAsync(string ticker)
    {
        string url = $"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={ticker}&apikey={_configuration["AlphaVantage:ApiKey"]}";

        string response = await _httpClient.GetStringAsync(url);

        using var doc = JsonDocument.Parse(response);
        var root = doc.RootElement;
        var quote = root.GetProperty("Global Quote");
        var priceString = quote.GetProperty("05. price").GetString();

        if (!decimal.TryParse(priceString, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var priceValue))
            return null;

        var exchange = ticker.Contains('.') ? ticker.Split('.').Last() : string.Empty;
        var currency = _currencyByExchange.TryGetValue(exchange, out var c) ? c : "USD";

        return new PriceResult { Price = priceValue, Currency = currency };
    }
}