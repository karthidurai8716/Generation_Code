using DiscountClient;
using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GenerationClient
{

    public class GenerationDiscountCodeClient
    {

        public GenerationDiscountCodeClient(string serverUrl)
        {
            generationChannel = GrpcChannel.ForAddress(serverUrl);
            generationClient = new DiscountCode.DiscountCodeClient(generationChannel);
        }

        private GrpcChannel generationChannel;
        private DiscountCode.DiscountCodeClient generationClient;

        internal async Task GenerateCode()
        {
            try
            {

                GenerateRequest request = UIHelper.InputGenerateParameters();
                GenerateResponse response = generationClient.GenerateCode(request);
                Console.WriteLine($"Discount Code is successfully generated({response.Message})");
            }
            catch (RpcException rpcException)
            {
                Console.WriteLine("There was an error communicating with gRPC server");
                Console.WriteLine($"Code: {rpcException.StatusCode}, Status: {rpcException.Status}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        internal async Task UseDiscountCode()
        {
            try
            {

                SearchRequest request = UIHelper.InputSearchParameters();
                DiscountModel response = generationClient.UseGenerateCode(request);
                if (response.Discountcode == request.DsiscountCode)
                    Console.WriteLine($"The DiscountCode: {response.Discountcode} Applied");
                else
                    Console.WriteLine($"The DiscountCode is Invalid. Please try again");
            }
            catch (RpcException rpcException)
            {
                Console.WriteLine("There was an error communicating with gRPC server");
                Console.WriteLine($"Code: {rpcException.StatusCode}, Status: {rpcException.Status}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        internal async Task GetAllDiscountCode()
        {
            try
            {
                DiscountsResponse result = generationClient.GetAllDiscountCode(new GetAllRequest());
                Console.WriteLine("[{0}]", string.Join(", ", result.Discount.Select(s => s.Discountcode).ToList()));
            }
            catch (RpcException rpcException)
            {
                Console.WriteLine("There was an error communicating with gRPC server");
                Console.WriteLine($"Code: {rpcException.StatusCode}, Status: {rpcException.Status}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        internal async Task DeleteAllDiscount()
        {
            try
            {
                ResponseMessage result = generationClient.DeleteAllDiscount(new DeleteDiscountRequest());
                Console.WriteLine(result.Message);
            }
            catch (RpcException rpcException)
            {
                Console.WriteLine("There was an error communicating with gRPC server");
                Console.WriteLine($"Code: {rpcException.StatusCode}, Status: {rpcException.Status}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

    }

}
