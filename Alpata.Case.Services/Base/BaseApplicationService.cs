using Alpata.Case.Domain.Base;
using Alpata.Case.Domain.Repository;
using Alpata.Case.Service.Contracts.Contracts.Base;
using Alpata.Case.Service.Contracts.Dtos.Base;
using AutoMapper;

namespace Alpata.Case.Services.Base
{
    public class BaseApplicationService<TEntity, TDto, TCreateDto, TUpdateDto> : IBaseApplicationService<TDto, TCreateDto, TUpdateDto>
    where TEntity : Entity
    where TDto : EntityDto
    where TCreateDto : class
    where TUpdateDto : EntityDto
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        public BaseApplicationService(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public virtual async Task<TDto> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<TDto> AddAsync(TCreateDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var entity = _mapper.Map<TEntity>(dto);
            entity = await _repository.AddAsync(entity);

            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task UpdateAsync(TUpdateDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var entity = _mapper.Map<TEntity>(dto);
            await _repository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(TDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var entity = _mapper.Map<TEntity>(dto);
            await _repository.DeleteAsync(entity);
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
