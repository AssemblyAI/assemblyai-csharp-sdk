using System.Text.Json.Serialization;
using AssemblyAI.Core;
using OneOf;

namespace AssemblyAI.Lemur;

public class LemurResponse : OneOfBase<LemurStringResponse, LemurQuestionAnswerResponse>
{
    private LemurResponse(OneOf<LemurStringResponse, LemurQuestionAnswerResponse> response) : base(response)
    {
    }

    public static implicit operator LemurResponse(OneOf<LemurStringResponse, LemurQuestionAnswerResponse> _) => new(_);
    public static implicit operator LemurResponse(LemurStringResponse _) => new(_);
    public static implicit operator LemurResponse(LemurQuestionAnswerResponse _) => new(_);
    
    public bool IsLemurStringResponse => IsT0;
    public bool IsLemurQuestionAnswerResponse => IsT1;
    public LemurStringResponse AsLemurStringResponse => AsT0;
    public LemurQuestionAnswerResponse AsLemurQuestionAnswerResponse => AsT1;
}