using Alpata.Case.Service.Contracts.Dtos.Base;

namespace Alpata.Case.Service.Contracts.Contracts.Base
{
    public interface IBaseApplicationService<TDto,TCreateDto,TUpdateDto> 
        where TDto : EntityDto 
        where TCreateDto : class 
        where TUpdateDto:EntityDto
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync(Guid id);
        Task<TDto> AddAsync(TCreateDto dto);
        Task UpdateAsync(TUpdateDto dto);
        Task DeleteAsync(TDto dto);
        Task DeleteAsync(Guid id);
    }
}
