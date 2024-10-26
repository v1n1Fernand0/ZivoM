using AutoMapper;
using ZivoM.Categories;
using ZivoM.Transactions;

namespace ZivoM.Mappings
{
    public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Transaction, TransactionDTO>().ReverseMap();
            CreateMap<CreateUpdateTransactionDTO, Transaction>().ReverseMap();

            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<CreateUpdateCategoryDTO, Category>().ReverseMap();
        }
    }
}
