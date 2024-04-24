using AIDotNet.Abstractions;
using AIDotNet.Abstractions.Exceptions;
using AIDotNet.Abstractions.ObjectModels.ObjectModels.RequestModels;
using AIDotNet.Abstractions.ObjectModels.ObjectModels.ResponseModels;
using AIDotNet.LLamaSharp.HistoryTransform;
using LLama;
using LLama.Abstractions;
using LLama.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDotNet.LLamaSharp
{
    public class LLamaSharpChatService : IApiChatCompletionService
    {
        private ConcurrentDictionary<string, LLamaWeights> _models { get; set; }

        public LLamaSharpChatService()
        {
            _models = new ConcurrentDictionary<string, LLamaWeights>();
        }

        private LLamaContext GetModelContext(string modelName)
        {
            ModelParams modelParams;
            switch (modelName.ToLower())
            {
                case "llama3":
                    modelParams = new ModelParams("./Models/llama3.gguf")
                    {
                        ContextSize = 8192
                    };
                    break;
                default:
                    throw new NotModelException($"{modelName ?? "model"} does not exist");
            }
            var model = _models.GetOrAdd(modelName, LLamaWeights.LoadFromFile(modelParams));
            return model.CreateContext(modelParams);
        }

        public async Task<ChatCompletionCreateResponse> CompleteChatAsync(ChatCompletionCreateRequest chatCompletionCreate, ChatOptions? options = null, CancellationToken cancellationToken = default)
        {
            if (chatCompletionCreate.Model == null)
                throw new NotModelException($"{chatCompletionCreate.Model ?? "model"} does not exist");

            using var model = GetModelContext(chatCompletionCreate.Model);
            var executor = new InteractiveExecutor(model);
            var chatHistory = new ChatHistory();
            foreach (var item in chatCompletionCreate.Messages)
            {
                switch (item.Role.ToLower())
                {
                    case "system":
                        chatHistory.AddMessage(AuthorRole.System, (string)item.ContentCalculated);
                        break;
                    case "user":
                        chatHistory.AddMessage(AuthorRole.User, (string)item.ContentCalculated);
                        break;
                    case "assistant":
                        chatHistory.AddMessage(AuthorRole.Assistant, (string)item.ContentCalculated);
                        break;
                    default:
                        chatHistory.AddMessage(AuthorRole.Unknown, (string)item.ContentCalculated);
                        break;
                }
            }
            var session = new ChatSession(executor, chatHistory);
            var inferenceParams = new InferenceParams()
            {
                MaxTokens = chatCompletionCreate.MaxTokens ?? 1024,
                AntiPrompts = chatCompletionCreate.StopCalculated?.ToList() ?? [],
                Temperature = chatCompletionCreate.Temperature ?? 0.8f,
                TopP = chatCompletionCreate.TopP ?? 0.95f
            };

            switch (chatCompletionCreate.Model.ToLower())
            {
                case "llama3":
                    session.WithHistoryTransform(new Llama3HistoryTransform());
                    inferenceParams.AntiPrompts = ["<|eot_id|>", "<|end_of_text|>"];
                    break;
                default:
                    break;
            }

            var retMessage = new OpenAI.ObjectModels.RequestModels.ChatMessage()
            {
                Role = "assistant"
            };
            var ret = new ChatCompletionCreateResponse()
            {
                Model = chatCompletionCreate.Model,
                Choices =
                [
                    new Abstractions.ObjectModels.ObjectModels.SharedModels.ChatChoiceResponse()
                    {
                        Delta = retMessage,
                        FinishReason = "stop",
                        Index = 0,
                    }
                ],
                Usage = new UsageResponse()
            };

            await foreach (var item in session.ChatAsync(chatHistory, inferenceParams, cancellationToken))
            {
                retMessage.Content += item;
            }
            return ret;
        }

        public async IAsyncEnumerable<ChatCompletionCreateResponse> StreamChatAsync(ChatCompletionCreateRequest chatCompletionCreate, ChatOptions? options = null, CancellationToken cancellationToken = default)
        {
            if (chatCompletionCreate.Model == null)
                throw new NotModelException($"{chatCompletionCreate.Model ?? "model"} does not exist");

            using var model = GetModelContext(chatCompletionCreate.Model);
            var executor = new InteractiveExecutor(model);
            var chatHistory = new ChatHistory();
            foreach (var item in chatCompletionCreate.Messages)
            {
                switch (item.Role.ToLower())
                {
                    case "system":
                        chatHistory.AddMessage(AuthorRole.System, (string)item.ContentCalculated);
                        break;
                    case "user":
                        chatHistory.AddMessage(AuthorRole.User, (string)item.ContentCalculated);
                        break;
                    case "assistant":
                        chatHistory.AddMessage(AuthorRole.Assistant, (string)item.ContentCalculated);
                        break;
                    default:
                        chatHistory.AddMessage(AuthorRole.Unknown, (string)item.ContentCalculated);
                        break;
                }
            }
            var session = new ChatSession(executor, chatHistory);
            var inferenceParams = new InferenceParams()
            {
                MaxTokens = chatCompletionCreate.MaxTokens ?? 1024,
                AntiPrompts = chatCompletionCreate.StopCalculated?.ToList() ?? [],
                Temperature = chatCompletionCreate.Temperature ?? 0.8f,
                TopP = chatCompletionCreate.TopP ?? 0.95f
            };

            await foreach (var item in session.ChatAsync(chatHistory, inferenceParams, cancellationToken))
            {
                yield return new ChatCompletionCreateResponse()
                {
                    Model = chatCompletionCreate.Model,
                    Choices =
                    [
                        new Abstractions.ObjectModels.ObjectModels.SharedModels.ChatChoiceResponse()
                        {
                            Delta = new OpenAI.ObjectModels.RequestModels.ChatMessage()
                            {
                                Role = "assistant",
                                Content = item
                            },
                            FinishReason = "stop",
                            Index = 0,
                        }
                    ],
                    Usage = new UsageResponse()
                };
            }
        }
    }
}
