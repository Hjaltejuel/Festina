using System;
using System.IO;
using System.Text;

using Newtonsoft.Json;

using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.APIGatewayEvents;
using System.Collections.Generic;
using System.Net;
using Amazon.DynamoDBv2;
using System.Threading.Tasks;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace KeyboardSound
{
    public class Function
    {
        private static readonly JsonSerializer _jsonSerializer = new JsonSerializer();
        private static readonly Random random = new Random();

        //ENDPOINT : https://18knounbuf.execute-api.eu-north-1.amazonaws.com/prod/keyboardsound
        public async Task<String> GET(APIGatewayProxyRequest request, ILambdaContext context)
        {
            string s = "[";

            using (var client = new AmazonDynamoDBClient())
            {
                var response = await client.ScanAsync(new ScanRequest("KeyboardSound"));

             

                foreach(Dictionary<string,AttributeValue> item in response.Items)
                {
                    s += @"{'SensorId': '" + int.Parse(item["SensorId"].N) + "',";
                    s += @"'Date': '" + item["Date"].S + "',";
                    s += @"'Value': '" + item["Value"].N + "'";
                    s += "},";
                }

                s = s.Substring(0, s.Length - 1);
                s += "]";
            }

            return (s);
        }
        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            if (request.HttpMethod.Equals("GET")){
                return CreateResponse(await GET(request, context));
            }
            var client = new AmazonDynamoDBClient();
            var settings = new JsonSerializerSettings { DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ" };
            InputData input = Newtonsoft.Json.JsonConvert.DeserializeObject<InputData>(request.Body,settings);

            var request1 = new PutItemRequest
            {
                TableName = "KeyboardSound",
                Item = new Dictionary<string, AttributeValue>
                {
                  {"Date" ,new AttributeValue {S =DateTime.Now.ToUniversalTime().ToString()}},
                  {"SensorId", new AttributeValue { N = input.SensorId.ToString()}},
                  { "Value", new AttributeValue { N =  input.Value.ToString() }}
                }
            };

           

            await client.PutItemAsync(request1);
          
            LambdaLogger.Log($"Calling function name: {context.FunctionName}\\n");
            return CreateResponse($"Data recieved: {input.SensorId} {input.Value}");
        }

        APIGatewayProxyResponse CreateResponse(String result)
        {
            int statusCode = (result != null) ?
                (int)HttpStatusCode.OK :
                (int)HttpStatusCode.InternalServerError;

            string body = (result != null) ?
                JsonConvert.SerializeObject(result) : string.Empty;

            var response = new APIGatewayProxyResponse
            {
                StatusCode = statusCode,
                Body = body,
                Headers = new Dictionary<string, string>
        {
            { "Content-Type", "application/json" },
            { "Access-Control-Allow-Origin", "*" }
        }
            };

            return response;
        }


    }
}