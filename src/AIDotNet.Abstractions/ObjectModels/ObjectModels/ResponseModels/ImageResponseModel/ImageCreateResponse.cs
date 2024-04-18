﻿using System.Text.Json.Serialization;
using AIDotNet.Abstractions.ObjectModels.ObjectModels.SharedModels;

namespace AIDotNet.Abstractions.ObjectModels.ObjectModels.ResponseModels.ImageResponseModel;

public record ImageCreateResponse : BaseResponse, IOpenAiModels.ICreatedAt
{
    [JsonPropertyName("data")] public List<ImageDataResult> Results { get; set; }

    [JsonPropertyName("created")] public int CreatedAt { get; set; }

    public record ImageDataResult
    {
        [JsonPropertyName("url")] public string Url { get; set; }
        [JsonPropertyName("b64_json")] public string B64 { get; set; }
        [JsonPropertyName("revised_prompt")] public string RevisedPrompt { get; set; }
    }
}