using DiscountClient;
using System;
using System.Linq;

namespace GenerationClient
{
    public static class UIHelper
    {
        public static GenerateRequest InputGenerateParameters()
        {
            GenerateRequest renerateRequest = new GenerateRequest();
            Console.Write($"Please Enter the number of codes Ex..(1000 to 2000): ");
            renerateRequest.Count = Convert.ToUInt32(Console.ReadLine());
            Console.Write($"Please Enter Length of the code  Ex..(7-8): ");
            string text = Console.ReadLine();
            byte[] byteArr = text.Split('-').Select(x => Convert.ToByte(x)).ToArray();
            renerateRequest.Length = Google.Protobuf.ByteString.CopyFrom(byteArr);
            return renerateRequest;
        }
        public static SearchRequest InputSearchParameters()
        {
            SearchRequest searchRequest = new SearchRequest();
            searchRequest.DsiscountCode = StringInputSameLine("Enter part of the Discount code");
            return searchRequest;
        }
        public static string StringInputSameLine(string inputMessage)
        {
            Console.Write($"{inputMessage}: ");
            return Console.ReadLine();
        }
    }
}
