using discountServer;
using GenerationServer.Repositories;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GenerationServer.Services
{
    public class GenerateDiscountCodeService : DiscountCode.DiscountCodeBase
    {
        private readonly ILogger<GenerateDiscountCodeService> logger;
        private readonly GenerateDiscountCodeRepository repository;

        public GenerateDiscountCodeService(ILogger<GenerateDiscountCodeService> logger, GenerateDiscountCodeRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public override Task<GenerateResponse> GenerateCode(GenerateRequest request, ServerCallContext context)
        {
            GenerateResponse response = repository.GenerateCode(request);

            return Task.FromResult(response);
        }

        public override Task<DiscountModel> UseGenerateCode(SearchRequest request, ServerCallContext context)
        {
            logger.LogInformation($"SearchRequest:{request.DsiscountCode}");
            DiscountModel response = new DiscountModel();
            bool result = repository.UseGenerateCode(request).Result;
            response.Discountcode = (result) ? request.DsiscountCode : response.Discountcode = string.Empty;
            return Task.FromResult(response);
        }
        public override Task<DiscountsResponse> GetAllDiscountCode(GetAllRequest request, ServerCallContext context)
        {
            DiscountsResponse discountsResponse = repository.GetAllDiscountCode().Result;
            return Task.FromResult(discountsResponse);
        }


        public override Task<ResponseMessage> DeleteAllDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            bool result = repository.DeleteAllDiscount().Result;
            {
                logger.LogInformation("AllDiscount Code is successfuly deleted");
                return (result) ? Task.FromResult(new ResponseMessage { Message = "AllDiscount Code is successfuly deleted" }) : Task.FromResult(new ResponseMessage { Message = "" });

            }


        }

    }
}
