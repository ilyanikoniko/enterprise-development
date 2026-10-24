namespace DryCleaner.Contracts.Dtos;

public record CategoryStatDto(
    IReadOnlyList<CategoryCountDto> MostPopular,
    IReadOnlyList<CategoryCountDto> LeastPopular
);

public record CategoryCountDto(int CategoryId, string CategoryName, int OrdersCount);