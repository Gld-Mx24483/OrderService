namespace OrderServiceApi.Api.Endpoints
{
    public static class ResponseMapper
    {
        public static IResult MapResponse(string responseCode, object response)
        {
            return responseCode switch
            {
                "00" => Results.Json(response, statusCode: 200),
                "01" => Results.Json(response, statusCode: 400),
                "02" => Results.Json(response, statusCode: 500),
                _ => Results.Json(response, statusCode: 500)
            };
        }
    }
}