using LLama.Abstractions;
using LLama.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDotNet.LLamaSharp.HistoryTransform
{
    public class Llama3HistoryTransform : IHistoryTransform
    {
        private readonly string defaultUserName = "User";
        private readonly string defaultAssistantName = "Assistant";
        private readonly string defaultSystemName = "System";
        private readonly string defaultUnknownName = "??";

        string _userName;
        string _assistantName;
        string _systemName;
        string _unknownName;
        bool _isInstructMode;
        public Llama3HistoryTransform(string? userName = null, string? assistantName = null,
            string? systemName = null, string? unknownName = null, bool isInstructMode = false)
        {
            _userName = userName ?? defaultUserName;
            _assistantName = assistantName ?? defaultAssistantName;
            _systemName = systemName ?? defaultSystemName;
            _unknownName = unknownName ?? defaultUnknownName;
            _isInstructMode = isInstructMode;
        }

        public IHistoryTransform Clone()
        {
            return new Llama3HistoryTransform(_userName, _assistantName, _systemName, _unknownName, _isInstructMode);
        }

        public virtual string HistoryToText(ChatHistory history)
        {
            StringBuilder sb = new();
            foreach (var message in history.Messages)
            {
                sb.Append("<|begin_of_text|>");
                if (message.AuthorRole == AuthorRole.User)
                {
                    sb.Append($"<|start_header_id|>user<|end_header_id|>\n\n{message.Content}<|eot_id|>");
                }
                else if (message.AuthorRole == AuthorRole.System)
                {
                    sb.Append($"<|start_header_id|>system<|end_header_id|>\n\n{message.Content}<|eot_id|>");
                }
                else if (message.AuthorRole == AuthorRole.Assistant)
                {
                    sb.Append($"<|start_header_id|>assistant<|end_header_id|>\n\n{message.Content}<|eot_id|>");
                }
            }
            sb.Append($"<|start_header_id|>assistant<|end_header_id|>\n\n");
            var ret = sb.ToString();
            return ret;
        }

        public virtual ChatHistory TextToHistory(AuthorRole role, string text)
        {
            ChatHistory history = new ChatHistory();
            history.AddMessage(role, TrimNamesFromText(text, role));
            return history;
        }

        public virtual string TrimNamesFromText(string text, AuthorRole role)
        {
            if (role == AuthorRole.User && text.StartsWith($"{_userName}:"))
            {
                text = text.Substring($"{_userName}:".Length).TrimStart();
            }
            else if (role == AuthorRole.Assistant && text.EndsWith($"{_assistantName}:"))
            {
                text = text.Substring(0, text.Length - $"{_assistantName}:".Length).TrimEnd();
            }
            if (_isInstructMode && role == AuthorRole.Assistant && text.EndsWith("\n> "))
            {
                text = text.Substring(0, text.Length - "\n> ".Length).TrimEnd();
            }
            return text;
        }
    }
}
