using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AutoBugReporter.ModelClasses
{
    public class Ticket
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("tasknumber")]
        public int TaskNumber { get; set; }

        [JsonPropertyName("name")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("priority")]
        public int Priority { get; set; }

        [JsonPropertyName("executor")]
        public Executor Executor { get; set; }

        [JsonPropertyName("tags")]
        public List<int> Tags { get; set; }

        [JsonPropertyName("createdat")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedat")]
        public DateTime UpdatedAt { get; set; }
    }

    public class Executor
    {
        [JsonPropertyName("userid")]
        public int? UserId { get; set; }

        [JsonPropertyName("groupid")]
        public int? GroupId { get; set; }
    }

    public class CreateTicketRequest
    {
        [JsonPropertyName("blocks")]
        public TicketBlocks Blocks { get; set; }

        [JsonPropertyName("Channel")]
        public string Channel { get; set; } = "web";
    }

    public class TicketBlocks
    {
        [JsonPropertyName("name")]
        public ValueWrapper<string> Name { get; set; } = new ValueWrapper<string>();

        [JsonPropertyName("description")]
        public ValueWrapper<string> Description { get; set; } = new ValueWrapper<string>();

        [JsonPropertyName("tags")]
        public ValueWrapper<List<int>> Tags { get; set; } = new ValueWrapper<List<int>>(new List<int>());
    }

    public class ValueWrapper<T>
    {
        [JsonPropertyName("value")]
        public T Value { get; set; }

        // Конструктор без параметров (нужен для десериализации)
        public ValueWrapper() { }

        // Конструктор с параметром
        public ValueWrapper(T value)
        {
            Value = value;
        }
    }

    public class UpdateTicketRequest
    {
        [JsonPropertyName("number")]
        public int TicketNumber { get; set; }

        [JsonPropertyName("blocks")]
        public UpdateTicketBlocks Blocks { get; set; }

        [JsonPropertyName("Channel")]
        public string Channel { get; set; } = "api";
    }

    public class UpdateTicketBlocks
    {
        [JsonPropertyName("status")]
        public ValueWrapper<int> Status { get; set; }

        [JsonPropertyName("service")]
        public ValueWrapper<string> Service { get; set; }

        [JsonPropertyName("tasktype")]
        public ValueWrapper<int> TaskType { get; set; }

        [JsonPropertyName("executor")]
        public ValueWrapper<Executor> Executor { get; set; }

        [JsonPropertyName("resolutiondateplan")]
        public ValueWrapper<DateTime> ResolutionDatePlan { get; set; }

        [JsonPropertyName("tags")]
        public ValueWrapper<List<int>> Tags { get; set; }

        [JsonPropertyName("comment")]
        public ValueWrapper<string> Comment { get; set; }

        [JsonPropertyName("evaluation")]
        public ValueWrapper<Evaluation> Evaluation { get; set; }
    }

    public class Evaluation
    {
        [JsonPropertyName("value")]
        public int Value { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

    public class AddCommentRequest
    {
        [JsonPropertyName("number")]
        public int TicketNumber { get; set; }

        [JsonPropertyName("blocks")]
        public CommentBlock Blocks { get; set; }

        [JsonPropertyName("Channel")]
        public string Channel { get; set; } = "api";
    }

    public class CommentBlock
    {
        [JsonPropertyName("comment")]
        public ValueWrapper<string> Comment { get; set; }
    }
}
