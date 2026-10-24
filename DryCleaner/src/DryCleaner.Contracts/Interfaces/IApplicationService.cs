namespace DryCleaner.Contracts.Interfaces;

public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
{
    public Task<TDto> Create(TCreateUpdateDto dto);
    public Task<TDto?> Get(TKey id);
    public Task<IList<TDto>> GetAll();
    public Task<TDto> Update(TCreateUpdateDto dto, TKey id);
    public Task<bool> Delete(TKey id);
}