using discountServer;
using GenerationServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenerationServer.Repositories
{
    public class GenerateDiscountCodeRepository
    {
        private readonly ILogger<GenerateDiscountCodeRepository> logger;
        private readonly DBContext _dBContext;
        public GenerateDiscountCodeRepository(ILogger<GenerateDiscountCodeRepository> logger, DBContext dBContext)
        {
            this.logger = logger;
            _dBContext = dBContext;
        }

        public List<DiscountModel> Discounts { get; set; }

        private readonly Random rnd = new Random();
        public GenerateResponse GenerateCode(GenerateRequest request)
        {
            GenerateDiscountCodes(request);
            return new GenerateResponse { Message = true };
        }


        private List<string> generatedCodes = new List<string>();
        public async Task<bool> GenerateDiscountCodes(GenerateRequest request)
        {
            generatedCodes.Clear();
            var random = new Random();
            int MaxCodesPerGeneration = (int)request.Count;
            while (generatedCodes.Count < MaxCodesPerGeneration)
            {

                var code = GenerateRandomCode(random, request);
                generatedCodes.Add(code);
                DiscountCodes discountCodes = new DiscountCodes
                {
                    DiscountCode = code
                };
                _dBContext.DiscountCodes.Add(discountCodes);

            }
            await _dBContext.SaveChangesAsync();
            logger.LogInformation($"Generating {MaxCodesPerGeneration} random DiscountCodes");
            return true;
        }

        private string GenerateRandomCode(Random random, GenerateRequest request)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var length = random.Next(7, 8);
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());
        }
        public async Task<bool> UseGenerateCode(SearchRequest request)
        {
            var discount = _dBContext.DiscountCodes.Where(s => s.DiscountCode == request.DsiscountCode).FirstOrDefault();
            if (discount != null)
            {
                _dBContext.DiscountCodes.Remove(discount);
                await _dBContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<DiscountsResponse> GetAllDiscountCode()
        {
            DiscountsResponse discountsResponse = new DiscountsResponse();
            foreach (var s in _dBContext.DiscountCodes)
            {
                DiscountModel discountModel = new DiscountModel
                { Discountcode = s.DiscountCode };

                discountsResponse.Discount.Add(discountModel);
            }
            return discountsResponse;
        }
        public async Task<bool> DeleteAllDiscount()
        {
            var discount = _dBContext.DiscountCodes.ToList();
            if (discount.Count > 0)
            {
                _dBContext.DiscountCodes.RemoveRange(discount);
                await _dBContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}
