using IceProducts.Shared.Dtos;

namespace IceProducts.Server.Services.interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> Get();
        Task Update(Guid productId, UpdateProductInputModel updateProductInput);
        Task Delete(Guid id);
        
    }
}
